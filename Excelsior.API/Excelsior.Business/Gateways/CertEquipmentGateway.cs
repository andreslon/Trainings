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
    public class CertEquipmentGateway : ICertEquipmentGateway
    {
        public ICertEquipmentRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        private IAuthUserRepository AuthRepository { get; set; }
        public IUsersRepository UsersRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }
        public CertEquipmentGateway(ICertEquipmentRepository repository, IAuditRecordsRepository auditRecordsRepository, IAuthUserRepository authRepository, IUsersRepository usersRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            AuthRepository = authRepository;
            UsersRepository = usersRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        private void SetDtoValues(CertEquipmentBaseDto dto, CERT_Equipment entity, CONTACT_User user)
        {
            dto.TotalUploads = Repository.GetTotalUploads(entity.CertEquipmentID);
            dto.TotalQueriesPending = Repository.GetTotalQueriesPending(entity);
            dto.TotalQueriesFlagged = Repository.GetTotalQueriesFlagged(entity, user);

            dto.hasPrevCert = Repository.GetPrevCertifications(entity, user).Any();
            dto.LastSubmissionDate = Repository.Context.CERT_UploadInfos.LastOrDefault(x => x.IsActive && x.CertEquipmentID == entity.CertEquipmentID)?.UploadDate;
            dto.LastSubmissionDate = Repository.Context.PACS_Series.Where(s => s.IsActive && s.PACSTimePoint.PACSSubject.PACSSite.TrialID == entity.TrialID && s.EquipmentID == entity.EquipmentID && s.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == entity.ImgProcedureID).OrderByDescending(s => s.StudyDate).FirstOrDefault()?.StudyDate;
        }

        public ResultInfo<IList<CertEquipmentBaseDto>> GetAll(CertEquipmentRequestDto request)
        {
            var result = new ResultInfo<IList<CertEquipmentBaseDto>>();
            try
            {
                var certEquipmentListResponse = new List<CertEquipmentBaseDto>();
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var entities = Repository.GetAll(user, request.StudyId, request.AffiliationId, request.EquipmentId, request.ProcedureId, request.IsActive, request.IsCertified, request.HasPrevCert, request.AssignedTo, request.Search, request.Sort);
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
                        var dto = new CertEquipmentBaseDto(entity);
                        SetDtoValues(dto, entity, user);
                        certEquipmentListResponse.Add(dto);
                    }
                }

                result.Result = certEquipmentListResponse;
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

        public ResultInfo<CertEquipmentFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<CertEquipmentFullDto>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var certEqu = Repository.Context.CERT_Equipments.FirstOrDefault(x => x.CertEquipmentID == id);
                if (certEqu == null)
                    throw new Exception("Cert Equipment not found");

                var studyId = certEqu.TrialID;
                PACS_Trial study = null;
                switch (user.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                    case "project manager":
                        study = Repository.Context.PACS_Trials.FirstOrDefault(t => t.TrialID == studyId);
                        break;
                    default:
                        study = Repository.Context.CONTACT_UserTrials.FirstOrDefault(t => t.TrialID == studyId && t.UserID == user.UserID)?.PACSTrial;
                        break;
                }
                
                if (study == null)
                    throw new UnauthorizedAccessException("Access denied");

                var hasAccess = false;
                var affiliationId = certEqu.CONTACTEquipment.AffiliationID;

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

                if(!hasAccess)
                    throw new UnauthorizedAccessException("Access denied");


                var equipments = Repository.GetAll(user, studyId, null, null, null, null, null, null, null, null, null);

                var entity = equipments.FirstOrDefault(x => x.CertEquipmentID == id);

                //var entity = Repository.GetSingle(x => x.CertEquipmentID == id);
                if (entity != null)
                {
                    var dto = new CertEquipmentFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                    throw new UnauthorizedAccessException("Access denied");
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

        public ResultInfo<CertEquipmentFullDto> Add(CertEquipmentFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<CertEquipmentFullDto>();
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
                var dto = new CertEquipmentFullDto(entity);
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

        public ResultInfo<CertEquipmentFullDto> Update(CertEquipmentFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<CertEquipmentFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CertEquipmentID == request.Id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                    entity = request.ToEntity(entity, fields);
                    Repository.Update(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new CertEquipmentFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("CertEquipment not found");
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
                var entity = Repository.GetSingle(x => x.CertEquipmentID == id);
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
                    throw new Exception("CertEquipment not found");
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

        public ResultInfo<CertEquipmentBaseDto> Assign(long id)
        {
            var result = new ResultInfo<CertEquipmentBaseDto>();

            try
            {
                var entity = Repository.GetSingle(x => x.CertEquipmentID == id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                    entity.AssignedToID = user.UserID;
                    Repository.Commit();
                    Repository.Refresh(entity);
                    CertEquipmentBaseDto dto = new CertEquipmentFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("CertEquipment not found");
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

        public ResultInfo<CertEquipmentBaseDto> Certify(long id, CertifyEquipmentRequestDto request, string password)
        {
            var result = new ResultInfo<CertEquipmentBaseDto>();

            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                //Validate Password
                if (!UserHelper.ValidatePassword(aspUser, password))
                    throw new Exception("Invalid Password.");

                var entity = Repository.GetSingle(x => x.CertEquipmentID == id);
                if (entity != null)
                {
                    var cUser = UsersRepository.GetSingle(x => x.AspUserID == new Guid(Convert.ToString(aspUser.UserId)));
                    entity.CertifiedByID = cUser.UserID;
                    entity.IsCertified = true;
                    entity.DateofCertification = DateTime.UtcNow;
                    entity.AssignedToID = null;

                    if (request != null)
                    {
                        if (request.PixelSpacingX != null)
                        {
                            entity.PixelSpacingX = request.PixelSpacingX;
                            if (request.PixelSpacingY == null)
                                entity.PixelSpacingY = request.PixelSpacingX;
                        }
                        if (request.PixelSpacingY != null)
                        {
                            entity.PixelSpacingY = request.PixelSpacingY;
                            if (request.PixelSpacingX == null)
                                entity.PixelSpacingX = request.PixelSpacingY;
                        }
                    }

                    var uploads = Repository.Context.CERT_UploadInfos.Where(x => x.IsActive && x.CertEquipmentID == id).ToList();
                    uploads.ForEach(x => x.IsCertified = true);

                    var cRecord = AuditRecordsRepository.AddRecord("CertificationCompleted");
                    cRecord.CertEquipmentID = entity.CertEquipmentID;
                    cRecord.TrialID = entity.TrialID;

                    Repository.Commit();
                    Repository.Refresh(entity);
                    CertEquipmentBaseDto dto = new CertEquipmentFullDto(entity);

                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;

                    NotificationsHelper.NotifyEquipmentCertification(user.UserID, entity);
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

        public ResultInfo<CertEquipmentBaseDto> Reject(long id, RejectCertificationRequestDto request, string password, string reason)
        {
            var result = new ResultInfo<CertEquipmentBaseDto>();

            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                //Validate Password
                if (!UserHelper.ValidatePassword(aspUser, password))
                    throw new Exception("Invalid Password.");

                var entity = Repository.GetSingle(x => x.CertEquipmentID == id);
                if (entity != null)
                {
                    entity.CertifiedByID = null;
                    entity.IsCertified = false;
                    entity.DateofCertification = null;
                    //entity.AssignedToID = null;

                    var cRecord = AuditRecordsRepository.AddRecord("CertificationSubmissionRejected");
                    cRecord.CertEquipmentID = entity.CertEquipmentID;
                    cRecord.TrialID = entity.TrialID;

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
                    var pqueries = Repository.Context.QRY_Queries.Where(x => x.IsActive && !x.IsResolved && x.CertEquipmentID == entity.CertEquipmentID);
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
                            TrialID = entity.TrialID,
                            CertEquipmentID = entity.CertEquipmentID,
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


                    List<CERT_UploadInfo> uploadEntities = null;
                    if (request != null)
                    {
                        if (request.Uploads != null && request.Uploads.Count > 0)
                        {
                            var uploadsString = request.Uploads.Select(x => string.Format("[{0}]", x));
                            uploadEntities = Repository.Context.CERT_UploadInfos.Where(x => x.IsActive && x.CertEquipmentID == id && uploadsString.Contains("[" + x.CertUploadInfoID.ToString() + "]")).ToList();
                        }
                    }
                    if(uploadEntities == null)
                    {
                        uploadEntities = Repository.Context.CERT_UploadInfos.Where(x => x.IsActive && x.CertEquipmentID == id).ToList();
                    }
                    uploadEntities.ForEach(x =>
                    {
                        x.IsCertified = false;
                        x.IsActive = false;
                        x.QRYQuery = query;
                    });

                    Repository.Commit();
                    Repository.Refresh(entity);
                    CertEquipmentBaseDto dto = new CertEquipmentFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;

                    Repository.Refresh(qmsg);
                    NotificationsHelper.NotifyNewQueryMessage(qmsg);
                }
                else
                {
                    throw new Exception("CertEquipment not found");
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

        public ResultInfo<IList<CertEquipmentBaseDto>> GetPrevCertifications(long id, BaseRequestDto request)
        {
            var result = new ResultInfo<IList<CertEquipmentBaseDto>>();
            try
            {
                var certEquipment = Repository.GetSingle(x => x.CertEquipmentID == id);
                if (certEquipment != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                    var entities = Repository.GetPrevCertifications(certEquipment, user, request.Search, request.Sort);
                    var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return entities.Count();
                    });
                    result.SetPager(count, request.Page, request.PageSize);
                    var entitiesPaged = GeneralHelper.GetPagedList(entities, result.Pager);
                    var certEquipmentListResponse = new List<CertEquipmentBaseDto>();
                    if (entitiesPaged != null)
                    {
                        foreach (var entity in entitiesPaged)
                        {
                            var dto = new CertEquipmentBaseDto(entity);
                            SetDtoValues(dto, entity, user);
                            certEquipmentListResponse.Add(dto);
                        }
                    }
                    result.Result = certEquipmentListResponse;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("CertEquipment not found");
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
