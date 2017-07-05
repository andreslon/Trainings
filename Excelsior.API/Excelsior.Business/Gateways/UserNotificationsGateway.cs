using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.Gateways.Interfaces;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using Excelsior.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.Gateways
{
    public class UserNotificationsGateway : IUserNotificationsGateway
    {
        public IUserNotificationsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public UserNotificationsGateway(IUserNotificationsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }
        public ResultInfo<IList<NotificationBaseDto>> GetNotifications()
        {
            var result = new ResultInfo<IList<NotificationBaseDto>>();
            try
            {
                var respose = new List<NotificationBaseDto>();
                var entities = Repository.GetNotifications();
                foreach (var entity in entities)
                {
                    var dto = new NotificationBaseDto(entity);
                    respose.Add(dto);
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
        public ResultInfo<IList<UserNotificationBaseDto>> GetAll(int userId)
        {
            var result = new ResultInfo<IList<UserNotificationBaseDto>>();
            try
            {
                var respose = new List<UserNotificationBaseDto>();
                var entities = Repository.FindBy(x=> x.UserID== userId);
                foreach (var entity in entities)
                {
                    var dto = new UserNotificationBaseDto(entity);
                    respose.Add(dto);
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
        public ResultInfo<bool> Update(int userId,List<UserNotificationFullDto> request)
        {
            var result = new ResultInfo<bool>();
            try
            {
                var entities = Repository.FindBy(x => x.UserID == userId);
                foreach (var entity in entities)
                {
                    Repository.Delete(entity);
                }
                foreach (var notification in request)
                {
                    notification.UserId = userId;
                   var newEntity = notification.ToEntity();
                    Repository.Add(newEntity);
                } 
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit(); 
                result.Result = true;
                result.IsSuccess = true;
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
