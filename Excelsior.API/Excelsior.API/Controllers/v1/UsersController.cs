using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {
        public IUsersGateway Gateway { get; set; }

        public UsersController(IUsersGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/users Get users
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Users
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active trials?
        * @apiParam (Request Parameters) {Boolean=true,false}                         isLocked=false Get only locked trials?
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of users JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                  "Id": 0,
                                  "AspnetUserId": "00000000-0000-0000-0000-000000000000",
                                  "UserName": "",
                                  "FirstName": "",
                                  "LastName": "",
                                  "Email": "",
                                  "JobTitle": "",
                                  "IsActive": false,
                                  "AffiliationId": 0,
                                  "RoleId": "00000000-0000-0000-0000-000000000000",
                                  "Affiliation": {
                                    "Id": 0,
                                    "Name": "",
                                    "IsActive": false,
                                    "CountryId": 0,
                                    "Country": {
                                      "Id": 0,
                                      "Name": ""
                                    }
                                  },
                                  "Role": {
                                    "LoweredName": "",
                                    "Description": "",
                                    "Id": "00000000-0000-0000-0000-000000000000",
                                    "Name": ""
                                  }
                                }
                           ],
                    "pager": {
                            "itemCount": 1,
                            "pageIndex": 1,
                            "pageSize": 10,
                            "pageCount": 1
                            }
               }
        *
        */
        [HttpGet]
        [Route("")]
        public IActionResult GetAll(bool? isActive, bool? isLocked, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new UsersRequestDto()
            {
                UserId = userId,
                IsActive = isActive,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/users/current Get current user
         * @apiName GetCurrent   
         * @apiVersion 1.0.0
         * @apiGroup Users
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiSuccess {JSON} Result The User JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": {
                                      "Id": 0,
                                      "AspnetUserId": "00000000-0000-0000-0000-000000000000",
                                      "UserName": "",
                                      "FirstName": "",
                                      "LastName": "",
                                      "Email": "",
                                      "JobTitle": "",
                                      "IsActive": false,
                                      "AffiliationId": 0,
                                      "RoleId": "00000000-0000-0000-0000-000000000000",
                                      "Affiliation": {
                                        "Id": 0,
                                        "Name": "",
                                        "IsActive": false,
                                        "CountryId": 0,
                                        "Country": {
                                          "Id": 0,
                                          "Name": ""
                                        }
                                      },
                                      "Role": {
                                        "LoweredName": "",
                                        "Description": "",
                                        "Id": "00000000-0000-0000-0000-000000000000",
                                        "Name": ""
                                      }
                                    }
                        }
         *
         */
        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var result = Gateway.GetSingle(new Guid(userId));
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/users/{id} Get user by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Users
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 User identifier
         * @apiSuccess {JSON} Result The user JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": {
                                      "Id": 0,
                                      "AspnetUserId": "00000000-0000-0000-0000-000000000000",
                                      "UserName": "",
                                      "FirstName": "",
                                      "LastName": "",
                                      "Email": "",
                                      "JobTitle": "",
                                      "IsActive": false,
                                      "AffiliationId": 0,
                                      "RoleId": "00000000-0000-0000-0000-000000000000",
                                      "Affiliation": {
                                        "Id": 0,
                                        "Name": "",
                                        "IsActive": false,
                                        "CountryId": 0,
                                        "Country": {
                                          "Id": 0,
                                          "Name": ""
                                        }
                                      },
                                      "Role": {
                                        "LoweredName": "",
                                        "Description": "",
                                        "Id": "00000000-0000-0000-0000-000000000000",
                                        "Name": ""
                                      }
                                    }
                        }
         *
         */

        [HttpGet("{id}")]
        public IActionResult GetSingle(string id)
        {
            long idL;
            ResultInfo<UserFullDto> result = null;
            if (long.TryParse(id, out idL))
                result = Gateway.GetSingle(idL);
            else
                result = Gateway.GetSingle(new Guid(id));
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/users/DecryptUserId/{cryptedcode} Decrypt User Id by crypted code
        * @apiName DecryptUserId   
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
                        "userId" = "userId"
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
        [HttpGet("DecryptUserId/{cryptedcode}")]
        public IActionResult DecryptUserId(string cryptedcode)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(cryptedcode);
            string decodedString = System.Text.Encoding.ASCII.GetString(encodedDataAsBytes);
            var result = Gateway.DecryptUserId(decodedString);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/users Post User
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Users
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of User
         * @apiParamExample {json} Request-Example:
         *     {
                  "Id": 0,
                  "AspnetUserId": "00000000-0000-0000-0000-000000000000",
                  "UserName": "",
                  "FirstName": "",
                  "LastName": "",
                  "Email": "",
                  "JobTitle": "",
                  "IsActive": false,
                  "AffiliationId": 0,
                  "RoleId": "00000000-0000-0000-0000-000000000000",
                  "Affiliation": {
                    "Id": 0,
                    "Name": "",
                    "IsActive": false,
                    "CountryId": 0,
                    "Country": {
                      "Id": 0,
                      "Name": ""
                    }
                  },
                  "Role": {
                    "LoweredName": "",
                    "Description": "",
                    "Id": "00000000-0000-0000-0000-000000000000",
                    "Name": ""
                  }
                }
         * @apiSuccess {JSON} Result The User JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": {
                                      "Id": 0,
                                      "AspnetUserId": "00000000-0000-0000-0000-000000000000",
                                      "UserName": "",
                                      "FirstName": "",
                                      "LastName": "",
                                      "Email": "",
                                      "JobTitle": "",
                                      "IsActive": false,
                                      "AffiliationId": 0,
                                      "RoleId": "00000000-0000-0000-0000-000000000000",
                                      "Affiliation": {
                                        "Id": 0,
                                        "Name": "",
                                        "IsActive": false,
                                        "CountryId": 0,
                                        "Country": {
                                          "Id": 0,
                                          "Name": ""
                                        }
                                      },
                                      "Role": {
                                        "LoweredName": "",
                                        "Description": "",
                                        "Id": "00000000-0000-0000-0000-000000000000",
                                        "Name": ""
                                      }
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
                                  "key": "UserName",
                                  "ErrorMessage": "The Name field is required."
                                }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]UserFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/users/{id} Put User
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Users
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "FirstName,LastName,IsActive,RoleId,AffiliationId,JobTitle,Email" 
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 User identifier
         * @apiParam {JSON} Object modified of User
         * @apiParamExample {json} Request-Example:
         *     {
                          "Id": 0,
                          "AspnetUserId": "00000000-0000-0000-0000-000000000000",
                          "UserName": "",
                          "FirstName": "",
                          "LastName": "",
                          "Email": "",
                          "JobTitle": "",
                          "IsActive": false,
                          "AffiliationId": 0,
                          "RoleId": "00000000-0000-0000-0000-000000000000",
                          "Affiliation": {
                            "Id": 0,
                            "Name": "",
                            "IsActive": false,
                            "CountryId": 0,
                            "Country": {
                              "Id": 0,
                              "Name": ""
                            }
                          },
                          "Role": {
                            "LoweredName": "",
                            "Description": "",
                            "Id": "00000000-0000-0000-0000-000000000000",
                            "Name": ""
                          }
                        }
         * @apiSuccess {JSON} Result The User JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": {
                                      "Id": 0,
                                      "AspnetUserId": "00000000-0000-0000-0000-000000000000",
                                      "UserName": "",
                                      "FirstName": "",
                                      "LastName": "",
                                      "Email": "",
                                      "JobTitle": "",
                                      "IsActive": false,
                                      "AffiliationId": 0,
                                      "RoleId": "00000000-0000-0000-0000-000000000000",
                                      "Affiliation": {
                                        "Id": 0,
                                        "Name": "",
                                        "IsActive": false,
                                        "CountryId": 0,
                                        "Country": {
                                          "Id": 0,
                                          "Name": ""
                                        }
                                      },
                                      "Role": {
                                        "LoweredName": "",
                                        "Description": "",
                                        "Id": "00000000-0000-0000-0000-000000000000",
                                        "Name": ""
                                      }
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
                                  "key": "UserName",
                                  "ErrorMessage": "The Name field is required."
                                }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]UserFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/users/{id} Delete User
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Users
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 User identifier
         * @apiSuccess {JSON} Result The User JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": true
                        }
         *
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/users/sendemail Post Send Email
        * @apiName sendemail
        * @apiVersion 1.0.0
        * @apiGroup Users
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Boolean Result
        * @apiParamExample {json} Request-Example:
                           {
                               "from":"...",
                               "subject":"...",
                               "body":"...",
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
                                 "ErrorMessage": "The email field is required."
                               }
                 ],
                 "pager": null
               } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("SendEmail")]
        public IActionResult SendEmail([FromBody]EmailBaseDto request)
        {
            var result = Gateway.SendEmail(request);
            return new OkObjectResult(result);
        }

        /**
       * @api {put} api/v1/users/Registration/{id} Put Registration
       * @apiName Registration
       * @apiVersion 1.0.0
       * @apiGroup Users
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *       "fields": "FirstName,LastName,IsActive,RoleId,AffiliationId,JobTitle,Email" 
       *  }
       * @apiParam (Request Parameters) {Number}                                                       Id=0 User identifier
       * @apiParam {JSON} Object modified of User
       * @apiParamExample {json} Request-Example:
                       *     {
                              "userName": "",
                              "newPassword": "",
                              "passwordConfirmation": "",
                              "passwordQuestion": "",
                              "passwordAnswer": "",
                              "countryId": 0,
                              "firstName": "",
                              "middleName": "",
                              "lastName": "",
                              "affiliationId": 0,
                              "email": "",
                              "city": "",
                              "address1": "",
                              "address2": "",
                              "stateProvince": "",
                              "zipCode": 0,
                              "phoneNumber": 0
                            }
       * @apiSuccess {JSON} Result The User JSON object.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
       *       {
                        "IsSuccess": false,
                        "Message": "successful"
                        "Exception": "Error",
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
                                "ErrorMessage": "The Name field is required."
                              }
                ],
                "pager": null
              } 

       *
       */ 
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("Registration/{id}")]
        public IActionResult Registration(long id, [FromBody]RegistrationFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Registration(request, fields);
            return new OkObjectResult(result);
        }


    }
}
