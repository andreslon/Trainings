using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using Excelsior.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class AuthUserGateway : IAuthUserGateway
    {
        public IAuthUserRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public AuthUserGateway(IAuthUserRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }
        public ResultInfo<IList<AuthUserBaseDto>> GetAll(AuthUsersRequestDto request)
        {
            var result = new ResultInfo<IList<AuthUserBaseDto>>();
            try
            {
                var templatesResult = new List<AuthUserBaseDto>();
                var templates = Repository.GetAll();
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return templates.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var templatesPaged = GeneralHelper.GetPagedList(templates.OrderBy(x => x.UserId), result.Pager);
                if (templatesPaged != null)
                {
                    foreach (var entity in templatesPaged)
                    {
                        var dto = new AuthUserBaseDto(entity);
                        templatesResult.Add(dto);
                    }
                }

                result.Result = templatesResult;
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
        public ResultInfo<AuthUserFullDto> GetSingle(long id)
        {
            throw new NotImplementedException();
        }
        public ResultInfo<AuthUserFullDto> GetSingle(Guid id)
        {
            var result = new ResultInfo<AuthUserFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.UserId == id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new AuthUserFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User does not exists");
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
        public ResultInfo<AuthUserFullDto> GetByUserName(string userName)
        {
            var result = new ResultInfo<AuthUserFullDto>();
            try
            {
                var entity = Repository.GetByUserName(userName);

                if (entity != null)
                {

                    //Convert to Dto
                    var dto = new AuthUserFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User does not exists");
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
        public ResultInfo<string> DecryptUserName(string cryptedcode)
        {
            var result = new ResultInfo<string>();
            try
            {
                var userName = Infrastructure.Utilities.Security.DecryptShort(cryptedcode);

                if (!userName.Equals(cryptedcode))
                {
                    result.Result = userName;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Crypted code is incorrect");
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
        public ResultInfo<AuthRecoveryDataBaseDto> GetRecoveryDataByUserName(string userName)
        {
            var result = new ResultInfo<AuthRecoveryDataBaseDto>();
            try
            {
                var entity = Repository.GetByUserName(userName);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new AuthRecoveryDataBaseDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User does not exists");
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
        public ResultInfo<bool> RecoveryDataByEmail(string email)
        {
            var result = new ResultInfo<bool>();
            try
            {
                //var entities = Repository.GetByEmail(email);

                //if (entities != null)
                //{
                //    var users = new List<string>();
                //    foreach (var entity in entities)
                //    {
                //        users.Add(entity.UserName);
                //    }
                NotificationsHelper.SendForgotUserNameEmail(email);
                result.Result = true;
                result.IsSuccess = true;
                //}
                //else
                //{
                //    throw new Exception("Email does not exists");
                //}
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
        public ResultInfo<bool> ForgotAnswer(string username)
        {
            var result = new ResultInfo<bool>();
            try
            {
                //var entity = Repository.GetByUserName(username);

                // if (entity != null)
                //{
                //var email = entity.Email;
                NotificationsHelper.SendForgotAnswerEmail(username);
                result.Result = true;
                result.IsSuccess = true;
                //}
                //else
                //{
                //    throw new Exception("User name does not exists");
                //}
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
        public ResultInfo<bool> ResetPassword(AuthRecoveryDataFullDto request)
        {
            var result = new ResultInfo<bool>();

            try
            {
                if (string.IsNullOrWhiteSpace(request.Answer))
                {
                    throw new Exception("The Answer field is required.");
                }
                var entity = Repository.GetByUserName(request.UserName);
                if (entity == null)
                {
                    throw new Exception("User does not exists");
                }
                var AnswerEncrypted = Infrastructure.Utilities.Security.EncodePassword(request.Answer?.Trim().ToLower(), entity.PasswordFormat, entity.PasswordSalt);
                if (AnswerEncrypted != entity.PasswordAnswer)
                {
                    throw new Exception("Invalid answer");
                }
                // If the values are different, then add a validation error with both members specified
                if (request.NewPassword != request.PasswordConfirmation)
                {
                    throw new Exception("Passwords do not match");
                }

                var newPassword = Infrastructure.Utilities.Security.EncodePassword(request.NewPassword, entity.PasswordFormat, entity.PasswordSalt);

                if (entity.Password == newPassword)
                {
                    throw new Exception("Please use a different password");
                }

                entity.Password = newPassword;
                entity.LastPasswordChangedDate = DateTime.UtcNow;
                entity.IsLockedOut = false;
                entity.FailedPasswordAttemptCount = 0;
                entity.FailedPasswordAttemptWindowStart = (DateTime)SqlDateTime.MinValue;
                entity.FailedPasswordAnswerAttemptCount = 0;
                entity.FailedPasswordAnswerAttemptWindowStart = (DateTime)SqlDateTime.MinValue;
                entity.LastLockoutDate = (DateTime)SqlDateTime.MinValue;

                Repository.Update(entity);
                var record = AuditRecordsRepository.AddRecord("UserPasswordChanged",
                    entity.CONTACT_Users?.FirstOrDefault().UserID);
                record.PerformedDateTime = DateTime.UtcNow;

                Repository.Commit();
                Repository.Refresh(entity);


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
        public ResultInfo<bool> ChangePassword(AuthRecoveryDataFullDto request)
        {
            var result = new ResultInfo<bool>();

            try
            {
                var entity = Repository.GetByUserName(request.UserName);
                if (entity == null)
                {
                    throw new Exception("User does not exist");
                }
                // If the values are different, then add a validation error with both members specified
                if (request.NewPassword != request.PasswordConfirmation)
                {
                    throw new Exception("Passwords do not match");
                }

                var newPassword = Infrastructure.Utilities.Security.EncodePassword(request.NewPassword, entity.PasswordFormat, entity.PasswordSalt);

                if (entity.Password == newPassword)
                {
                    throw new Exception("Please use a different password");
                }

                entity.Password = newPassword;
                entity.LastPasswordChangedDate = DateTime.UtcNow;
                Repository.Update(entity);
                var record = AuditRecordsRepository.AddRecord("UserPasswordChanged",
                    entity.CONTACT_Users?.FirstOrDefault().UserID);
                record.PerformedDateTime = DateTime.UtcNow;
                Repository.Commit();
                Repository.Refresh(entity);

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
        public ResultInfo<AuthUserFullDto> Add(AuthUserFullDto request)
        {
            var result = new ResultInfo<AuthUserFullDto>();
            try
            {
                var entity = request.ToEntity();

                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new AuthUserFullDto(entity);
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
        public ResultInfo<AuthUserFullDto> Update(AuthUserFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<AuthUserFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.UserId == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity, fields);
                    Repository.Update(entity);

                    var userEntity = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == request.Id);
                    if (userEntity != null)
                    {
                        userEntity.Email = entity.Email;
                        userEntity.LoweredEmail = entity.LoweredEmail;
                    }

                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new AuthUserFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User does not exists");
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
            throw new NotImplementedException();
        }
        public ResultInfo<bool> Delete(Guid id)
        {
            //Perform input validation
            //---- 
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.UserId == id);
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
                    throw new Exception("User does not exists");
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

        public ResultInfo<bool> Logout()
        {
            var result = new ResultInfo<bool>();
            try
            { 
                var record = AuditRecordsRepository.AddRecord("UserLogOut");
                Repository.Commit();
                result.IsSuccess = true;
                result.Result = false;
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