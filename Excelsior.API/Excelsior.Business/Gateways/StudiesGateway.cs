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
    public class StudiesGateway : IStudiesGateway
    {
        public IStudiesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        private IAuthUserRepository AuthRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }

        public StudiesGateway(IStudiesRepository repository, IAuditRecordsRepository auditRecordsRepository, IAuthUserRepository authRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            AuthRepository = authRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        private void SetDtoValues(StudyBaseDto dto, PACS_Trial entity, CONTACT_User user)
        {
            dto.TotalSubjects = Repository.GetTotalSubjects(entity);
            dto.TotalQueriesPending = Repository.GetTotalQueriesPending(entity);
            dto.TotalQueriesFlagged = Repository.GetTotalQueriesFlagged(entity, user);
        }

        public ResultInfo<IList<StudyBaseDto>> GetAll(StudiesRequestDto request)
        {
            var result = new ResultInfo<IList<StudyBaseDto>>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var studies = new List<StudyBaseDto>();
                var trials = Repository.GetAll(request.UserId, request.IsActive, request.IsLocked, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return trials.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var trialsPaged = GeneralHelper.GetPagedList(trials.OrderBy(x => x.TrialName), result.Pager);
                if (trialsPaged != null)
                {
                    foreach (var entity in trialsPaged)
                    {
                        var dto = new StudyBaseDto(entity);
                        SetDtoValues(dto, entity, user);
                        studies.Add(dto);
                    }
                }

                result.Result = studies;
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

        public ResultInfo<StudyFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<StudyFullDto>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());                

                var studies = Repository.GetAll(aspUserId.ToString(), null, null, null);

                var entity = studies.FirstOrDefault(x => x.TrialID == id);

                //var entity = Repository.GetSingle(x => x.TrialID == id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new StudyFullDto(entity);

                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                    SetDtoValues(dto, entity, user);

                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Study not found");
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

        public ResultInfo<StudyFullDto> Add(StudyFullDto request)
        {
            var result = new ResultInfo<StudyFullDto>();
            try
            {
                var entity = request.ToEntity();
                entity.IsActive = true;
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new StudyFullDto(entity);

                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

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

        public ResultInfo<StudyFullDto> Update(StudyFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<StudyFullDto>();
            try
            {
                if (!string.IsNullOrEmpty(password))
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                    //Validate Password
                    if (!UserHelper.ValidatePassword(aspUser, password))
                        throw new Exception("Invalid password.");
                }

                var entity = Repository.GetSingle(x => x.TrialID == request.Id);
                if (entity != null)
                {
                    var oldDto = new StudyFullDto(entity);
                    entity = request.ToEntity(entity, fields);
                    var newDto = new StudyFullDto(entity);
                    var changes = ChangeSetHelper.GetPropertiesChangeInfo(newDto, oldDto, fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("TrialEdited");
                    record.TrialID = entity.TrialID;
                    if (!string.IsNullOrEmpty(reason))
                        record.ReasonForChange = reason;
                    if (changes.Count > 0)
                        record.DetailsXML = ChangeSetHelper.ToXML(ChangeSetHelper.CreateEntityChangeList("Trial", "Update", changes));
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new StudyFullDto(entity);

                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Study not found");
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
                var entity = Repository.GetSingle(x => x.TrialID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Study not found");
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

        public ResultInfo<StudyFullDto> SetIsLocked(long id, bool isLocked, string password, string reason)
        {
            var result = new ResultInfo<StudyFullDto>();
            try
            {
                if (!string.IsNullOrEmpty(password))
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                    //Validate Password
                    if (!UserHelper.ValidatePassword(aspUser, password))
                        throw new Exception("Invalid password.");
                }

                var entity = Repository.GetSingle(x => x.TrialID == id);
                if (entity != null)
                {
                    AUDIT_Record record;
                    if (isLocked)
                    {
                        entity.IsLocked = true;
                        entity.TrialLockedDate = DateTime.UtcNow;
                        record = AuditRecordsRepository.AddRecord("TrialDataLocked");
                    }
                    else
                    {
                        entity.IsLocked = false;
                        entity.TrialLockedDate = null;
                        record = AuditRecordsRepository.AddRecord("TrialDataUnlocked");
                    }

                    record.TrialID = id;
                    if (!string.IsNullOrEmpty(reason))
                        record.ReasonForChange = reason;

                    Repository.Context.SaveChanges();

                    Repository.Refresh(entity);
                    var dto = new StudyFullDto(entity);

                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Study not found");
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

        public ResultInfo<IList<ProcedureFullDto>> GetProcedures(long id)
        {
            var result = new ResultInfo<IList<ProcedureFullDto>>();
            try
            {
                var entities = Repository.GetProcedures(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new ProcedureFullDto(x, new StudyBaseDto())).ToList();
                });
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

        public ResultInfo<IList<ProcedureFullDto>> SetProcedures(long id, IList<ProcedureFullDto> procedures)
        {
            var result = new ResultInfo<IList<ProcedureFullDto>>();
            try
            {
                var entity = Repository.GetSingle(x => x.TrialID == id);
                if (entity != null)
                {
                    //check existing steps
                    AttachProcedures(entity, procedures);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = entity.CERT_ImgProcedureLists.Select(x => new ProcedureFullDto(x)).ToList();
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Study not found");
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

        public ResultInfo<IList<ProcedureFullDto>> AddProcedures(long id, IList<ProcedureFullDto> procedures)
        {
            var result = new ResultInfo<IList<ProcedureFullDto>>();
            try
            {
                var entity = Repository.GetSingle(x => x.TrialID == id);
                if (entity != null)
                {
                    //check existing steps
                    AddProcedures(entity, procedures);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = entity.CERT_ImgProcedureLists.Select(x => new ProcedureFullDto(x)).ToList();
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Study not found");
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

        public ResultInfo<IList<ProcedureFullDto>> RemoveProcedures(long id, IList<ProcedureFullDto> procedures)
        {
            var result = new ResultInfo<IList<ProcedureFullDto>>();
            try
            {
                var entity = Repository.GetSingle(x => x.TrialID == id);
                if (entity != null)
                {
                    //check existing steps
                    RemoveProcedures(entity, procedures);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = entity.CERT_ImgProcedureLists.Select(x => new ProcedureFullDto(x)).ToList();
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Study not found");
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

        private void AttachProcedures(PACS_Trial entity, IList<ProcedureFullDto> procedures)
        {
            var procEntities = RefreshOrRemoveExistingProcedures(entity, procedures);
            foreach (var procedure in procEntities)
            {
                var tagEntity = Repository.AddProcedure(entity, procedure);
            }
        }

        private void AddProcedures(PACS_Trial entity, IList<ProcedureFullDto> procedures)
        {
            var procEntities = RefreshExistingProcedures(entity, procedures);
            foreach (var procedure in procEntities)
            {
                var tagEntity = Repository.AddProcedure(entity, procedure);
            }
        }

        private void RemoveProcedures(PACS_Trial entity, IList<ProcedureFullDto> procedures)
        {
            var entityProcs = entity.CERT_ImgProcedureLists.ToList();
            foreach (var proc in entityProcs)
            {
                if (procedures.Any(x => x.Id == proc.ImgProcedureID))
                    entity.CERT_ImgProcedureLists.Remove(proc);
            }
        }

        private IList<CERT_ImgProcedureList> RefreshOrRemoveExistingProcedures(PACS_Trial entity, IList<ProcedureFullDto> procedures)
        {
            var entityProcs = entity.CERT_ImgProcedureLists.ToList();
            foreach (var proc in entityProcs)
            {
                if (!procedures.Any(x => x.Id == proc.ImgProcedureID))
                    entity.CERT_ImgProcedureLists.Remove(proc);
            }

            var procEntities = new List<CERT_ImgProcedureList>();
            foreach (var proc in procedures)
            {
                if (proc.Id <= 0)
                    continue;

                var entityProc = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.Context.CERT_ImgProcedureLists.FirstOrDefault(x => x.ImgProcedureID == proc.Id);
                });
                if (entityProc != null)
                {
                    procEntities.Add(entityProc);
                }
            }
            return procEntities;
        }

        private IList<CERT_ImgProcedureList> RefreshExistingProcedures(PACS_Trial entity, IList<ProcedureFullDto> procedures)
        {
            var procEntities = new List<CERT_ImgProcedureList>();
            foreach (var proc in procedures)
            {
                if (proc.Id <= 0)
                    continue;

                var entityProc = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.Context.CERT_ImgProcedureLists.FirstOrDefault(x => x.ImgProcedureID == proc.Id);
                });
                if (entityProc != null)
                {
                    procEntities.Add(entityProc);
                }
            }
            return procEntities;
        }
    }
}