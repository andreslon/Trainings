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
    public class StudyUserController : Controller
    {
        public IStudyUserGateway Gateway { get; set; }

        public StudyUserController(IStudyUserGateway gateway)
        {
            Gateway = gateway;
        }

        /**
       * @api {get} api/v1/StudyUser Get all StudyUser
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup StudyUser
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  } 
       * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active StudyUser? 
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page. 
       * @apiSuccess {JSON} Result                                                   The paginated array of StudyUser JSON objects.
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
                      "isKeyStudyUser": false
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
        public IActionResult GetAll(bool? isActive, int? page, int? pageSize)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new StudyUserRequestDto()
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize,
                IsActive = isActive
            };
            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/StudyUser/{id} Get StudyUser by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup StudyUser
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 StudyUser identifier
        * @apiSuccess {JSON} Result The StudyUser JSON object.
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
                                  "isKeyStudyUser": false
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
        * @api {post} api/v1/StudyUser Post StudyUser
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup StudyUser
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Object created of StudyUser
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
                                  "isKeyStudyUser": false
                                }
        * @apiSuccess {JSON} Result The StudyUser JSON object.
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
                                  "isKeyStudyUser": false
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
        public IActionResult Post([FromBody]StudyUserFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/StudyUser/{id} Put StudyUser
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup StudyUser
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "StudyId,UserId,IsActive"  
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 StudyUser identifier
        * @apiParam {JSON} Object modified of StudyUser
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
                                  "isKeyStudyUser": false
                                }
        * @apiSuccess {JSON} Result The StudyUser JSON object.
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
                                  "isKeyStudyUser": false
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
        public IActionResult Put(int id, [FromBody]StudyUserFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
        * @api {delete} api/v1/StudyUser/{id} Delete StudyUser
        * @apiName Delete
        * @apiVersion 1.0.0
        * @apiGroup StudyUser
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 StudyUser identifier
        * @apiSuccess {JSON} Result The StudyUser JSON object.
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
    }
}
