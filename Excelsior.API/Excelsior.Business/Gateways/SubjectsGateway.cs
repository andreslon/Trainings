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
    public class SubjectsGateway : ISubjectsGateway
    {
        public ISubjectsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }

        public SubjectsGateway(ISubjectsRepository repository, IAuditRecordsRepository auditRecordsRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        public ResultInfo<IList<SubjectBaseDto>> GetAll(SubjectsRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<SubjectBaseDto>>();
            try
            {
                var subjectsRespose = new List<SubjectBaseDto>();
                var subjects = Repository.GetAll(request.UserId, request.StudyId, request.SiteId, request.AffiliationId, request.GroupId, request.CohortId, request.IsActive, request.IsRejected, request.Search, request.Sort);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return subjects.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var subjectsPaged = GeneralHelper.GetPagedList(subjects, result.Pager);
                if (subjectsPaged != null)
                {
                    foreach (var subject in subjectsPaged)
                    {
                        var dto = new SubjectBaseDto(subject);
                        subjectsRespose.Add(dto);
                    }
                }

                result.Result = subjectsRespose;
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

        public ResultInfo<SubjectFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<SubjectFullDto>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var subject = Repository.Context.PACS_Subjects.FirstOrDefault(x => x.SubjectID == id);
                if (subject == null)
                    throw new Exception("Subject not found");

                var studyId = subject.PACSSite.TrialID;
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
                var affiliationId = subject.PACSSite.AffiliationID;

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

                var subjects = Repository.GetAll(aspUserId.ToString(), study.TrialID, null, null, null, null, null, null, null, null);

                var entity = subjects.FirstOrDefault(x => x.SubjectID == id);

                //var entity = Repository.GetSingle(x => x.SubjectID == id);
                if (entity != null)
                {
                    var dto = new SubjectFullDto(entity);
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

        public ResultInfo<SubjectFullDto> Add(SubjectFullDto request)
        {
            var result = new ResultInfo<SubjectFullDto>();

            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("SubjectCreated");
                record.PACSSubject = entity;
                record.TrialID = entity?.PACSSite?.TrialID;
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new SubjectFullDto(entity);
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

        public ResultInfo<SubjectFullDto> Update(SubjectFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<SubjectFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.SubjectID == request.Id);
                if (entity != null)
                {
                    var oldDto = new SubjectFullDto(entity);
                    entity = request.ToEntity(entity, fields);
                    var newDto = new SubjectFullDto(entity);
                    var changes = ChangeSetHelper.GetPropertiesChangeInfo(newDto, oldDto, fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("SubjectEdited");
                    record.SubjectID = entity.SubjectID;
                    record.TrialID = entity?.PACSSite?.TrialID;
                    if (!string.IsNullOrEmpty(reason))
                        record.ReasonForChange = reason;
                    if (changes.Count > 0)
                        record.DetailsXML = ChangeSetHelper.ToXML(ChangeSetHelper.CreateEntityChangeList("Subject", "Update", changes));
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new SubjectFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Subject not found");
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
                var entity = Repository.GetSingle(x => x.SubjectID == id);
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
                    throw new Exception("Subject not found");
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

        public ResultInfo<IList<Dictionary<string, string>>> Valid(SubjectFullDto request)
        {
            var exists = false;

            var site = Repository.Context.PACS_Sites.FirstOrDefault(x => x.SiteID == request.SiteId);

            ResultInfo<IList<Dictionary<string, string>>> result = new ResultInfo<IList<Dictionary<string, string>>>();
            List<Dictionary<string, string>> errors = new List<Dictionary<string, string>>();

            if (!string.IsNullOrWhiteSpace(request.NameCode))
            {
                exists = Repository.Any(x => x.PACSSite.TrialID == site.TrialID && x.SubjectID != request.Id && x.NameCode == request.NameCode);
                if (exists)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("key", "NameCode");
                    dictionary.Add("ErrorMessage", "the value already exists.");
                    errors.Add(dictionary);
                }
            }
            if (!string.IsNullOrWhiteSpace(request.AlternativeRandomizedId))
            {
                exists = Repository.Any(x => x.PACSSite.TrialID == site.TrialID && x.SubjectID != request.Id && x.AlternativeRandomizedSubjectID == request.AlternativeRandomizedId);
                if (exists)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("key", "AlternativeRandomizedId");
                    dictionary.Add("ErrorMessage", "the value already exists.");
                    errors.Add(dictionary);
                }
            }
            if (!string.IsNullOrWhiteSpace(request.RandomizedId))
            {
                exists = Repository.Any(x => x.PACSSite.TrialID == site.TrialID && x.SubjectID != request.Id && x.RandomizedSubjectID == request.RandomizedId);
                if (exists)
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("key", "RandomizedId");
                    dictionary.Add("ErrorMessage", "the value already exists.");
                    errors.Add(dictionary);
                }
            }
            if (errors != null && errors.Count > 0)
            {
                result.Message = "Invalid Model";
                result.Result = errors;
            }
            return result;
        }
    }
}