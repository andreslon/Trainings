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

namespace Excelsior.Business.Gateways
{
    public class QueryMessagesGateway : IQueryMessagesGateway
    {
        public IQueryMessagesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }

        public QueryMessagesGateway(IQueryMessagesRepository repository, IAuditRecordsRepository auditRecordsRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        public ResultInfo<IList<QueryMessageBaseDto>> GetAll(QueryMessagesRequestDto request)
        {
            //Get the result
            var result = new ResultInfo<IList<QueryMessageBaseDto>>();
            try
            {
                if (!request.QueryId.HasValue)
                    throw new Exception("QueryId is required.");
                var respose = new List<QueryMessageBaseDto>();
                var entities = Repository.GetAll(request.QueryId.Value, request.IsActive, request.Search);
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
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new QueryMessageBaseDto(entity);
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

        public ResultInfo<QueryMessageFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<QueryMessageFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.QueryID == id);
                if (entity != null)
                {
                    var dto = new QueryMessageFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Query Message not found");
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

        public ResultInfo<QueryMessageFullDto> Add(QueryMessageFullDto request)
        {
            var result = new ResultInfo<QueryMessageFullDto>();
            try
            {
                var query = Repository.Context.QRY_Queries.FirstOrDefault(x => x.QueryID == request.QueryId);

                if (query == null)
                    throw new Exception("Query not found.");

                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var entity = request.ToEntity();

                if (entity.UserID == null)
                    entity.UserID = user.UserID;

                SetQueryStatus(entity, query);
                Repository.Add(entity);
                //var record = AuditRecordsRepository.AddRecord("ActionUndefined");

                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new QueryMessageFullDto(entity);
                result.IsSuccess = true;

                //TO DO: send to worker role
                NotificationsHelper.NotifyNewQueryMessage(entity);
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

        private void SetQueryStatus(QRY_Message message, QRY_Query query)
        {
            var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.UserID == message.UserID);
            if (user == null)
                throw new Exception("User not found.");

            string statusName = query?.QRYStatus?.StatusName;
            if (statusName == "Resolved")
                throw new Exception("Can not add message to a resolved query.");

            //long? targetAffiliationId = null;
            //if (query.SeriesID != null)
            //    targetAffiliationId = query.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.AffiliationID;
            //else if (query.CertEquipmentID != null)
            //    targetAffiliationId = query.CERTEquipment.CONTACTEquipment.AffiliationID;
            //else if (query.CertUserID != null)
            //    targetAffiliationId = query.CERTUser.CONTACTUserTrial.CONTACTUser.AffiliationID;

            //if (targetAffiliationId == null)
            //    throw new Exception("Affiliation not found.");

            //if (targetAffiliationId == user.AffiliationID)
            //    statusName = "Pending Resolution";
            //else
            //    statusName = "Pending Response";

            if (query.SeriesID != null || query.CertEquipmentID != null || query.CertUserID != null)
            {
                switch (user.AspnetRole.LoweredRoleName)
                {
                    case "ophthalmic technician":
                    case "site coordinator":
                        statusName = "Pending Resolution";
                        break;
                    case "administrator":
                    case "project manager":
                    case "manager":
                    case "data quality evaluator":
                        statusName = "Pending Response";
                        break;
                }
            }
            else
            {
                throw new Exception("Query type not supported");
            }

            if (statusName != query?.QRYStatus?.StatusName)
            {
                var status = Repository.Context.QRY_Status.FirstOrDefault(x => x.StatusName == statusName);
                query.StatusID = status.StatusID;
            }
        }

        public ResultInfo<QueryMessageFullDto> Update(QueryMessageFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<QueryMessageFullDto>();
            try
            {
                throw new NotImplementedException("Modify a query message is not supported.");
                //var entity = Repository.GetSingle(x => x.QueryID == request.Id);
                //if (entity != null)
                //{
                //    entity = request.ToEntity(entity, fields);
                //    Repository.Update(entity);
                //    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                //    Repository.Commit();
                //    Repository.Refresh(entity);
                //    result.Result = new QueryMessageFullDto(entity);
                //    result.IsSuccess = true;
                //}
                //else
                //{
                //    throw new Exception("Query Message not found");
                //}
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
                    Repository.Delete(entity);
                    var query = entity.QRYQuery;
                    var lastMessage = query.QRY_Messages.Last(x => x.IsActive && x.MessageID != entity.MessageID);
                    SetQueryStatus(lastMessage, entity.QRYQuery);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Query Message not found");
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
    }
}
