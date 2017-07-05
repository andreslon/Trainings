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
    public class SitesGateway : ISitesGateway
    {
        public ISitesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        private IAuthUserRepository AuthRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }

        public SitesGateway(ISitesRepository repository, IAuditRecordsRepository auditRecordsRepository, IAuthUserRepository authRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            AuthRepository = authRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        private void SetDtoValues(SiteBaseDto dto, PACS_Site entity, CONTACT_User user)
        {
            dto.TotalSubjects = Repository.GetTotalSubjects(entity);
            dto.TotalQueriesPending = Repository.GetTotalQueriesPending(entity);
            dto.TotalQueriesFlagged = Repository.GetTotalQueriesFlagged(entity, user);
        }

        public ResultInfo<IList<SiteBaseDto>> GetAll(SitesRequestDto request)
        {
            var result = new ResultInfo<IList<SiteBaseDto>>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var sitesRespose = new List<SiteBaseDto>();
                var entities = Repository.GetAll(user, request.StudyId, request.IsActive, request.Search, request.Sort);
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
                        var dto = new SiteBaseDto(entity);
                        SetDtoValues(dto, entity, user);
                        sitesRespose.Add(dto);
                    }
                }

                result.Result = sitesRespose;
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

        public ResultInfo<SiteFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<SiteFullDto>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var site = Repository.Context.PACS_Sites.FirstOrDefault(x => x.SiteID == id);
                if (site == null)
                    throw new Exception("Site not found");

                var studyId = site.TrialID;
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

                var sites = Repository.GetAll(user, studyId, null, null, null);

                var entity = sites.FirstOrDefault(x => x.SiteID == id);

                //var entity = Repository.GetSingle(x => x.SiteID == id);
                if (entity != null)
                {
                    var dto = new SiteFullDto(entity);
                    result.Result = dto;
                    SetDtoValues(dto, entity, user);
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

        public ResultInfo<SiteFullDto> Add(SiteFullDto request)
        {
            var result = new ResultInfo<SiteFullDto>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var entity = request.ToEntity();
                Repository.Add(entity);
                //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new SiteFullDto(entity);
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

        public ResultInfo<SiteFullDto> Update(SiteFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<SiteFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.SiteID == request.Id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    entity = request.ToEntity(entity, fields);
                    Repository.Update(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new SiteFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Site not found");
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
                var entity = Repository.GetSingle(x => x.SiteID == id);
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
                    throw new Exception("Site not found");
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