using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways.Interfaces;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using Excelsior.Domain.Repositories.Interface;
using Excelsior.Infrastructure.Interfaces;
using Excelsior.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Excelsior.Business.Gateways
{
    public class CertUsersGateway : ICertUsersGateway
    {
        public ICertUsersRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        private IAuthUserRepository AuthRepository { get; set; }
        public IUsersRepository UsersRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }
        public CertUsersGateway(ICertUsersRepository repository, IAuditRecordsRepository auditRecordsRepository, IAuthUserRepository authRepository, IUsersRepository usersRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            AuthRepository = authRepository;
            UsersRepository = usersRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        private void SetDtoValues(CertUserBaseDto dto, CERT_User entity, CONTACT_User user)
        {
            dto.TotalUploads = Repository.GetTotalUploads(entity.CertUserID);
            dto.TotalQueriesPending = Repository.GetTotalQueriesPending(entity);
            dto.TotalQueriesFlagged = Repository.GetTotalQueriesFlagged(entity, user);

            dto.hasPrevCert = Repository.GetPrevCertifications(entity, user).Any();
            dto.LastSubmissionDate = Repository.Context.PACS_Series.Where(s => s.IsActive && s.PACSTimePoint.PACSSubject.PACSSite.TrialID == entity.CONTACTUserTrial.TrialID && s.PhotographerID == entity.CONTACTUserTrial.UserID && s.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == entity.ImgProcedureID).OrderByDescending(s => s.StudyDate).FirstOrDefault()?.StudyDate;
        }

        public ResultInfo<IList<CertUserBaseDto>> GetAll(CertUserRequestDto request)
        {
            var result = new ResultInfo<IList<CertUserBaseDto>>();
            try
            {
                var CertUserListResponse = new List<CertUserBaseDto>();
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var entities = Repository.GetAll(user, request.StudyId, request.AffiliationId, request.TechnicianId, request.ProcedureId, request.IsActive, request.IsCertified, request.HasPrevCert, request.AssignedTo, request.Search, request.Sort);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities, result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new CertUserBaseDto(entity);
                        SetDtoValues(dto, entity, user);
                        CertUserListResponse.Add(dto);
                    }
                }

                result.Result = CertUserListResponse;
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

        public ResultInfo<CertUserFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<CertUserFullDto>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var certUser = Repository.Context.CERT_Users.FirstOrDefault(x => x.CertUserID == id);
                if (certUser == null)
                    throw new Exception("Cert User not found");

                var studyId = certUser.CONTACTUserTrial?.TrialID;
                PACS_Trial study = null;

                if (studyId == null)
                    throw new Exception("Study not found");

                switch (user.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                    case "project manager":
                    case "super user":
                        study = Repository.Context.PACS_Trials.FirstOrDefault(t => t.TrialID == studyId);
                        break;
                    default:
                        study = Repository.Context.CONTACT_UserTrials.FirstOrDefault(t => t.TrialID == studyId && t.UserID == user.UserID)?.PACSTrial;
                        break;
                }

                if (study == null)
                    throw new UnauthorizedAccessException("Access denied");

                var hasAccess = false;
                var affiliationId = certUser.CONTACTUserTrial?.CONTACTUser?.AffiliationID;

                switch (user.AspnetRole.LoweredRoleName)
                {
                    case "site coordinator":
                    case "ophthalmic technician":
                        if (user.AffiliationID == affiliationId)
                            hasAccess = true;
                        break;
                    default:
                        hasAccess = true;
                        break;
                }

                if (!hasAccess)
                    throw new UnauthorizedAccessException("Access denied");

                var users = Repository.GetAll(user, studyId, null, null, null, null, null, null, null, null, null);

                var entity = users.FirstOrDefault(x => x.CertUserID == id);

                //var entity = Repository.GetSingle(x => x.CertUserID == id);
                if (entity != null)
                {
                    var dto = new CertUserFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new UnauthorizedAccessException("Access denied");
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

        public ResultInfo<CertUserFullDto> Add(CertUserFullDto request)
        {
            var result = new ResultInfo<CertUserFullDto>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                var entity = request.ToEntity();
                entity.DateCreated = DateTime.UtcNow;
                Repository.Add(entity);
                //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new CertUserFullDto(entity);
                SetDtoValues(dto, entity, user);
                result.Result = dto;
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

        public ResultInfo<CertUserFullDto> Update(CertUserFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<CertUserFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CertUserID == request.Id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                    entity = request.ToEntity(entity, fields);
                    Repository.Update(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new CertUserFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("CertUser not found");
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
            //Perform input validation
            //---- 
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.CertUserID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("CertUser not found");
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

        public ResultInfo<CertUserBaseDto> Assign(long id)
        {
            var result = new ResultInfo<CertUserBaseDto>();

            try
            {
                var entity = Repository.GetSingle(x => x.CertUserID == id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                    entity.AssignedToID = user.UserID;
                    Repository.Commit();
                    Repository.Refresh(entity);
                    CertUserBaseDto dto = new CertUserFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("CertUser not found");
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

        public ResultInfo<CertUserBaseDto> Certify(long id, string password)
        {
            var result = new ResultInfo<CertUserBaseDto>();

            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                //Validate Password
                if (!UserHelper.ValidatePassword(aspUser, password))
                    throw new Exception("Invalid Password.");

                var entity = Repository.GetSingle(x => x.CertUserID == id);
                if (entity != null)
                {
                    var cUser = UsersRepository.GetSingle(x => x.AspUserID == new Guid(Convert.ToString(aspUser.UserId)));
                    entity.CertifiedByID = cUser.UserID;
                    entity.IsCertified = true;
                    entity.DateofCertification = DateTime.UtcNow;
                    entity.AssignedToID = null;

                    var uploads = Repository.Context.CERT_UploadInfos.Where(x => x.IsActive && x.CertUserID == id).ToList();
                    uploads.ForEach(x => x.IsCertified = true);

                    var cRecord = AuditRecordsRepository.AddRecord("CertificationCompleted");
                    cRecord.CertUserID = entity.CertUserID;
                    cRecord.TrialID = entity.CONTACTUserTrial.TrialID;

                    Repository.Commit();
                    Repository.Refresh(entity);
                    CertUserBaseDto dto = new CertUserFullDto(entity);

                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;

                    NotificationsHelper.NotifyTechnicianCertification(user.UserID, entity);
                }
                else
                {
                    throw new Exception("CertUser not found");
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

        public ResultInfo<CertUserBaseDto> Reject(long id, string password, string reason)
        {
            var result = new ResultInfo<CertUserBaseDto>();

            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                //Validate Password
                if (!UserHelper.ValidatePassword(aspUser, password))
                    throw new Exception("Invalid Password.");

                var entity = Repository.GetSingle(x => x.CertUserID == id);
                if (entity != null)
                {
                    entity.CertifiedByID = null;
                    entity.IsCertified = false;
                    entity.DateofCertification = null;
                    //entity.AssignedToID = null;

                    var uploads = Repository.Context.CERT_UploadInfos.Where(x => x.IsActive && x.CertUserID == id).ToList();
                    uploads.ForEach(x =>
                    {
                        x.IsCertified = false;
                        x.IsActive = false;
                    });

                    var cRecord = AuditRecordsRepository.AddRecord("CertificationSubmissionRejected");
                    cRecord.CertUserID = entity.CertUserID;
                    cRecord.TrialID = entity.CONTACTUserTrial.TrialID;

                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    var shouldFilterByAffiliation = false;
                    switch (user.AspnetRole.LoweredRoleName)
                    {
                        case "super user":
                        case "data quality evaluator":
                            shouldFilterByAffiliation = true;
                            break;
                    }

                    //Find opened query
                    var pqueries = Repository.Context.QRY_Queries.Where(x => x.IsActive && !x.IsResolved && x.CertUserID == entity.CertUserID);
                    QRY_Query query = null;
                    if (pqueries.Count() > 0)
                    {
                        query = pqueries.FirstOrDefault(x => x.Sender.AffiliationID == user.AffiliationID);
                        if (query == null && !shouldFilterByAffiliation)
                        {
                            query = pqueries.FirstOrDefault();
                        }
                    }

                    var qstatus = Repository.Context.QRY_Status.FirstOrDefault(x => x.StatusName == "Pending Response");

                    if (query == null)
                    {
                        //Create new query
                        query = new QRY_Query()
                        {
                            TrialID = entity.CONTACTUserTrial.TrialID,
                            CertUserID = entity.CertUserID,
                            InitiateDate = DateTime.UtcNow,
                            SenderID = user.UserID,
                            IsActive = true,
                            IsResolved = false,
                            Subject = "Certification Rejected",
                            StatusID = qstatus?.StatusID
                        };
                        Repository.Context.Add(query);
                    }

                    //Add message
                    var qmsg = new QRY_Message()
                    {
                        UserID = user.UserID,
                        DateCreated = DateTime.UtcNow,
                        MessageBody = reason,
                        IsActive = true
                    };

                    Repository.Context.Add(qmsg);
                    qmsg.QRYQuery = query;

                    Repository.Commit();
                    Repository.Refresh(entity);
                    CertUserBaseDto dto = new CertUserFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;

                    Repository.Refresh(qmsg);
                    NotificationsHelper.NotifyNewQueryMessage(qmsg);
                }
                else
                {
                    throw new Exception("CertUser not found");
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

        public ResultInfo<IList<CertUserBaseDto>> GetPrevCertifications(long id, BaseRequestDto request)
        {
            var result = new ResultInfo<IList<CertUserBaseDto>>();
            try
            {
                var certUser = Repository.GetSingle(x => x.CertUserID == id);
                if (certUser != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                    var entities = Repository.GetPrevCertifications(certUser, user, request.Search, request.Sort);
                    var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return entities.Count();
                    });
                    result.SetPager(count, request.Page, request.PageSize);
                    var entitiesPaged = GeneralHelper.GetPagedList(entities, result.Pager);
                    var certUserListResponse = new List<CertUserBaseDto>();
                    if (entitiesPaged != null)
                    {
                        foreach (var entity in entitiesPaged)
                        {
                            var dto = new CertUserBaseDto(entity);
                            SetDtoValues(dto, entity, user);
                            certUserListResponse.Add(dto);
                        }
                    }
                    result.Result = certUserListResponse;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("CertUser not found");
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