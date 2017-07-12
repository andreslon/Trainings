using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Excelsior.Business.Gateways;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.DtoEntities.Full;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class TimePointsController : Controller
    {
        public ITimePointsGateway Gateway { get; set; }

        public TimePointsController(ITimePointsGateway gateway)
        {
            Gateway = gateway;
        }
        /**
       * @api {get} api/v1/TimePoints Get all Time Points
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup Time Points
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  }
       * @apiParam (Request Parameters) {Number}                                     studyId=0 Study identifier
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
       * @apiParam (Request Parameters) {String}                                     [search] Search text.
       * @apiSuccess {JSON} Result                                                   The paginated array of Time Points JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
       *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": [
                               {
                                  "Id": 0,
                                  "StudyID": 0,
                                  "Description": "",
                                  "Index": 0,
                                  "IsInitial": false,
                                  "IsTerminal": false,
                                  "IsEligibility": false,
                                  "ExpectedVisitStartDay": 0,
                                  "ExpectedVisitEndDay": 0
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
        public ResultInfo<IList<TimePointBaseDto>> GetAll(long studyId, int? page, int? pageSize, string search)
        {
            var request = new TimePointsRequestDto()
            {
                StudyId = studyId,
                Page = page,
                PageSize = pageSize,
                Search = search
            };
            return Gateway.GetAll(request);
        }
        /**
        * @api {get} api/v1/TimePoints/{id} Get Time Point by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup Time Points
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Time Point identifier
        * @apiSuccess {JSON} Result The Time Point JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "Id": 0,
                                  "StudyID": 0,
                                  "Description": "",
                                  "Index": 0,
                                  "IsInitial": false,
                                  "IsTerminal": false,
                                  "IsEligibility": false,
                                  "ExpectedVisitStartDay": 0,
                                  "ExpectedVisitEndDay": 0
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
        * @api {post} api/v1/TimePoints Post Time Point
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup Time Points
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Object created of Time Point
        * @apiParamExample {json} Request-Example:
                               {
                                  "Id": 0,
                                  "StudyID": 0,
                                  "Description": "",
                                  "Index": 0,
                                  "IsInitial": false,
                                  "IsTerminal": false,
                                  "IsEligibility": false,
                                  "ExpectedVisitStartDay": 0,
                                  "ExpectedVisitEndDay": 0
                                }
        * @apiSuccess {JSON} Result The Time Point JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "Id": 0,
                                  "StudyID": 0,
                                  "Description": "",
                                  "Index": 0,
                                  "IsInitial": false,
                                  "IsTerminal": false,
                                  "IsEligibility": false,
                                  "ExpectedVisitStartDay": 0,
                                  "ExpectedVisitEndDay": 0
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
		                            "key": "StudyID",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]TimePointFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }
        /**
        * @api {put} api/v1/TimePoints/{id} Put Time Point
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup Time Points
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "StudyId,Description,Index,IsInitial,IsTerminal,IsEligibility,ExpectedVisitStartDay,ExpectedVisitEndDay" 
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Time Point identifier
        * @apiParam {JSON} Object modified of Time Point
        * @apiParamExample {json} Request-Example:
                               {
                                  "Id": 0,
                                  "StudyID": 0,
                                  "Description": "",
                                  "Index": 0,
                                  "IsInitial": false,
                                  "IsTerminal": false,
                                  "IsEligibility": false,
                                  "ExpectedVisitStartDay": 0,
                                  "ExpectedVisitEndDay": 0
                                }
        * @apiSuccess {JSON} Result The Time Point JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "Id": 0,
                                  "StudyID": 0,
                                  "Description": "",
                                  "Index": 0,
                                  "IsInitial": false,
                                  "IsTerminal": false,
                                  "IsEligibility": false,
                                  "ExpectedVisitStartDay": 0,
                                  "ExpectedVisitEndDay": 0
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
		                            "key": "StudyID",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TimePointFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }
        /**
        * @api {delete} api/v1/TimePoints/{id} Delete Time Point
        * @apiName Delete
        * @apiVersion 1.0.0
        * @apiGroup Time Points
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Time Point identifier
        * @apiSuccess {JSON} Result The Time Point JSON object.
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
