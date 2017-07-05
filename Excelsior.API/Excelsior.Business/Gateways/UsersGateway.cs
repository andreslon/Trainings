using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using Excelsior.Infrastructure.Interfaces;
using Excelsior.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class UsersGateway : IUsersGateway
    {
        public IUsersRepository Repository { get; set; }
        public IAuthUserRepository AuthRepository { get; set; }
        public IAspUserRepository AspUserRepository { get; set; }
        public ISettings Settings { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public UsersGateway(IUsersRepository repository, IAuditRecordsRepository auditRecordsRepository, ISettings settings, IAuthUserRepository authRepository, IAspUserRepository aspUserRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
            Settings = settings;
            AuthRepository = authRepository;
            AspUserRepository = aspUserRepository;
        }

        public ResultInfo<IList<UserBaseDto>> GetAll(UsersRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<UserBaseDto>>();
            try
            {
                var usersResult = new List<UserBaseDto>();
                var users = Repository.GetAll(request.IsActive, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return users.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var usersPaged = GeneralHelper.GetPagedList(users.OrderBy(x => x.LoweredUserName), result.Pager);
                if (usersPaged != null)
                {
                    foreach (var user in usersPaged)
                    {
                        var dto = new UserBaseDto(user);
                        usersResult.Add(dto);
                    }
                }

                result.Result = usersResult;
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

        public ResultInfo<UserFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<UserFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.UserID == id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new UserFullDto(entity);

                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User not found");
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

        public ResultInfo<UserFullDto> GetSingle(Guid id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<UserFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.AspUserID == id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new UserFullDto(entity);

                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User not found");
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

        public ResultInfo<UserFullDto> Add(UserFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<UserFullDto>();
            try
            {
                var entity = request.ToEntity();

                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("UserCreated");
                record.CONTACTUser = entity;
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new UserFullDto(entity);
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

        public ResultInfo<UserFullDto> Update(UserFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<UserFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.UserID == request.Id);
                if (entity != null)
                {
                    var oldDto = new UserFullDto(entity);
                    entity = request.ToEntity(entity, fields);
                    var newDto = new UserFullDto(entity);
                    var changes = ChangeSetHelper.GetPropertiesChangeInfo(newDto, oldDto, fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("UserUpdated");
                    record.UserID = entity.UserID;
                    if (!string.IsNullOrEmpty(reason))
                        record.ReasonForChange = reason;
                    if (changes.Count > 0)
                        record.DetailsXML = ChangeSetHelper.ToXML(ChangeSetHelper.CreateEntityChangeList("User", "Update", changes));
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new UserFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("User not found");
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
                var entity = Repository.GetSingle(x => x.UserID == id);
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
                    throw new Exception("User not found");
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

        public ResultInfo<bool> SendEmail(EmailBaseDto email)
        {
            var result = new ResultInfo<bool>();
            try
            {
                var emailSended = NotificationsHelper.SendEMail(email,
                    Settings.GetSetting("SupportEmail"),
                    Settings.GetSetting("SysEmailAccountName"),
                    Settings.GetSetting("SysEmailAccountPort"),
                    Settings.GetSetting("SysEmailAccountHost"),
                    Settings.GetSetting("SysEmailAccountPwd"));

                if (emailSended)
                {
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Error sending email.");
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

        public ResultInfo<string> DecryptUserId(string cryptedcode)
        {
            var result = new ResultInfo<string>();
            try
            {
                var userId = Infrastructure.Utilities.Security.DecryptShort(cryptedcode);

                if (!userId.Equals(cryptedcode))
                {
                    result.Result = userId;
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

        public ResultInfo<bool> Registration(RegistrationFullDto request, string fields = null)
        {
            var result = new ResultInfo<bool>();
            try
            {
                var userEntity = Repository.GetSingle(x => x.UserID == request.Id);
                if (userEntity == null)
                {
                    throw new Exception("User does not exists");
                }

                var userExists = AuthRepository.GetByUserName(request.UserName);
                if (userExists != null)
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.DuplicateUserName));
                }

                if (!Security.ValidateParameter(request.UserName, true, true, true, 256))
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidUserName));
                }

                if (!Security.ValidateParameter(request.Email, true, true, false, 256))
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidEmail));
                }
                if (!Security.IsValidEmail(request.Email))
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidEmail));
                }

                if (!Security.ValidateParameter(request.PasswordQuestion, true, true, false, 256))
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidQuestion));
                }

                //Password Validations

                if (request.Password != request.PasswordConfirmation)
                {
                    throw new Exception("Passwords do not match.");
                }
                if (request.Password.Length < 7)
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.MinimunPassword));
                }
                else if (request.Password.Length > 50)
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.MaximunPassword));
                }

                int count = 0;

                for (int i = 0; i < request.Password.Length; i++)
                {
                    if (!char.IsLetterOrDigit(request.Password, i))
                    {
                        count++;
                    }
                }
                if (count < 1)
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidFormatPassword));
                }


                if (!Security.ValidateParameter(request.Password, true, true, false, 128))
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidPassword));
                }
                string salt = Security.GenerateSalt();
                string pass = Security.EncodePassword(request.Password, (int)Security.MembershipPasswordFormat.Hashed, salt);
                if (pass.Length > 128)
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidPassword));
                }
                request.Password = pass;

                /////////////////////////

                request.PasswordAnswer = request.PasswordAnswer?.Trim();
                if (!string.IsNullOrEmpty(request.PasswordAnswer))
                {
                    if (request.PasswordAnswer.Length > 128)
                    {
                        throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidAnswer));
                    }
                    request.PasswordAnswer = Security.EncodePassword(request.PasswordAnswer.ToLower(CultureInfo.InvariantCulture), (int)Security.MembershipPasswordFormat.Hashed, salt);
                }

                var aspnetApplication = AspUserRepository.GetCurrentApplication(); 
                
                var aspUserEntity = request.ToMembershipEntity(null, fields);
                //aspUserEntity.UserId = newUserId; 
                aspUserEntity.LastActivityDate = DateTime.UtcNow;
                aspUserEntity.Password = request.Password;
                aspUserEntity.PasswordFormat = (int)Security.MembershipPasswordFormat.Hashed;
                aspUserEntity.PasswordSalt = salt;
                aspUserEntity.Email = request.Email;
                aspUserEntity.LoweredEmail = request.Email?.ToLower();
                aspUserEntity.PasswordQuestion = request.PasswordQuestion;
                aspUserEntity.PasswordAnswer = request.PasswordAnswer;
                aspUserEntity.IsApproved = true;
                aspUserEntity.IsLockedOut = false;
                aspUserEntity.CreateDate = DateTime.UtcNow;
                aspUserEntity.LastLoginDate = DateTime.UtcNow;
                aspUserEntity.LastPasswordChangedDate = DateTime.UtcNow;
                aspUserEntity.LastLockoutDate = DateTime.UtcNow;
                aspUserEntity.FailedPasswordAttemptWindowStart = DateTime.UtcNow;
                aspUserEntity.FailedPasswordAnswerAttemptWindowStart = DateTime.UtcNow;
                aspUserEntity.FailedPasswordAttemptCount = 0;
                aspUserEntity.FailedPasswordAnswerAttemptCount = 0;

                AspUserRepository.Add(aspUserEntity);
                aspUserEntity.ApplicationId = aspnetApplication.ApplicationId;
                aspUserEntity.AspnetApplication = aspnetApplication;
                AspUserRepository.Context.FlushChanges();
                AspUserRepository.Refresh(aspUserEntity);

                userEntity = request.ToEntity(userEntity, fields);
                userEntity.IsValidated = true;
                userEntity.Email = request.Email;
                userEntity.LoweredEmail = request.Email?.ToLower();
                Repository.Update(userEntity);
                userEntity.AspUserID = aspUserEntity.UserId;
                userEntity.AspnetUser = aspUserEntity;
                //var record = AuditRecordsRepository.AddRecord("UserUpdated");
                //record.UserID = userEntity.UserID;
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