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
    public class CertEquipmentController : Controller
    {
        public ICertEquipmentGateway Gateway { get; set; }

        public CertEquipmentController(ICertEquipmentGateway gateway)
        {
            Gateway = gateway;
        }

        /**
       * @api {get} api/v1/CertEquipment Get all CertEquipment
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup CertEquipment
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  }
       * @apiParam (Request Parameters) {Number}                                    studyId=0 Study identifier
       * @apiParam (Request Parameters) {Number}                                     procedureId=0 Procedure identifier
       * @apiParam (Request Parameters) {Number}                                     equipmentId=0 Equipment identifier
       * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active CertEquipment? 
       * @apiParam (Request Parameters) {Boolean=true,false}                         IsCertified=false Get only Certified CertEquipment? 
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
       * @apiParam (Request Parameters) {String}                                     [search] Search text.
       * @apiParam (Request Parameters) {String}                                     [sort] Soting fields.
       * @apiSuccess {JSON} Result                                                   The paginated array of CertEquipment JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
                {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": [
                    {
                      "id": 0,
                      "tudyId": 0,
                      "imageWidth": 1000,
                      "imageHeight": 1000,
                      "index": 1,
                      "imageLocation": "location/file.jpg",
                      "isActive": true,
                      "refLineType": null,
                      "refLineCoordinates": null,
                      "isKeyCertEquipment": false
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
        public IActionResult GetAll(long? studyId, long? affiliationId, long? equipmentId, long? procedureId, bool? isActive, bool? isCertified, bool? hasPrevCert, string assignedTo, int? page, int? pageSize, string search, string sort)
        {          
            var request = new CertEquipmentRequestDto()
            { 
                StudyId = studyId,
                AffiliationId = affiliationId,
                EquipmentId = equipmentId,
                ProcedureId= procedureId,
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
        * @api {get} api/v1/CertEquipment/{id} Get CertEquipment by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup CertEquipment
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 CertEquipment identifier
        * @apiSuccess {JSON} Result The CertEquipment JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "id": 0,
                                  "tudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertEquipment": false
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
        * @api {post} api/v1/CertEquipment Post CertEquipment
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup CertEquipment
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Object created of CertEquipment
        * @apiParamExample {json} Request-Example:
                               {
                                  "id": 0,
                                  "tudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertEquipment": false
                                }
        * @apiSuccess {JSON} Result The CertEquipment JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "id": 0,
                                  "tudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertEquipment": false
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
        public IActionResult Post([FromBody]CertEquipmentFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/CertEquipment/{id} Put CertEquipment
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup CertEquipment
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "StudyId,ProcedureId,EquipmentId,CertifiedDate,PixelSpacingY,PixelSpacingX,FrameSpacing,IsCertified,IsActive,CertifiedById" 
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 CertEquipment identifier
        * @apiParam {JSON} Object modified of CertEquipment
        * @apiParamExample {json} Request-Example:
                               {
                                  "id": 0,
                                  "tudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertEquipment": false
                                }
        * @apiSuccess {JSON} Result The CertEquipment JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "id": 0,
                                  "tudyId": 0,
                                  "imageWidth": 1000,
                                  "imageHeight": 1000,
                                  "index": 1,
                                  "imageLocation": "location/file.jpg",
                                  "isActive": true,
                                  "refLineType": null,
                                  "refLineCoordinates": null,
                                  "isKeyCertEquipment": false
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
        public IActionResult Put(int id, [FromBody]CertEquipmentFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
        * @api {delete} api/v1/CertEquipment/{id} Delete CertEquipment
        * @apiName Delete
        * @apiVersion 1.0.0
        * @apiGroup CertEquipment
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 CertEquipment identifier
        * @apiSuccess {JSON} Result The CertEquipment JSON object.
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
         * @api {post} api/v1/CertEquipment/{id}/assign Post assign CertEquipment to user
         * @apiName Assign
         * @apiVersion 1.0.0
         * @apiGroup CertEquipment
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id Series identifier
         * @apiSuccess {JSON} Result The list of CertEquipment JSON object.
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
         * @api {post} api/v1/certequipment/{id}/certify Post Certify
         * @apiName Certify
         * @apiVersion 1.0.0
         * @apiGroup CertEquipment
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeader (Header) {String} Password User Password.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *      "Password": "xxxxxxxx"
         *  }
         * @apiParam {Number}                         Id CertEquipment identifier
         * @apiSuccess {JSON} Result The list of CertEquipment JSON object.
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
        public IActionResult Certify(long id, [FromBody]CertifyEquipmentRequestDto request, [FromHeader]string password)
        {
            var result = Gateway.Certify(id, request, password);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/certequipment/{id}/reject Post Reject
         * @apiName Reject
         * @apiVersion 1.0.0
         * @apiGroup CertEquipment
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeader (Header) {String} Password User Password.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *      "Password": "xxxxxxxx"
         *  }
         * @apiParam {Number}                         Id CertEquipment identifier
         * @apiSuccess {JSON} Result The list of CertEquipment JSON object.
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
        public IActionResult Reject(long id, [FromBody]RejectCertificationRequestDto request, [FromHeader]string password, [FromHeader]string reason)
        {
            var result = Gateway.Reject(id, request, password, reason);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/certequipment/{id}/prevcertifications GET Previous Certifications
         * @apiName Previous Certifications
         * @apiVersion 1.0.0
         * @apiGroup CertEquipment
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id CertEquipment identifier
         * @apiSuccess {JSON} Result The list of CertEquipment JSON objects.
         * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
         * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
         * @apiParam (Request Parameters) {String}                                     [search] Search text.
         * @apiParam (Request Parameters) {String}                                     [sort] Soting fields.
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
