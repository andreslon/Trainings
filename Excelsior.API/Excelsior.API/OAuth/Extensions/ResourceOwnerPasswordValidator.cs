using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain.Repositories;
using Excelsior.Infrastructure.Utilities;
using IdentityModel;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Excelsior.Domain;
using System.Data.SqlTypes;

namespace Excelsior.API.OAuth.Extensions
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        /**
       * @api {post} connect/token Post Get oAuth Token 
       * @apiName token
       * @apiVersion 1.0.0
       * @apiGroup oAuth
       *
       * @apiParam (Request Parameters) {String=password,refresh_token}  grant_type Grant type (Required)
       * @apiParam (Request Parameters) {String}                         client_id Client Id (Required)
       * @apiParam (Request Parameters) {String}                         client_secret Client Secret (Required)
       * @apiParam (Request Parameters) {String}                         [username] User Name (Required when grant_type="password")
       * @apiParam (Request Parameters) {String}                         [password] Password (Required when grant_type="password")
       * @apiParam (Request Parameters) {String=webapi offline_access}   [scope] Scope (Required when grant_type="password")
       * @apiParam (Request Parameters) {String}                         [refresh_token] Refresh Token (Required when grant_type="refresh_token")
       * 
       * @apiParamExample {json} Request-Example:
            {
                "grant_type": "password",
                "client_id": "6A82A457-D632-4F1C-B2BA-2C0B59D91A3E",
                "client_secret": "EdgeSelectSecret",
                "username": "user1",
                "password": "@pass1",
                "scope": "webapi"
            }
       *        
       * @apiSuccess {JSON} Result                                       Json Object of Token for api header
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
       *       {
                  "access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjZCN0FDQzUyMDMwNUJGREI0RjcyNTJEQUVCMjE3N0NDMDkxRkFBRTEiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJhM3JNVWdNRnY5dFBjbExhNnlGM3pBa2ZxdUUifQ.eyJuYmYiOjE0NzAzNzAyMDQsImV4cCI6MTQ3MDM3MzgwNCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1NTc4MCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTU3ODAvcmVzb3VyY2VzIiwiY2xpZW50X2lkIjoiNmE4MmE0NTctZDYzMi00ZjFjLWIyYmEtMmMwYjU5ZDkxYTNlIiwic2NvcGUiOiJ3ZWJhcGkiLCJzdWIiOiJmNjFmZjg1My1jOWJkLTQ3ZjItODBiYy1kZjQ2ODE1MDVhZTQiLCJhdXRoX3RpbWUiOjE0NzAzNzAyMDQsImlkcCI6ImxvY2FsIiwiYW1yIjpbInBhc3N3b3JkIl19.kPAl5ym7RcCY-QZeDBQBACN8I8BFP19tc_8sMWd-aDYIH2x5eTFB9Kak1mZPsQya2Cdzldb4KomDMjew-p1DPxdHkGdwavUto_2VJxsqxLdFkW5kkEoIPcIwnJlqPpLsg-QXUmQtVUQS5Xu-oG0cBhXyDpdB1gxtphvzZPWbfwCwxhSsPBFve5mRdS6hf--l912N2el9bSCCbHovof9U5CVhtCW3oqumHJi5ubmOfb74pn0TjyABA6Yv5YdoFKVzsPTu1Ka7V1kA06YBoJiPcwH1xRKEAOp1y-fQPJE2GKo8Ubt0ApB3eHzOf5WzdMDqLRousRGAfzS_suZn4d9MQw",
                  "expires_in": 3600,
                  "token_type": "Bearer"
                  "refresh_token": "5f2673ed812553c7ead2fab93435b1c8"
                }
       *
       */

        private IAuthUserRepository AuthRepository { get; set; }
        private IAuditRecordsRepository AuditRecordsRepository { get; set; }
        private IAuditActionsRepository AuditActionsRepository { get; set; }
        private IUsersRepository UsersRepository { get; set; }
        private ISystemRepository SystemRepository { get; set; }

        public ResourceOwnerPasswordValidator(IAuthUserRepository authRepository, IUsersRepository usersRepository, ISystemRepository systemRepository,
            IAuditRecordsRepository auditRecordsRepository)
        {
            AuthRepository = authRepository;
            AuditRecordsRepository = auditRecordsRepository;
            UsersRepository = usersRepository;
            SystemRepository = systemRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = AuthRepository.GetByUserName(context.UserName);
            if (user == null)
            {
                context.Result.IsError = true;
                context.Result.ErrorDescription = "Username Incorrect";
                return Task.FromResult(false);
            }
            else
            {
                //Validate if it is locked to return the error
                if (user.IsLockedOut)
                {
                    //Locking time is longer than the current time
                    if (DateTime.UtcNow < user.FailedPasswordAttemptWindowStart)
                    {
                        context.Result.IsError = true;
                        context.Result.ErrorDescription = "Your account has been locked. If you forgot your password, please click on 'Forgot Password' to reset your password.";
                        return Task.FromResult(false);
                    }
                }

                var pwd = Security.EncodePassword(context.Password, user.PasswordFormat, user.PasswordSalt);
                if (user.Password == pwd)
                {
                    var cUser = UsersRepository.GetSingle(x => x.AspUserID == new Guid(Convert.ToString(user.UserId)));

                    //Check if user is inactive
                    if (!cUser.IsActive)
                    {
                        context.Result.IsError = true;
                        context.Result.ErrorDescription = "Inactive Account";
                        return Task.FromResult(false);
                    }

                    ////Check if user is system role
                    //if (cUser.AspnetRole.RoleName == "System")
                    //{
                    //    context.Result.IsError = true;
                    //    context.Result.ErrorDescription = "System Account";
                    //    return Task.FromResult(false);
                    //}

                    context.Result.IsError = false;
                    context.Result.Error = null;
                    context.Result.ErrorDescription = null;

                    var claims = new Claim[] {
                        new Claim(JwtClaimTypes.Subject, user.UserId.ToString()),
                        new Claim(JwtClaimTypes.Name, user.UserName),
                        new Claim(JwtClaimTypes.IdentityProvider, "AuthServer"),
                        new Claim(JwtClaimTypes.AuthenticationTime, DateTime.UtcNow.ToEpochTime().ToString()),
                        new Claim(JwtClaimTypes.Role, cUser?.AspnetRole?.RoleName)
                    };

                    var id = new ClaimsIdentity(claims, ClaimTypes.Authentication, ClaimTypes.Name, ClaimTypes.Role);

                    context.Result.Subject = new ClaimsPrincipal(id);
                    context.Result.CustomResponse = new Dictionary<string, object>();

                    var sysInfo = SystemRepository.GetAll().FirstOrDefault();
                    context.Result.CustomResponse.Add("dbVersion", sysInfo?.SystemVersion);
                    //Check password expiration
                    var isPwdExpired = false;
                    if (user.LastPasswordChangedDate < DateTime.UtcNow.AddDays(-90))
                        isPwdExpired = true;
                    context.Result.CustomResponse.Add("IsPasswordExpired", isPwdExpired);
                    context.Result.CustomResponse.Add("identity", new UserFullDto(cUser));
                    RegisterLoginData(user, context.Request.Client.ClientName);
                    return Task.FromResult(context.Result);
                }
                else
                {
                    //TODO: Must check the count of attempts to decide if should lock the account
                    var isLocked = lockUser(user, context.Request.Client.ClientName);
                    if (isLocked)
                        context.Result.ErrorDescription = "Locked Account";
                    else
                        context.Result.ErrorDescription = "Password Incorrect";
                    context.Result.IsError = true;

                    return Task.FromResult(false);
                }
            }
        }

        private bool lockUser(Aspnet_Membership user, string clientName)
        {
            if (DateTime.UtcNow.AddMinutes(-10) > user.LastLockoutDate)
            {
                user.FailedPasswordAttemptCount = 0;
            }
            user.FailedPasswordAttemptCount++;
            if (user.FailedPasswordAttemptCount >= 3)
            {
                var record = AuditRecordsRepository.AddAuthRecord(user.UserId, clientName, "UserLockedOut");
                record.PerformedDateTime = DateTime.UtcNow;
                user.FailedPasswordAttemptWindowStart = DateTime.UtcNow.AddMinutes(30);
                user.IsLockedOut = true;
            }
            else
            {
                var record = AuditRecordsRepository.AddAuthRecord(user.UserId, clientName, "UserFailedLogin");
                record.PerformedDateTime = DateTime.UtcNow;
                user.IsLockedOut = false;
            }
            user.LastLockoutDate = DateTime.UtcNow;

            AuthRepository.Update(user);
            AuthRepository.Commit();
            AuthRepository.Refresh(user);
            return user.IsLockedOut;
        }

        private void RegisterLoginData(Aspnet_Membership user, string clientName)
        {
            user.LastLoginDate = DateTime.UtcNow;

            user.IsLockedOut = false;
            user.FailedPasswordAttemptCount = 0;
            user.FailedPasswordAttemptWindowStart = (DateTime)SqlDateTime.MinValue;
            user.FailedPasswordAnswerAttemptCount = 0;
            user.FailedPasswordAnswerAttemptWindowStart = (DateTime)SqlDateTime.MinValue;
            user.LastLockoutDate = (DateTime)SqlDateTime.MinValue;

            var record = AuditRecordsRepository.AddAuthRecord(user.UserId, clientName, "UserLogIn");
            AuthRepository.Update(user);
            AuthRepository.Commit();
            AuthRepository.Refresh(user);
        }
    }
}
