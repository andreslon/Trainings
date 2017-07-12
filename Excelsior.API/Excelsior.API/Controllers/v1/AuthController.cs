using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Excelsior.Business.Gateways;
using Excelsior.Business.DtoEntities.Full;
using System.Text.RegularExpressions;
using IdentityModel.Client;
using Excelsior.Infrastructure.Interfaces;
using Excelsior.Business.DtoEntities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {

        public IAuthUserGateway Gateway { get; set; }
        public ISettings Settings { get; set; }

        public AuthController(IAuthUserGateway gateway, ISettings settings)
        {
            Gateway = gateway;
            Settings = settings;
        }
        /**
        * @api {get} api/v1/Auth/DecryptUserName/{cryptedcode} Decrypt User Name by crypted code
        * @apiName DecryptUserName   
        * @apiVersion 1.0.0
        * @apiGroup Auth
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {String}                                  crypted code
        * @apiSuccess {JSON} Result the user JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                    {
                        "UserName" = "userName"
                    }
              }
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                {
                  "isSuccess": false,
                  "message": "Exception",
                  "exception": "Crypted code is incorrect",
                  "result": [ 
                  ],
                  "pager": null
                } 
        *
        */
        [HttpGet("DecryptUserName/{cryptedcode}")]
        public IActionResult DecryptUserName(string cryptedcode)
        {
            var result = Gateway.DecryptUserName(cryptedcode);
            return new OkObjectResult(result);
        }
        /**
        * @api {get} api/v1/Auth/GetPasswordQuestion/{username} Get Password Question
        * @apiName GetPasswordQuestion   
        * @apiVersion 1.0.0
        * @apiGroup Auth
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {String}                                  User Name
        * @apiSuccess {JSON} Result the user JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                    {
                        "PasswordQuestion":"------?"
                        "UserName" = "userName"
                    }
              }
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                {
                  "isSuccess": false,
                  "message": "Invalid Model",
                  "exception": "",
                  "result": [
                                {
                                  "key": "Name",
                                  "ErrorMessage": "The Name field is required."
                                }
                  ],
                  "pager": null
                } 
        *
        */
        [HttpGet("GetPasswordQuestion/{username}")]
        public IActionResult GetPasswordQuestion(string username)
        {
            var result = Gateway.GetRecoveryDataByUserName(username);
            return new OkObjectResult(result);
        }
        /**
         * @api {post} api/v1/Auth/RecoveryDataByEmail/{email} Post Recovery Data By Email
         * @apiName Post RecoveryDataByEmail
         * @apiVersion 1.0.0
         * @apiGroup Auth
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {String}                                  Email
         * @apiParam {JSON} Boolean Result 
         * @apiSuccess {JSON} Result boolean response.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": true,
                    "Message": ""
                    "Exception": "",
                    "Result": true
                }
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                {
                  "isSuccess": false,
                  "message": "Invalid Model",
                  "exception": "",
                  "result": [
                                {
                                  "key": "Email",
                                  "ErrorMessage": "The Email field is required."
                                }
                  ],
                  "pager": null
                } 

         *
         */
        [HttpPost("RecoveryDataByEmail/{email:regex(^\\S+@\\S+.\\S+$)}")]
        public IActionResult RecoveryDataByEmail(string email)
        {
            var result = Gateway.RecoveryDataByEmail(email);
            return new OkObjectResult(result);
        }
        /**
        * @api {post} api/v1/Auth/ForgotAnswer/{username} Post Recovery Data By User Name
        * @apiName Post ForgotAnswer
        * @apiVersion 1.0.0
        * @apiGroup Auth
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {String}                                  Email
        * @apiParam {JSON} Boolean Result 
        * @apiSuccess {JSON} Result boolean response.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                   "IsSuccess": true,
                   "Message": ""
                   "Exception": "",
                   "Result": true
               }
        * @apiError BadRequest (400) The model state validation JSON object
        * @apiErrorExample {json} Error example:
        *     HTTP/1.1 400 Bad Request
               {
                 "isSuccess": false,
                 "message": "Invalid Model",
                 "exception": "",
                 "result": [
                               {
                                 "key": "UserName",
                                 "ErrorMessage": "The username field is required."
                               }
                 ],
                 "pager": null
               } 

        *
        */
        [HttpPost("ForgotAnswer/{username}")]
        public IActionResult ForgotAnswer(string username)
        {
            var result = Gateway.ForgotAnswer(username);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/Auth/ResetPassword/{username} Post Reset Password
         * @apiName ResetPassword
         * @apiVersion 1.0.0
         * @apiGroup Auth
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {String}                                  User Name
         * @apiParam {JSON} Boolean Result
         * @apiParamExample {json} Request-Example:
                            {
                                "Answer":"...",
                                "NewPassword":"...",
                                "PasswordConfirmation":"...",
                            }
         * @apiSuccess {JSON} Result boolean response.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": true,
                    "Message": ""
                    "Exception": "",
                    "Result": true
                }
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                {
                  "isSuccess": false,
                  "message": "Invalid Model",
                  "exception": "",
                  "result": [
                                {
                                  "key": "Email",
                                  "ErrorMessage": "The username field is required."
                                }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("ResetPassword/{username}")]
        public IActionResult ResetPassword(string username, [FromBody]AuthRecoveryDataFullDto request)
        {
            request.UserName = username;
            var result = Gateway.ResetPassword(request);
            return new OkObjectResult(result);
        }
        /**
        * @api {post} api/v1/Auth/ChangePassword/{username} Post Change Password Account
        * @apiName ChangePassword
        * @apiVersion 1.0.0
        * @apiGroup Auth
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {String}                                  User Name
        * @apiParam {JSON} Boolean Result
        * @apiParamExample {json} Request-Example:
                           {
                               "NewPassword":"...",
                               "PasswordConfirmation":"...",
                           }
        * @apiSuccess {JSON} Result boolean response.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                   "IsSuccess": true,
                   "Message": ""
                   "Exception": "",
                   "Result": true
               }
        * @apiError BadRequest (400) The model state validation JSON object
        * @apiErrorExample {json} Error example:
        *     HTTP/1.1 400 Bad Request
               {
                 "isSuccess": false,
                 "message": "Invalid Model",
                 "exception": "",
                 "result": [
                               {
                                 "key": "Email",
                                 "ErrorMessage": "The username field is required."
                               }
                 ],
                 "pager": null
               } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("ChangePassword/{username}")]
        public IActionResult ChangePassword(string username, [FromBody]AuthRecoveryDataFullDto request)
        {
            request.UserName = username;
            var result = Gateway.ChangePassword(request);
            return new OkObjectResult(result);
        }
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("GetTokenUserSystem")]
        [AllowAnonymous]
        async public Task<IActionResult> GetTokenUserSystem()
        {
            var result = new ResultInfo<string>();
            try
            {
                var tokenClient = new TokenClient($"{Settings.GetSetting("ApiUrl")}connect/token", "9E1E36E2-BE2B-4D53-9B18-8078607CABA4", "ExcelsiorWebSecret");
                var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(Settings.GetSetting("SysAccountName"), Settings.GetSetting("SysAccountPwd"), "webapi");
                if (tokenResponse.IsError)
                {
                    result.IsSuccess = false;
                    result.Message = tokenResponse.Error;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Result = tokenResponse.AccessToken;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
                result.Message = ex.Message;
            }
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/Auth/Logout Post Logout
        * @apiName Logout
        * @apiVersion 1.0.0
        * @apiGroup Auth
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiSuccess {JSON} Result boolean response.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                   "IsSuccess": true,
                   "Message": ""
                   "Exception": "",
                   "Result": true
               }
 
        */
        [HttpPost("Logout")]
        [AllowAnonymous]
        async public Task<IActionResult> Logout()
        { 
            var result = Gateway.Logout();
            return new OkObjectResult(result);
        }
    }
}
