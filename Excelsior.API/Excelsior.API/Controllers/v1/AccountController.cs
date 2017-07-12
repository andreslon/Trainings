using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Excelsior.Business.Gateways;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.Gateways.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        public IAccountGateway Gateway { get; set; }
        public IUserNotificationsGateway UserNotificationsGateway { get; set; }
        public AccountController(IAccountGateway gateway, IUserNotificationsGateway userNotificationsGateway)
        {
            Gateway = gateway;
            UserNotificationsGateway = userNotificationsGateway;
        }
        /**
       * @api {post} api/v1/Account/ChangePassword/{username} Post Change Password 
       * @apiName ChangePassword
       * @apiVersion 1.0.0
       * @apiGroup Account
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
                              "CurrentPassword":"...",
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
                                "key": "CurrentPassword",
                                "ErrorMessage": "The CurrentPassword field is required."
                              }
                ],
                "pager": null
              } 

       *
       */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("ChangePassword/{username}")]
        public IActionResult ChangePassword(string username, [FromBody]AccountFullDto request)
        {
            request.UserName = username;
            var result = Gateway.ChangePassword(request);
            return new OkObjectResult(result);
        }
        /**
      * @api {post} api/v1/Account/ChangePin/{username} Post Change Pin 
      * @apiName ChangePin
      * @apiVersion 1.0.0
      * @apiGroup Account
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
                             "CurrentPassword":"...",
                             "NewPin":"...",
                             "PinConfirmation":"...",
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
                               "key": "CurrentPassword",
                               "ErrorMessage": "The CurrentPassword field is required."
                             }
               ],
               "pager": null
             } 

      *
      */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("ChangePin/{username}")]
        public IActionResult ChangePin(string username, [FromBody]AccountFullDto request)
        {
            request.UserName = username;
            var result = Gateway.ChangePin(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/Account/ChangeSecurityQuestion/{id} Post Change Pin 
        * @apiName ChangeSecurityQuestion
        * @apiVersion 1.0.0
        * @apiGroup Account
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
                             "CurrentPassword":"...",
                             "SecurityQuestion":"...",
                             "PasswordAnswer":"...",
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
                               "key": "PasswordAnswer",
                               "ErrorMessage": "The password answer is not formatted correctly."
                             }
               ],
               "pager": null
             } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("ChangeSecurityQuestion/{username}")]
        public IActionResult ChangeSecurityQuestion(string username, [FromBody]AccountFullDto request)
        {
            request.UserName = username;
            var result = Gateway.ChangeSecurityQuestion(request);
            return new OkObjectResult(result);
        }
        
        /**
           * @api {get} api/v1/Account/GetNotifications Get Notifications
           * @apiName GetNotifications
           * @apiVersion 1.0.0
           * @apiGroup Account
           *
           * @apiHeader (Header) {String} Authorization Authorization Bearer token.
           * @apiHeaderExample Header Example
           *  {
           *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
           *  }
           *
           * @apiSuccess {JSON} Result                                                   The paginated array of countries JSON objects.
           * @apiSuccessExample Success-Response
           *  HTTP/1.1 200 OK
           *       {
                    "IsSuccess": true,
                    "Message": "Successful",
                    "Exception": "Exception Message",
                    "Result": [
                                  {
                                     "Id": 0,
                                     "Name": "...",
                                     "Description": "...",
                                     "Message": "...",
                                     "IsPrivate": false,
                                     }
                              ]
                    "pager": null
                  }
           *
           */
        [HttpGet]
        [Route("GetNotifications")]
        public IActionResult GetNotifications()
        {
            var result = UserNotificationsGateway.GetNotifications();
            return new OkObjectResult(result);
        }

        /**
           * @api {get} api/v1/Account/GetNotificationsByUser/{id} Get Notifications By User
           * @apiName GetNotificationsByUser
           * @apiVersion 1.0.0
           * @apiGroup Account
           *
           * @apiHeader (Header) {String} Authorization Authorization Bearer token.
           * @apiHeaderExample Header Example
           *  {
           *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
           *  }
           *
           * @apiSuccess {JSON} Result                                                   The paginated array of countries JSON objects.
           * @apiSuccessExample Success-Response
           *  HTTP/1.1 200 OK
           *       {
                    "IsSuccess": true,
                    "Message": "Successful",
                    "Exception": "Exception Message",
                    "Result": [
                                  {
                                     "Id": 0,
                                     "Name": "...",
                                     "Description": "...",
                                     "Message": "...",
                                     "IsPrivate": false,
                                     }
                              ]
                    "pager": null
                  }
           *
           */
        [HttpGet]
        [Route("GetNotificationsByUser/{id}")]
        public IActionResult GetNotificationsByUser(int id)
        {
            var result = UserNotificationsGateway.GetAll(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/Account/UpdateNotifications/{id} Post Update Notifications 
        * @apiName UpdateNotifications
        * @apiVersion 1.0.0
        * @apiGroup Account
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {String}                                  User Name
        * @apiParam {JSON} Boolean Result
        * @apiParamExample {json} Request-Example:
                            [
                               {
                                   "NotificationID":0
                               }
                          ]
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
                               "key": "NotificationID",
                               "ErrorMessage": "The NotificationID field is required."
                             }
               ],
               "pager": null
             } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("UpdateNotifications/{id}")]
        public IActionResult UpdateNotifications(int id, [FromBody]List<UserNotificationFullDto> request)
        {
            var result = UserNotificationsGateway.Update(id, request);
            return new OkObjectResult(result);
        }
    }
}
