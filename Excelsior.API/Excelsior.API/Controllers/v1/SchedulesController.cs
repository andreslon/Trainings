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
    public class SchedulesController : Controller
    {
        public ISchedulesGateway Gateway { get; set; }

        public SchedulesController(ISchedulesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
       * @api {get} api/v1/Schedules Get all Schedules
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup Schedules
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  }
       * @apiParam (Request Parameters) {Number}                                     studyId Study identifier
       * @apiParam (Request Parameters) {Boolean}                                    scheduled filer scheduled
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
       * @apiParam (Request Parameters) {String}                                     [search] Search text.
       * @apiSuccess {JSON} Result                                                   The paginated array of Schedules JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
       *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": [
                               {
                                  "Id": 0,
                                  "TimePointId": 0,
                                  "ProcedureId": null,
                                  "WFTemplateId": null,
                                  "GTemplateId": null,
                                  "CRFTemplateId": null,
                                  "IsGradeBothLaterality": false,
                                  "PercentSeriesForReview": 0,
                                  "CounterSeriesForReview": 0,
                                  "CounterSeriesSigned": 0,
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
        public ResultInfo<IList<ScheduleBaseDto>> GetAll(long studyId, long? timePointId, long? procedureId, long? subjectId, int? page, int? pageSize, string search)
        {
            var request = new SchedulesRequestDto()
            {
                StudyId = studyId,
                TimePointId = timePointId,
                ProcedureId = procedureId,
                SubjectId = subjectId,
                Page = page,
                PageSize = pageSize,
                Search = search
            };
            return Gateway.GetAll(request);
        }

        /**
        * @api {get} api/v1/Schedules/{id} Get Schedule by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup Schedules
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Schedule identifier
        * @apiSuccess {JSON} Result The Schedule JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "Id": 0,
                                  "TimePointId": 0,
                                  "ProcedureId": null,
                                  "WFTemplateId": null,
                                  "GTemplateId": null,
                                  "CRFTemplateId": null,
                                  "IsGradeBothLaterality": false,
                                  "PercentSeriesForReview": 0,
                                  "CounterSeriesForReview": 0,
                                  "CounterSeriesSigned": 0,
                                  "Procedure": null,
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
        * @api {post} api/v1/Schedules Post Schedule
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup Schedules
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Object created of Schedule
        * @apiParamExample {json} Request-Example:
                               {
                                  "Id": 0,
                                  "TimePointId": 0,
                                  "ProcedureId": null,
                                  "WFTemplateId": null,
                                  "GTemplateId": null,
                                  "CRFTemplateId": null,
                                  "IsGradeBothLaterality": false,
                                  "PercentSeriesForReview": 0,
                                  "CounterSeriesForReview": 0,
                                  "CounterSeriesSigned": 0,
                                }
        * @apiSuccess {JSON} Result The Schedule JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "Id": 0,
                                  "TimePointId": 0,
                                  "ProcedureId": null,
                                  "WFTemplateId": null,
                                  "GTemplateId": null,
                                  "CRFTemplateId": null,
                                  "IsGradeBothLaterality": false,
                                  "PercentSeriesForReview": 0,
                                  "CounterSeriesForReview": 0,
                                  "CounterSeriesSigned": 0,
                                  "Procedure": null,
                                }
              }
        *
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                    {
                      "isSuccess": false,
                      "message": "Invalid Model",
                      "exception": "",
                      "result": [
	                    {
		                    "key": "TimePointId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
	                    },
                        {
		                    "key": "ProcedureId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
	                    }
                      ],
                      "pager": null
                    }
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]ScheduleFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/Schedules/{id} Put Schedule
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup Schedules
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "TimePointId,ProcedureId,WFTemplateId,GTemplateId,CRFTemplateId,IsGradeBothLaterality,PercentSeriesForReview,CounterSeriesForReview,CounterSeriesSigned" 
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Schedule identifier
        * @apiParam {JSON} Object modified of Schedule
        * @apiParamExample {json} Request-Example:
                               {
                                  "Id": 0,
                                  "TimePointId": 0,
                                  "ProcedureId": null,
                                  "WFTemplateId": null,
                                  "GTemplateId": null,
                                  "CRFTemplateId": null,
                                  "IsGradeBothLaterality": false,
                                  "PercentSeriesForReview": 0,
                                  "CounterSeriesForReview": 0,
                                  "CounterSeriesSigned": 0,
                                }
        * @apiSuccess {JSON} Result The Schedule JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "Id": 0,
                                  "TimePointId": 0,
                                  "ProcedureId": null,
                                  "WFTemplateId": null,
                                  "GTemplateId": null,
                                  "CRFTemplateId": null,
                                  "IsGradeBothLaterality": false,
                                  "PercentSeriesForReview": 0,
                                  "CounterSeriesForReview": 0,
                                  "CounterSeriesSigned": 0,
                                  "Procedure": null,
                                }
              }
        *
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                    {
                      "isSuccess": false,
                      "message": "Invalid Model",
                      "exception": "",
                      "result": [
	                    {
		                    "key": "TimePointId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
	                    },
                        {
		                    "key": "ProcedureId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
	                    }
                      ],
                      "pager": null
                    }
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ScheduleFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
        * @api {delete} api/v1/Schedules/{id} Delete Schedule
        * @apiName Delete
        * @apiVersion 1.0.0
        * @apiGroup Schedules
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Schedule identifier
        * @apiSuccess {JSON} Result The Schedule JSON object.
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
