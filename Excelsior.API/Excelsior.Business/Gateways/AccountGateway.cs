using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain.Repositories;
using Excelsior.Infrastructure.Interfaces;
using Excelsior.Infrastructure.Utilities;
using System.Globalization;

namespace Excelsior.Business.Gateways
{
    public class AccountGateway : IAccountGateway
    {
        public IAuthUserRepository AuthUserRepository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public AccountGateway(IAuthUserRepository authUserRepository, IAuditRecordsRepository auditRecordsRepository)
        {
            AuthUserRepository = authUserRepository;
            AuditRecordsRepository = auditRecordsRepository;
        }
        public ResultInfo<bool> ChangePassword(AccountFullDto request)
        {
            //Perform input validation
            var result = new ResultInfo<bool>();

            try
            {
                if (string.IsNullOrWhiteSpace(request.NewPassword))
                    throw new Exception("The NewPassword field is required.");
                if (string.IsNullOrWhiteSpace(request.PasswordConfirmation))
                    throw new Exception("The PasswordConfirmation field is required.");

                var entity = AuthUserRepository.GetByUserName(request.UserName);
                if (entity == null)
                {
                    throw new Exception("User does not exists");
                }

                var currentPassword = Infrastructure.Utilities.Security.EncodePassword(request.CurrentPassword, entity.PasswordFormat, entity.PasswordSalt);
                if (entity.Password != currentPassword)
                {
                    throw new Exception("The current password is incorrect.");
                }

                // If the values are different, then add a validation error with both members specified
                if (request.NewPassword != request.PasswordConfirmation)
                {
                    throw new Exception("Passwords do not match.");
                }

                var newPassword = Infrastructure.Utilities.Security.EncodePassword(request.NewPassword, entity.PasswordFormat, entity.PasswordSalt);
                entity.Password = newPassword;
                entity.LastPasswordChangedDate = DateTime.UtcNow;
                AuthUserRepository.Update(entity);
                var record = AuditRecordsRepository.AddRecord("UserPasswordChanged",
                    entity.CONTACT_Users?.FirstOrDefault().UserID);
                record.PerformedDateTime = DateTime.UtcNow;
                AuthUserRepository.Commit();
                AuthUserRepository.Refresh(entity);


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
        public ResultInfo<bool> ChangePin(AccountFullDto request)
        {
            //Perform input validation
            var result = new ResultInfo<bool>();

            try
            {
                if (string.IsNullOrWhiteSpace(request.NewPin))
                    throw new Exception("The NewPin field is required.");
                if (string.IsNullOrWhiteSpace(request.PinConfirmation))
                    throw new Exception("The PinConfirmation field is required.");

                var entity = AuthUserRepository.GetByUserName(request.UserName);
                if (entity == null)
                {
                    throw new Exception("User does not exists");
                }
                var currentPassword = Infrastructure.Utilities.Security.EncodePassword(request.CurrentPassword, entity.PasswordFormat, entity.PasswordSalt);
                if (entity.Password != currentPassword)
                {
                    throw new Exception("The current password is incorrect.");
                }

                // If the values are different, then add a validation error with both members specified
                if (request.NewPin != request.PinConfirmation)
                {
                    throw new Exception("Passwords do not match.");
                }

                var newPin = Infrastructure.Utilities.Security.EncodePIN(request.NewPin, entity.PasswordFormat, entity.PasswordSalt);
                entity.MobilePIN = newPin;
                AuthUserRepository.Update(entity);
                var record = AuditRecordsRepository.AddRecord("UserPINChanged",
                    entity.CONTACT_Users?.FirstOrDefault().UserID);
                record.PerformedDateTime = DateTime.UtcNow;
                AuthUserRepository.Commit();
                AuthUserRepository.Refresh(entity);


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
        public ResultInfo<bool> ChangeSecurityQuestion(AccountFullDto request)
        {
            //Perform input validation
            var result = new ResultInfo<bool>();

            try
            {
                if (string.IsNullOrWhiteSpace(request.PasswordQuestion))
                    throw new Exception("The PasswordQuestion field is required.");
                if (string.IsNullOrWhiteSpace(request.PasswordAnswer))
                    throw new Exception("The PasswordAnswer field is required.");

                var entity = AuthUserRepository.GetByUserName(request.UserName);
                if (entity == null)
                {
                    throw new Exception("User does not exists");
                }
                var currentPassword = Infrastructure.Utilities.Security.EncodePassword(request.CurrentPassword, entity.PasswordFormat, entity.PasswordSalt);
                if (entity.Password != currentPassword)
                {
                    throw new Exception("The current password is incorrect.");
                }

                if (!Security.ValidateParameter(request.PasswordQuestion, true, true, false, 256))
                {
                    throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidQuestion));
                }
                request.PasswordAnswer = request.PasswordAnswer?.Trim();
                if (!string.IsNullOrEmpty(request.PasswordAnswer))
                {
                    if (request.PasswordAnswer.Length > 128)
                    {
                        throw new Exception(StringEnum.GetStringValue(Security.MembershipCreateStatus.InvalidAnswer));
                    }
                    request.PasswordAnswer = Security.EncodePassword(request.PasswordAnswer?.Trim().ToLower(), entity.PasswordFormat, entity.PasswordSalt);
                }
                entity.PasswordQuestion = request.PasswordQuestion;
                entity.PasswordAnswer = request.PasswordAnswer;
                AuthUserRepository.Update(entity);

                var record = AuditRecordsRepository.AddRecord("UserUpdated",
                    entity.CONTACT_Users?.FirstOrDefault().UserID);
                record.PerformedDateTime = DateTime.UtcNow;
                AuthUserRepository.Commit();
                AuthUserRepository.Refresh(entity);


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
