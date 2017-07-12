using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;

using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class GradingReportsController : Controller
    {
        public IGradingReportsGateway Gateway { get; set; }

        public GradingReportsController(IGradingReportsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/GradingReports Get all Grading Reports
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup GradingReport
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                  seriesId Series identifier
        * @apiParam (Request Parameters) {Number}                                  performedbyId Performed by identifier
        * @apiParam (Request Parameters) {bool}                                    isActive Is active?
        * @apiParam (Request Parameters) {bool}                                    isPrimary Is primary?
        * @apiParam (Request Parameters) {bool}                                    isSigned Is signed?
        * @apiSuccess {JSON} Result                                                The paginated array of Grading Reports JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                  "Id": 0,
                                  "TemplateId": 0,
                                  "SeriesId": 0,
                                  "TemplateName": "",
                                  "PerformedDate": "2016-10-03T19:13:22.4429996+00:00",
                                  "PerformedTime": "2016-10-03T19:13:22.4429996+00:00",
                                  "PerformedBy": 0,
                                  "TimePointDescription": "",
                                  "TimePointId": 0,
                                  "IsActive": false,
                                  "IsSigned": false,
                                  "IsPrimaryResult": false
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
        public IActionResult GetAll(long? seriesId, long? performedById, bool? isActive, bool? isPrimary, bool? isSigned)
        {
            var request = new GradingReportsRequestDto()
            {
                SeriesId = seriesId,
                PerformedById = performedById,
                IsActive = isActive,
                IsPrimary = isPrimary,
                IsSigned = isSigned
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }
       
        /**
         * @api {get} api/v1/GradingReports/{id} Get Report by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup GradingReport
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Report identifier
         * @apiSuccess {JSON} Result The Report JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                              "Results": [
                                {
                                  "QuestionString": "",
                                  "AnswerString": "",
                                  "Laterality": ""
                                }
                              ],
                              "Id": 0,
                              "TemplateId": 0,
                              "SeriesId": 0,
                              "TemplateName": "",
                              "PerformedDate": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedTime": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedBy": 0,
                              "TimePointDescription": "",
                              "TimePointId": 0,
                              "IsActive": false,
                              "IsSigned": false,
                              "IsPrimaryResult": false
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
         * @api {post} api/v1/GradingReports Post Report
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup GradingReport
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Report
         * @apiParamExample {json} Request-Example:
                            {
                              "Results": [
                                {
                                  "QuestionString": "",
                                  "AnswerString": "",
                                  "Laterality": ""
                                }
                              ],
                              "Id": 0,
                              "TemplateId": 0,
                              "SeriesId": 0,
                              "TemplateName": "",
                              "PerformedDate": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedTime": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedBy": 0,
                              "TimePointDescription": "",
                              "TimePointId": 0,
                              "IsActive": false,
                              "IsSigned": false,
                              "IsPrimaryResult": false
                            }
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                              "Results": [
                                {
                                  "QuestionString": "",
                                  "AnswerString": "",
                                  "Laterality": ""
                                }
                              ],
                              "Id": 0,
                              "TemplateId": 0,
                              "SeriesId": 0,
                              "TemplateName": "",
                              "PerformedDate": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedTime": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedBy": 0,
                              "TimePointDescription": "",
                              "TimePointId": 0,
                              "IsActive": false,
                              "IsSigned": false,
                              "IsPrimaryResult": false
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
                              "key": "SeriesId",
                              "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
                            },
                            {
                              "key": "PerformedDate",
                              "ErrorMessage": "Could not convert string to DateTime: report."
                            },
                            {
                              "key": "PerformedTime",
                              "ErrorMessage": "Unexpected character encountered while parsing value: 3."
                            }
                          ]
                        }
         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]GradingReportFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/GradingReports/{id} Put Report
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup GradingReport
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "TemplateId,SeriesId,PerformedDatePerformedTime,PerformedBy,IsActive,IsSigned,IsPrimaryResult"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Report identifier
         * @apiParam {JSON} Object modified of Report
         * @apiParamExample {json} Request-Example:
                            {
                              "Results": [
                                {
                                  "QuestionString": "",
                                  "AnswerString": "",
                                  "Laterality": ""
                                }
                              ],
                              "Id": 0,
                              "TemplateId": 0,
                              "SeriesId": 0,
                              "TemplateName": "",
                              "PerformedDate": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedTime": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedBy": 0,
                              "TimePointDescription": "",
                              "TimePointId": 0,
                              "IsActive": false,
                              "IsSigned": false,
                              "IsPrimaryResult": false
                            }
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample {json} Success example
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                              "Results": [
                                {
                                  "QuestionString": "",
                                  "AnswerString": "",
                                  "Laterality": ""
                                }
                              ],
                              "Id": 0,
                              "TemplateId": 0,
                              "SeriesId": 0,
                              "TemplateName": "",
                              "PerformedDate": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedTime": "2016-10-03T19:15:17.8540071+00:00",
                              "PerformedBy": 0,
                              "TimePointDescription": "",
                              "TimePointId": 0,
                              "IsActive": false,
                              "IsSigned": false,
                              "IsPrimaryResult": false
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
                              "key": "SeriesId",
                              "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
                            },
                            {
                              "key": "PerformedDate",
                              "ErrorMessage": "Could not convert string to DateTime: report."
                            },
                            {
                              "key": "PerformedTime",
                              "ErrorMessage": "Unexpected character encountered while parsing value: 3."
                            }
                          ]
                        }
         * 
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]GradingReportFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/gradingreports/{id} Delete grading report
         * @apiName DeleteAffiliation
         * @apiVersion 1.0.0
         * @apiGroup Affiliations
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                         Id=0 Affiliation identifier
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": true,
                    "Message": "successful"
                    "Exception": "Exception Message",
                    "Result": true
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
		                    "key": "StudyId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                    }
                      ],
                      "pager": null
                    }
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }
    }
}
