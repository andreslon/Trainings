using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;

using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Business.Gateways;
using Excelsior.Business.Helpers;
using Excelsior.Business.Repositories;
using Excelsior.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class SeriesController : Controller
    {
        public ISeriesGateway Gateway { get; set; }

        public SeriesController(ISeriesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/series Get series
        * @apiName GetSeries
        * @apiVersion 1.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                                       studyId Study Id
        * @apiParam (Request Parameters) {String=["upload","check-in","grade","verify","completed"]}    [step] Workflow step name
        * @apiParam (Request Parameters) {String=["me","any","none",""]}                                [assignedTo] Assigned To
        * @apiParam (Request Parameters) {Number}                                                       page_size=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                                       page=1 Current page.
        * @apiParam (Request Parameters) {String}                                                       sort="recent" Sort by.
        * @apiParam (Request Parameters) {String}                                                       [search] Search text.
        * @apiParam (Request Parameters) {Number}                                                       [siteId] Site Id.
        * @apiParam (Request Parameters) {Number}                                                       [subjectId] Subject Id.
        * @apiParam (Request Parameters) {Number}                                                       [timePointId] Time Point Id.
        * @apiParam (Request Parameters) {Number}                                                       [procedureId] Procedure Id.
        * @apiSuccess {JSON} Result The paginated array of Sequence JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "",
                 "Exception": "",
                 "Result": [
                              {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
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
        public IActionResult GetAll(long studyId, string step, long? categoryId, string dataType, long? siteId, long? subjectId, long? timePointListId, long? procedureId, string assignedTo, long? seriesGroupId, long? subjectGroupId, long? subjectCohortId, int? page, int? pageSize, string search, string sort, string filter)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new SeriesRequestDto
            {
                UserId = userId,
                CategoryId = categoryId,
                DataType = dataType,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort,
                Filter = filter,
                StudyId = studyId,
                Step = step,
                SiteId = siteId,
                SubjectId = subjectId,
                TimePointListId = timePointListId,
                ProcedureId = procedureId,
                AssignedTo = assignedTo,
                SeriesGroupId = seriesGroupId,
                SubjectCohortId = subjectCohortId,
                SubjectGroupId = subjectGroupId,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/series Post series
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Series
         * @apiParamExample {json} Request-Example:
                            {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
                                }
                            }
         * @apiSuccess {JSON} Result The Series JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": 
                            {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
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
                                  "key": "Name",
                                  "ErrorMessage": "The Name field is required."
                                }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]SeriesFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
                * @api {put} api/v1/Series/{id} Put Series
                * @apiName Put
                * @apiVersion 1.0.0
                * @apiGroup Series
                *
                * @apiHeader (Header) {String} Authorization Authorization Bearer token.
                * @apiHeader (Header) {String} fields fields to be modified.
                * @apiHeaderExample Header Example
                *  {
                *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
                *       "fields": "seriesgroupid,laststepcompletionDate,studydate,directorreviewcomplete,photographerid,equipmentid,lastexportdate,dicominstanceuid,isactive,isdataqualityadequate,isqcseries,isvalidated,worflowtemplatestepid,colorcategoryid,assignedtoid,scheduleid" 
                *  }
                * @apiParam (Request Parameters) {Number}  Id Series identifier
                * @apiParam {JSON} Object modified of Series
                * @apiParamExample {json} Request-Example:
                        {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
                                }
                        }
                * @apiSuccess {JSON} Result The Media JSON object.
                * @apiSuccessExample Success-Response
                *  HTTP/1.1 200 OK
                *       {
                        "IsSuccess": true,
                        "Message": "Successful",
                        "Exception": "Exception Message",
                        "Result": 
                            {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
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
                                            "key": "DataTypeId",
                                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
                                        }
                          ],
                          "pager": null
                        } 

                *
                */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]SeriesFullDto request, [FromHeader]string fields, [FromHeader]string password, [FromHeader]string reason)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields, password, reason);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/Series/{id} Get Series by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id Series identifier
        * @apiSuccess {JSON} Result The Series JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                    {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
                                }
                    }
              }
        *
        */
        [HttpGet("{id}")]
        [Route("{id}")]
        public IActionResult GetSingle(long id)
        {
            var result = Gateway.GetSingle(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/Series/{id} Delete Series
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id Series identifier
         * @apiSuccess {JSON} Result The Template JSON object.
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
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }

        #region Uploads

        /**
         * @api {get} api/v1/Series/{id}/uploads Get Uploads
         * @apiName GetUploads
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Series identifier
         * @apiSuccess {JSON} Result The list of Media JSON objects.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    
                            }
               }
         *
         */
        [HttpGet("{id}/uploads")]
        [Route("{id}/uploads")]
        public IActionResult GetUploads(long id)
        {
            var result = Gateway.GetUploads(id);
            return new OkObjectResult(result);
        }

        #endregion

        #region Media

        /**
         * @api {get} api/v1/Series/{id}/media Get Media
         * @apiName GetMedia
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Series identifier
         * @apiSuccess {JSON} Result The list of Media JSON objects.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    
                            }
               }
         *
         */
        [HttpGet("{id}/media")]
        [Route("{id}/media")]
        public IActionResult GetMedia(long id)
        {
            var result = Gateway.GetMedia(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/Series/{id}/media Post Media
        * @apiName PostMedia
        * @apiVersion 1.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id Media identifier
        * @apiParam {JSON} List of Media Objects
        * @apiParamExample {json} Request-Example:
                    {
                    }
        * @apiSuccess {JSON} Result The List of Procedure JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": {
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
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("{id}/media")]
        [Route("{id}/media")]
        public IActionResult PostMedia(long id, [FromBody]IList<MediaFullDto> media)
        {
            var result = Gateway.SetMedia(id, media);
            return new OkObjectResult(result);
        }

        #endregion

        #region Comments

        /**
         * @api {get} api/v1/Series/{id}/comments Get Comments
         * @apiName GetComments
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Series identifier
         * @apiSuccess {JSON} Result The list of Comment JSON objects.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    
                            }
               }
         *
         */
        [HttpGet("{id}/comments")]
        [Route("{id}/comments")]
        public IActionResult GetComments(long id)
        {
            var result = Gateway.GetComments(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/Series/{id}/comments Add a Comment
         * @apiName AddComment
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Series identifier
         * @apiSuccess {JSON} Result The list of Comment JSON objects.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    
                            }
               }
         *
         */
        [HttpPost("{id}/comments")]
        [Route("{id}/comments")]
        public IActionResult AddComment(long id, [FromBody]string value)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var result = Gateway.AddComment(id, userId, value);
            return new OkObjectResult(result);
        }

        #endregion

        #region Grading 

        /**
        * @api {get} api/v1/series/{id}/gradingTemplate Get Grading Template for a Series
        * @apiName GetSeriesGradingTemplate   
        * @apiVersion 1.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                                       Id=0 Series identifier
        * @apiParam (Request Parameters) {Boolean}                                                      [isHierarchical]=false Get a hierarchical list
        * @apiSuccess {JSON} Result The GradingTemplate JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": [
                                       {
                                         "Id": 0,

                                       }
                                   ]
                       }
        *
        */
        [HttpGet]
        [Route("{id}/gradingtemplate")]
        public IActionResult GetGradingTemplateForSeries(long id, bool? isHierarchical)
        {
            ResultInfo<GradingTemplateFullDto> result = Gateway.GetGradingTemplateForSeries(id, isHierarchical.GetValueOrDefault(false));
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/series/{id}/gradingreport Get Grading Report for a Series
        * @apiName GetSeriesGradingReport
        * @apiVersion 1.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                                       Id=0 Series identifier
        * @apiSuccess {JSON} Result The GradingReport JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": [
                                       {
                                         "Id": 0,

                                       }
                                   ]
                       }
        *
        */
        [HttpGet]
        [Route("{id}/gradingreport")]
        public IActionResult GetGradingReportForSeries(long id)
        {
            ResultInfo<GradingReportFullDto> result = Gateway.GetGradingReportForSeries(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/series/{id}/gradingreport/history Get History Grading Reports for a Series
        * @apiName GetSeriesHistoryGradingReports
        * @apiVersion 1.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}        Id=0 Series identifier
        * @apiSuccess {JSON} Result The list of GradingReport JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": [
                                       {
                                         "Id": 0,

                                       }
                                   ]
                       }
        *
        */
        [HttpGet]
        [Route("{id}/gradingreport/history")]
        public IActionResult GetHistoryGradingReports(long id)
        {
            ResultInfo<IList<GradingReportFullDto>> result = Gateway.GetHistoryGradingReports(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/series/{id}/gradingreport/graders Get Graders Grading Reports for a Series
        * @apiName GetSeriesGradersGradingReports
        * @apiVersion 1.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}        Id=0 Series identifier
        * @apiSuccess {JSON} Result The list of GradingReport JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": [
                                       {
                                         "Id": 0,

                                       }
                                   ]
                       }
        *
        */
        [HttpGet]
        [Route("{id}/gradingreport/graders")]
        public IActionResult GetGradersGradingReports(long id)
        {
            ResultInfo<IList<GradingReportFullDto>> result = Gateway.GetGradersGradingReports(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/Series/{id}/report/save Save Grading Report
         * @apiName Post Report
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}      id Series identifier
         * @apiParam {JSON} report Report JSON object
         * @apiSuccess {JSON} Result report JSON objects.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    
                            }
               }
         *
         */
        [HttpPost("{id}/gradingreport/save")]
        [Route("{id}/gradingreport/save")]
        public IActionResult SaveReport(long id, [FromBody]GradingReportFullDto report)
        {
            var result = Gateway.SaveReport(id, report);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/Series/{id}/report/sign Sign Grading Report
         * @apiName Post Sign Report
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}      id Series identifier
         * @apiParam (Request Parameters) {Boolean}     isPass Is pass
         * @apiParam (Request Parameters) {Boolean}     needsReview Needs review
         * @apiParam (Request Parameters) {Number}      subjectLaterality Subject laterality
         * @apiParam (Request Parameters) {Boolean}     ignoreMultiModality Ignore Multimodality
         * @apiParam {JSON} report Report JSON object
         * @apiSuccess {JSON} Result The list of Series JSON objects.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    
                            }
               }
         *
         */
        [HttpPost("{id}/gradingreport/sign")]
        [Route("{id}/gradingreport/sign")]
        public IActionResult SignReport(long id, long currentStepId, bool? isPass, bool? needsReview, string subjectLaterality, bool? ignoreMultiModality, [FromBody]GradingReportFullDto report, [FromHeader]string password)
        {
            var result = Gateway.SignReport(id, currentStepId, isPass.GetValueOrDefault(false), needsReview.GetValueOrDefault(false), subjectLaterality, ignoreMultiModality.GetValueOrDefault(false), report, password);
            return new OkObjectResult(result);
        }

        #endregion

        #region Workflow

        /**
         * @api {post} api/v1/series/{id}/completestep Post complete current step
         * @apiName CompleteStep
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeader (Header) {String} Password User Password.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *      "Password": "xxxxxxxx"
         *  }
         * @apiParam {Number}                         Id Series identifier
         * @apiSuccess {JSON} Result The list of Series JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": [
                            {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
                                }
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
        [Route("{id}/completestep")]
        public IActionResult CompleteStep(long id, long currentStepId, bool? ignoreMultiModality, string receivedLaterality, [FromHeader]string password)
        {
            var result = Gateway.CompleteStep(id, currentStepId, ignoreMultiModality.GetValueOrDefault(false), receivedLaterality, password);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/series/{id}/review Post send series to verify
         * @apiName Review
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeader (Header) {String} Password User Password.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *      "Password": "xxxxxxxx"
         *  }
         * @apiParam {Number}                         Id Series identifier
         * @apiSuccess {JSON} Result The list of Series JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": [
                            {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
                                }
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
        [Route("{id}/review")]
        public IActionResult Review(long id, long currentStepId, [FromHeader]string password, [FromHeader]string reason)
        {
            var result = Gateway.Review(id, currentStepId, password, reason);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/series/{id}/assign Post assign series to user
         * @apiName Assign
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id Series identifier
         * @apiSuccess {JSON} Result The list of Series JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": [
                            {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
                                }
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
        public IActionResult Assign(long id, bool? ignoreRegrade)
        {
            var result = Gateway.Assign(id, ignoreRegrade.GetValueOrDefault(false));
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/series/{id}/unassign Post unassign series
         * @apiName Unassign
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id Series identifier
         * @apiSuccess {JSON} Result The list of Series JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": [
                            {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
                                }
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
        [Route("{id}/unassign")]
        public IActionResult Unassign(long id)
        {
            var result = Gateway.Unassign(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/series/group Post Group series
         * @apiName Group
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object modified of Media
         * @apiParamExample {json} Request-Example:
                {
                    "seriesIds" : [
                        100741, ...
                    ]
                ]
         * @apiSuccess {JSON} Result The list of Series JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": [
                            {
                                "id":100741,
                                "isActive":true,
                                "timePointId":90519,
                                "timePointName":"Week 1",
                                "procedureId":10,
                                "procedureName":"SD-OCT",
                                "siteId":10060,
                                "siteName":"001 (Eye Hospital)",
                                "dataTypeId":2,
                                "dataTypeName":"OPT",
                                "subject": {
                                    "id":70263,
                                    "randomizedId":"001-020",
                                    "alternativeRandomizedId":null,
                                    "nameCode":null,
                                    "laterality":"R",
                                    "gender":null,
                                    "birthYear":1900,
                                    "enrollmentDate":null,
                                    "isActive":true,
                                    "isValidated":false,
                                    "isRejected":false,
                                    "isTesting":false,
                                    "isDismissed":false
                                }
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
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        [Route("group")]
        public IActionResult Group([FromBody]SeriesGroupRequestDto request)
        {
            var result = Gateway.Group(request);
            return new OkObjectResult(result);
        }

        #endregion

        #region Audit

        /**
         * @api {get} api/v1/Series/{id}/audits Get Audit Records
         * @apiName GetAudits
         * @apiVersion 1.0.0
         * @apiGroup Series
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Series identifier
         * @apiSuccess {JSON} Result The list of Audit Records JSON objects.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    
                            }
               }
         *
         */
        [HttpGet("{id}/audits")]
        [Route("{id}/audits")]
        public IActionResult GetWorkflowAuditRecords(long id)
        {
            var result = Gateway.GetWorkflowAuditRecords(id);
            return new OkObjectResult(result);
        }

        #endregion
    }
}