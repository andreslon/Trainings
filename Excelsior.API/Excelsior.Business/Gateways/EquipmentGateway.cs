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
    public class EquipmentGateway : IEquipmentGateway
    {
        public IEquipmentRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public EquipmentGateway(IEquipmentRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        public ResultInfo<IList<EquipmentBaseDto>> GetAll(EquipmentRequestDto request)
        {
            var result = new ResultInfo<IList<EquipmentBaseDto>>();
            try
            {
                var equipmentListResponse = new List<EquipmentBaseDto>();

                IQueryable<CONTACT_Equipment> tpListItems = Repository.GetAll(request.AffiliationId, request.IsActive, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return tpListItems.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var tpListItemsPaged = GeneralHelper.GetPagedList(tpListItems.OrderBy(x => x.EquipmentID), result.Pager);
                if (tpListItemsPaged != null)
                {
                    foreach (var tpList in tpListItemsPaged)
                    {
                        var dto = new EquipmentBaseDto(tpList);
                        equipmentListResponse.Add(dto);
                    }
                }

                result.Result = equipmentListResponse;
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

        public ResultInfo<EquipmentFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<EquipmentFullDto>();
            try
            {
                var equipment = Repository.GetSingle(x => x.EquipmentID == id);
                if (equipment != null)
                {
                    var dto = new EquipmentFullDto(equipment);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Equipment not found");
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

        public ResultInfo<EquipmentFullDto> Add(EquipmentFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<EquipmentFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new EquipmentFullDto(entity);
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

        public ResultInfo<EquipmentFullDto> Update(EquipmentFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<EquipmentFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.EquipmentID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new EquipmentFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Equipment not found");
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
                var entity = Repository.GetSingle(x => x.EquipmentID == id);
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
                    throw new Exception("Equipment not found");
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
