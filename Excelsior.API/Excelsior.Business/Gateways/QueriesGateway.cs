using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using Excelsior.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace Excelsior.Business.Gateways
{
    public class QueriesGateway : IQueriesGateway
    {
        public IQueriesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        private IAuthUserRepository AuthRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }

        public QueriesGateway(IQueriesRepository repository, IAuditRecordsRepository auditRecordsRepository, IAuthUserRepository authRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            AuthRepository = authRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        private void SetDtoValues(QueryBaseDto dto, QRY_Query entity, CONTACT_User user)
        {
            dto.TotalMessages = Repository.GetTotalMessages(entity.QueryID);
            dto.LastMessage = new QueryMessageFullDto(Repository.GetLastMessage(entity.QueryID), this);

            //long? taffiliationId = null;
            //if (entity.SeriesID != null)
            //    taffiliationId = entity.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.AffiliationID;
            //else if (entity.CertEquipmentID != null)
            //    taffiliationId = entity.CERTEquipment.CONTACTEquipment.AffiliationID;
            //else if (entity.CertUserID != null)
            //    taffiliationId = entity.CERTUser.CONTACTUserTrial.CONTACTUser.AffiliationID;
            var uaffiliationId = user.AffiliationID;

            switch (user.AspnetRole.LoweredRoleName)
            {
                case "administrator":
                case "project manager":
                case "study manager":
                    //dto.IsFlagged = (dto.Status == "Pending Resolution");
                    //break;
                case "super user":
                case "data quality evaluator":
                    dto.IsFlagged = (dto.Status == "Pending Resolution") && entity.Sender.AffiliationID == uaffiliationId;
                    break;
                case "site coordinator":
                case "ophthalmic technician":
                    dto.IsFlagged = (dto.Status == "Pending Response");// && taffiliationId == uaffiliationId;
                    break;
                default:
                    dto.IsFlagged = false;
                    break;
            }
        }

        public ResultInfo<IList<QueryBaseDto>> GetAll(QueriesRequestDto request)
        {
            //Get the result
            var result = new ResultInfo<IList<QueryBaseDto>>();
            try
            {
                if (!request.StudyId.HasValue)
                    throw new Exception("StudyId is required.");

                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var respose = new List<QueryBaseDto>();
                var entities = Repository.GetAll(user, request.StudyId.Value, request.SiteId, request.SeriesId, request.CertEquipmentId, request.CertUserId, request.QueryType, request.QueryStatus, request.IsActive, request.Search, request.Sort);
                var count = 0;
                try
                {
                    count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return entities.Count();
                    });
                }
                catch (Exception e)
                {
                }

                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities, result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (dynamic entity in entitiesPaged)
                    {
                        QueryBaseDto dto = null;
                        if(request.SeriesId.HasValue)
                            dto = new QueryBaseDto(entity, new SeriesBaseDto());
                        else if (request.CertUserId.HasValue)
                            dto = new QueryBaseDto(entity, new CertUserBaseDto());
                        else if (request.CertEquipmentId.HasValue)
                            dto = new QueryBaseDto(entity, new CertEquipmentBaseDto());
                        else
                            dto = new QueryBaseDto(entity);
                        SetDtoValues(dto, entity, user);
                        respose.Add(dto);
                    }
                }

                result.Result = respose;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<QueryFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<QueryFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.QueryID == id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    var dto = new QueryFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Query not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<QueryFullDto> Add(QueryFullDto request)
        {
            var result = new ResultInfo<QueryFullDto>();
            try
            {
                var msg = request.LastMessage;
                if (msg == null)
                    throw new Exception("LastMessage is required.");

                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var entity = request.ToEntity();
                entity.SenderID = user.UserID;
                entity.InitiateDate = DateTime.UtcNow;
                entity.IsActive = true;
                entity.IsResolved = false;
                Repository.Add(entity);

                var msgEntity = msg.ToEntity();
                msgEntity.UserID = user.UserID;
                msgEntity.DateCreated = DateTime.UtcNow;
                msgEntity.IsActive = true;
                Repository.Context.Add(msgEntity);
                msgEntity.QRYQuery = entity;

                var status = Repository.Context.QRY_Status.FirstOrDefault(x => x.StatusName == "Pending Response");
                entity.StatusID = status?.StatusID;

                Repository.Commit();
                Repository.Refresh(entity);
                Repository.Refresh(msgEntity);
                var dto = new QueryFullDto(entity);
                SetDtoValues(dto, entity, user);
                result.Result = dto;
                result.IsSuccess = true;

                //Send notification to site users
                NotificationsHelper.NotifyNewQueryMessage(msgEntity);
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<QueryFullDto> Update(QueryFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<QueryFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.QueryID == request.Id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    entity = request.ToEntity(entity, fields);
                    Repository.Update(entity);
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new QueryFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Query not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<bool> Delete(long id)
        {
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.QueryID == id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    if (user.UserID != entity.SenderID && !"[Administrator]".Contains(user.AspnetRole.RoleName))
                        throw new Exception("Query can only be deleted by the the user that created it.");

                    Repository.Delete(entity);
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Query not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<QueryFullDto> Resolve(long id, string password = null, string reason = null)
        {
            var result = new ResultInfo<QueryFullDto>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                //Validate Password
                if (!UserHelper.ValidatePassword(aspUser, password))
                    throw new Exception("Invalid password.");

                var entity = Repository.GetSingle(x => x.QueryID == id);
                if (entity != null)
                {
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    if (!((user.AffiliationID == entity.Sender.AffiliationID && "[Study Manager][Super User][Data Quality Evaluator]".Contains(user.AspnetRole.RoleName)) || "[Administrator][Project Manager]".Contains(user.AspnetRole.RoleName)))
                        throw new UnauthorizedAccessException("You are not authorized to resolve the query.");

                    var status = Repository.Context.QRY_Status.FirstOrDefault(x => x.StatusName == "Resolved");
                    entity.StatusID = status?.StatusID;
                    entity.IsResolved = true;
                    entity.DateResolved = DateTime.UtcNow;

                    var msgEntity = new QRY_Message()
                    {
                        MessageBody = reason,
                        UserID = user.UserID,
                        DateCreated = DateTime.UtcNow,
                        IsActive = true,
                        QueryID = id
                    };

                    Repository.Context.Add(msgEntity);

                    Repository.Commit();
                    Repository.Refresh(entity);
                    //Repository.Refresh(msgEntity);
                    var dto = new QueryFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;

                    Task.Factory.StartNew(() =>
                    {
                        NotificationsHelper.NotifyQueryResolved(msgEntity);
                    });
                }
                else
                {
                    throw new Exception("Query not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }
    }
}
