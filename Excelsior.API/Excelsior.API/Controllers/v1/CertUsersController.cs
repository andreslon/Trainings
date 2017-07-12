using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Excelsior.Business.Gateways.Interfaces;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.DtoEntities.Full;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class CertUsersController : Controller
    {
        public ICertUsersGateway Gateway { get; set; }

        public CertUsersController(ICertUsersGateway gateway)
        {
            Gateway = gateway;
        }

        /**
       * @api {get} api/v1/CertUsers Get all CertUsers
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup CertUser
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  }
       * @apiParam (Request Parameters) {Number}                                     userStudyId=0 user Study identifier
       * @apiParam (Request Parameters) {Number}                                     procedureId=0 Procedure identifier
       * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active CertUser? 
       * @apiParam (Request Parameters) {Boolean=true,false}                         IsCertified=false Get only Certified CertUser? 
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
       * @apiParam (Request Parameters) {String}                                     [search] Search text.
       * @apiParam (Request Parameters) {String}                                     [sort] Soting fields.
       * @apiSuccess {JSON} Result                                                   The paginated array of CertUser JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
                {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": [
                    {
                      "id": 0,
                      "userStudyId": 0,
                      "imageWidth": 1000,
                      "imageHeight": 1000,
                      "index": 1,
                      "imageLocation": "location/file.jpg",
                      "isActive": true,
                      "refLineType": null,
                      "refLineCoordinates": null,
                      "isKeyCertUser": false
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
        public IActionResult GetAll(long? studyId, long? affiliationId, long? technicianId, long? procedureId, bool? isActive, bool? isCertified, bool? hasPrevCert, string assignedTo, int? page, int? pageSize, string search, string sort)
        {
            var request = new CertUserRequestDto()
            {
                StudyId = studyId,
                TechnicianId = technicianId,
                AffiliationId = affiliationId,
                ProcedureId = procedureId,
                IsActive = isActive,
                IsCertified = isCertified,
                HasPrevCert = hasPrevCert,
                AssignedTo = assignedTo,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort
            };
            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/CertUsers/{id} Get CertUser by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup CertUser
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 CertUser identifier
        * @apiSuccess {JSON} Result The CertUser JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "id": 0,
                                  "userStudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertUser": false
                                }
              }
        *
        */
        [HttpGet("{id}")]
        public IActionResult GetSingle(long id)
        {
            var result = Gateway.GetSingle(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/CertUsers Post CertUser
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup CertUser
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Object created of CertUser
        * @apiParamExample {json} Request-Example:
                               {
                                  "id": 0,
                                  "userStudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertUser": false
                                }
        * @apiSuccess {JSON} Result The CertUser JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "id": 0,
                                  "userStudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertUser": false
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
		                            "key": "rawDataId",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]CertUserFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/CertUsers/{id} Put CertUser
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup CertUser
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "StudyUserId,CertifiedDate,IsCertified,IsActive,CertifiedById,ProcedureId"  
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 CertUser identifier
        * @apiParam {JSON} Object modified of CertUser
        * @apiParamExample {json} Request-Example:
                               {
                                  "id": 0,
                                  "userStudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertUser": false
                                }
        * @apiSuccess {JSON} Result The CertUser JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "id": 0,
                                  "userStudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertUser": false
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
		                            "key": "rawDataId",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CertUserFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
        * @api {delete} api/v1/CertUsers/{id} Delete CertUser
        * @apiName Delete
        * @apiVersion 1.0.0
        * @apiGroup CertUser
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 CertUser identifier
        * @apiSuccess {JSON} Result The CertUser JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                   "IsSuccess": true,
                   "Message": "successful"
                   "Exception": "Exception Message",
                   "Result": true
               }
        *
        */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/CertUsers/{id}/assign Post assign CertUser to user
         * @apiName Assign
         * @apiVersion 1.0.0
         * @apiGroup CertUser
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id Series identifier
         * @apiSuccess {JSON} Result The list of CertUser JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": [
                            {
                            }
                    ]
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
        [HttpPost]
        [Route("{id}/assign")]
        public IActionResult Assign(long id)
        {
            var result = Gateway.Assign(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/certusers/{id}/certify Post Certify
         * @apiName Certify
         * @apiVersion 1.0.0
         * @apiGroup CertUser
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeader (Header) {String} Password User Password.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *      "Password": "xxxxxxxx"
         *  }
         * @apiParam {Number}                         Id CertUser identifier
         * @apiSuccess {JSON} Result The list of CertUser JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": [
                            {
                            }
                    ]
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
        [HttpPost]
        [Route("{id}/certify")]
        public IActionResult Certify(long id, [FromHeader]string password)
        {
            var result = Gateway.Certify(id, password);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/certusers/{id}/reject Post Reject
         * @apiName Reject
         * @apiVersion 1.0.0
         * @apiGroup CertUser
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeader (Header) {String} Password User Password.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *      "Password": "xxxxxxxx"
         *      "Reason": ""
         *  }
         * @apiParam {Number}                         Id CertUser identifier
         * @apiSuccess {JSON} Result The list of CertUser JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": [
                            {
                            }
                    ]
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
        [HttpPost]
        [Route("{id}/reject")]
        public IActionResult Reject(long id, [FromHeader]string password, [FromHeader]string reason)
        {
            var result = Gateway.Reject(id, password, reason);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/certusers/{id}/prevcertifications GET Previous Certifications
         * @apiName Previous Certifications
         * @apiVersion 1.0.0
         * @apiGroup CertUser
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id CertUser identifier
         * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
         * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
         * @apiParam (Request Parameters) {String}                                     [search] Search text.
         * @apiParam (Request Parameters) {String}                                     [sort] Soting fields.
         * @apiSuccess {JSON} Result The list of CertUser JSON objects.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": [
                            {
                            }
                    ]
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
        [HttpGet]
        [Route("{id}/prevcertifications")]
        public IActionResult GetPrevCertifications(long id, int? page, int? pageSize, string search, string sort)
        {
            var request = new BaseRequestDto()
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort
            };
            var result = Gateway.GetPrevCertifications(id, request);
            return new OkObjectResult(result);
        }
    }
}
