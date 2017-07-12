using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

  
namespace Excelsior.API.Controllers.v1
{
    
    [Authorize]
    [Route("api/v1/[controller]")]
    public class TemplateAnswersController : Controller
    {
        public ITemplateAnswersGateway Gateway { get; set; }

        public TemplateAnswersController(ITemplateAnswersGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/TemplateAnswers Get Template Answers
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Template Answers
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     trialquestionId Trial Question Identifier
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of Template Answers JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                  "Id": 0,
                                  "TemplateQuestionId": 0,
                                  "Value": "",
                                  "Code": "",
                                  "Index": 0
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
        public IActionResult GetAll(long? trialquestionId, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new TemplateAnswersRequestDto()
            {
                UserId = userId,
                QuestionId = trialquestionId,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/TemplateAnswers/{id} Get Template Answer by Id
         * @apiName GetSingle 
         * @apiVersion 1.0.0
         * @apiGroup Template Answers
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Template Answer identifier
         * @apiSuccess {JSON} Result The Template Answer JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                                  "TemplateDependencies": [
                                    {
                                      "Sources": [
                                        {
                                          "Id": 0,
                                          "DependencyID": 0,
                                          "SourceAnswerId": 0,
                                          "SourceQuestionId": 0
                                        }
                                      ],
                                      "Id": 0,
                                      "SourceAnswerId": 0,
                                      "TargetQuestionId": 0,
                                      "TargetAnswerId": 0,
                                      "Expression": "",
                                      "ActionEnable": false
                                    }
                                  ],
                                  "Id": 0,
                                  "TemplateQuestionId": 0,
                                  "Value": "",
                                  "Code": "",
                                  "Index": 0
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
         * @api {post} api/v1/TemplateAnswers Post Template Answer
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Template Answers
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Template Answer
         * @apiParamExample {json} Request-Example:
         *      {
                      "TemplateDependencies": [
                        {
                          "Sources": [
                            {
                              "Id": 0,
                              "DependencyID": 0,
                              "SourceAnswerId": 0,
                              "SourceQuestionId": 0
                            }
                          ],
                          "Id": 0,
                          "SourceAnswerId": 0,
                          "TargetQuestionId": 0,
                          "TargetAnswerId": 0,
                          "Expression": "",
                          "ActionEnable": false
                        }
                      ],
                      "Id": 0,
                      "TemplateQuestionId": 0,
                      "Value": "",
                      "Code": "",
                      "Index": 0
                    }
         * @apiSuccess {JSON} Result The Template Answer JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": 
                                {
                                      "TemplateDependencies": [
                                        {
                                          "Sources": [
                                            {
                                              "Id": 0,
                                              "DependencyID": 0,
                                              "SourceAnswerId": 0,
                                              "SourceQuestionId": 0
                                            }
                                          ],
                                          "Id": 0,
                                          "SourceAnswerId": 0,
                                          "TargetQuestionId": 0,
                                          "TargetAnswerId": 0,
                                          "Expression": "",
                                          "ActionEnable": false
                                        }
                                      ],
                                      "Id": 0,
                                      "TemplateQuestionId": 0,
                                      "Value": "",
                                      "Code": "",
                                      "Index": 0
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
		                        "key": "TemplateQuestionId",
		                        "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                        }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]TemplateAnswerFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/TemplateAnswers/{id} Put Template Answer
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Template Answers
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Code,Index,Value,IsActive,TemplateQuestionId" 
         *  }
         * @apiParam (Request Parameters) {Number}            Id=0 Template Answer identifier
         * @apiParam {JSON} Object modified of Template Answer
         * @apiParamExample {json} Request-Example:
         *      {
                      "TemplateDependencies": [
                        {
                          "Sources": [
                            {
                              "Id": 0,
                              "DependencyID": 0,
                              "SourceAnswerId": 0,
                              "SourceQuestionId": 0
                            }
                          ],
                          "Id": 0,
                          "SourceAnswerId": 0,
                          "TargetQuestionId": 0,
                          "TargetAnswerId": 0,
                          "Expression": "",
                          "ActionEnable": false
                        }
                      ],
                      "Id": 0,
                      "TemplateQuestionId": 0,
                      "Value": "",
                      "Code": "",
                      "Index": 0
                    }
         * @apiSuccess {JSON} Result The Template Answer JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": true,
                    "Message": "successful"
                    "Exception": "Exception Message",
                    "Result": 
                        {
                              "TemplateDependencies": [
                                {
                                  "Sources": [
                                    {
                                      "Id": 0,
                                      "DependencyID": 0,
                                      "SourceAnswerId": 0,
                                      "SourceQuestionId": 0
                                    }
                                  ],
                                  "Id": 0,
                                  "SourceAnswerId": 0,
                                  "TargetQuestionId": 0,
                                  "TargetAnswerId": 0,
                                  "Expression": "",
                                  "ActionEnable": false
                                }
                              ],
                              "Id": 0,
                              "TemplateQuestionId": 0,
                              "Value": "",
                              "Code": "",
                              "Index": 0
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
		                            "key": "TemplateQuestionId",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TemplateAnswerFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/TemplateAnswers/{id} Delete Template Answer
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Template Answers
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                         Id=0 Template Answer identifier
         * @apiSuccess {JSON} Result The Template Answer JSON object.
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
