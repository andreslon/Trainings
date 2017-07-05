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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class StudyUserGateway : IStudyUserGateway
    {
        public IStudyUserRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public StudyUserGateway(IStudyUserRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        public ResultInfo<IList<StudyUserBaseDto>> GetAll(StudyUserRequestDto request)
        {
            var result = new ResultInfo<IList<StudyUserBaseDto>>();
            try
            {
                var userStudyListResponse = new List<StudyUserBaseDto>();

                IQueryable<CONTACT_UserTrial> tpListItems = Repository.GetAll(request.UserId, request.IsActive);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return tpListItems.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var tpListItemsPaged = GeneralHelper.GetPagedList(tpListItems.OrderBy(x => x.UserTrialID), result.Pager);
                if (tpListItemsPaged != null)
                {
                    foreach (var tpList in tpListItemsPaged)
                    {
                        var dto = new StudyUserBaseDto(tpList);
                        userStudyListResponse.Add(dto);
                    }
                }

                result.Result = userStudyListResponse;
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

        public ResultInfo<StudyUserFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<StudyUserFullDto>();
            try
            {
                var userStudy = Repository.GetSingle(x => x.UserTrialID == id);
                if (userStudy != null)
                {
                    var dto = new StudyUserFullDto(userStudy);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User Study not found");
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

        public ResultInfo<StudyUserFullDto> Add(StudyUserFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<StudyUserFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("AssignedTrialUser");
                record.RelatedUserID = entity.UserID;
                record.TrialID = entity.TrialID;
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new StudyUserFullDto(entity);
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

        public ResultInfo<StudyUserFullDto> Update(StudyUserFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<StudyUserFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.UserTrialID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new StudyUserFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User Study not found");
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
                var entity = Repository.GetSingle(x => x.UserTrialID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    var record = AuditRecordsRepository.AddRecord("UnassignedTrialUser");
                    record.RelatedUserID = id;
                    record.TrialID = entity.TrialID;
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User Study not found");
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
