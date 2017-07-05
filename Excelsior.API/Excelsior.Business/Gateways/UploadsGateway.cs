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
    public class UploadsGateway : IUploadsGateway
    {
        public IUploadsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public IUsersRepository UsersRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }

        public UploadsGateway(IUploadsRepository repository, IAuditRecordsRepository auditRecordsRepository, IUsersRepository usersRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            UsersRepository = usersRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        private string GetDefaultDataFileLocation(UPLD_UploadInfo entity)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}/Uploads/File_{5}.", entity.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.TrialID, entity.PACSSeries.PACSTimePoint.PACSSubject.SiteID, entity.PACSSeries.PACSTimePoint.SubjectID, entity.PACSSeries.TimePointsID, entity.SeriesID, entity.UploadInfoID);
        }

        public ResultInfo<IList<UploadBaseDto>> GetAll(UploadsRequestDto request)
        {
            var result = new ResultInfo<IList<UploadBaseDto>>();
            try
            {
                var listDto = new List<UploadBaseDto>();
                var listResult = Repository.GetAll(request.SeriesId, request.IsActive);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return listResult.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var listPaged = GeneralHelper.GetPagedList(listResult.OrderBy(x => x.UploadDate), result.Pager);
                if (listPaged != null)
                {
                    foreach (var entity in listPaged)
                    {
                        var dto = new UploadBaseDto(entity, new SeriesBaseDto());
                        if (string.IsNullOrEmpty(dto.DataFileLocation))
                            dto.DataFileLocation = GetDefaultDataFileLocation(entity);
                        listDto.Add(dto);
                    }
                }
                result.Result = listDto;
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

        private void SetMediaStatus(UPLD_UploadInfo entity, string statusName)
        {
            entity.StatusID = Repository.Context.PACS_RawDataStatus.Single(x => x.StatusName == statusName).StatusID;
        }

        public MediaStatusFullDto GetMediaStatus(string statusName)
        {
            var entity = Repository.Context.PACS_RawDataStatus.Single(x => x.StatusName == statusName);
            return new MediaStatusFullDto(entity);
        }

        public ResultInfo<UploadFullDto> Add(UploadFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<UploadFullDto>();
            try
            {
                var UserId = ResourceOwnerData.GetUserId();
                var cUser = UsersRepository.GetSingle(x => x.AspUserID == new Guid(Convert.ToString(UserId)));
                var entity = request.ToEntity();
                SetMediaStatus(entity, "Saved");
                entity.UploaderID = cUser.UserID;
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new UploadFullDto(entity);
                if (string.IsNullOrEmpty(dto.DataFileLocation))
                    dto.DataFileLocation = GetDefaultDataFileLocation(entity);
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

        public ResultInfo<UploadFullDto> Update(UploadFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<UploadFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.UploadInfoID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new UploadFullDto(entity);
                    if (string.IsNullOrEmpty(dto.DataFileLocation))
                        dto.DataFileLocation = GetDefaultDataFileLocation(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Upload not found");
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
                var entity = Repository.GetSingle(x => x.UploadInfoID == id);
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
                    throw new Exception("Upload not found");
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

        public ResultInfo<UploadFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<UploadFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.UploadInfoID == id);
                if (entity != null)
                {
                    var dto = new UploadFullDto(entity);
                    if (string.IsNullOrEmpty(dto.DataFileLocation))
                        dto.DataFileLocation = GetDefaultDataFileLocation(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Upload not found");
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