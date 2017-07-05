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

    public class EquipmentModelGateway : IEquipmentModelGateway
    {
        public IEquipmentModelRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public EquipmentModelGateway(IEquipmentModelRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        public ResultInfo<IList<EquipmentModelBaseDto>> GetAll(EquipmentModelRequestDto request)
        {
            var result = new ResultInfo<IList<EquipmentModelBaseDto>>();
            try
            {
                var equipmentModelListResponse = new List<EquipmentModelBaseDto>();
                IQueryable<CONTACT_EquipmentModel> tpListItems = Repository.GetAll(request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return tpListItems.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var tpListItemsPaged = GeneralHelper.GetPagedList(tpListItems.OrderBy(x => x.EquipmentModelID), result.Pager);
                if (tpListItemsPaged != null)
                {
                    foreach (var tpList in tpListItemsPaged)
                    {
                        var dto = new EquipmentModelBaseDto(tpList);
                        equipmentModelListResponse.Add(dto);
                    }
                }

                result.Result = equipmentModelListResponse;
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

        public ResultInfo<EquipmentModelFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<EquipmentModelFullDto>();
            try
            {
                var equipmentModel = Repository.GetSingle(x => x.EquipmentModelID == id);
                if (equipmentModel != null)
                {
                    var dto = new EquipmentModelFullDto(equipmentModel);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Equipment Model not found");
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

        public ResultInfo<EquipmentModelFullDto> Add(EquipmentModelFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<EquipmentModelFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new EquipmentModelFullDto(entity);
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

        public ResultInfo<EquipmentModelFullDto> Update(EquipmentModelFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<EquipmentModelFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.EquipmentModelID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new EquipmentModelFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Equipment Model not found");
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
                var entity = Repository.GetSingle(x => x.EquipmentModelID == id);
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
                    throw new Exception("Equipment Model not found");
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
