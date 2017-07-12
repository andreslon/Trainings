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
 

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{

    [Authorize]
    [Route("api/v1/[controller]")]
    public class TemplateDependenciesController : Controller
    {
        public ITemplateDependenciesGateway Gateway { get; set; }

        public TemplateDependenciesController(ITemplateDependenciesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
       * @api {get} api/v1/TemplateDependencies Get all Template Dependencies
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup Template Dependencies
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  }
       * @apiParam (Request Parameters) {Number}                                     studyId=0 Target Question identifier
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
       * @apiParam (Request Parameters) {String}                                     [search] Search text.
       * @apiSuccess {JSON} Result                                                   The paginated array of Template Dependencies JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
       *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": [
                             {
                              "Id": 0,
                              "SourceAnswerId": 0,
                              "TargetQuestionId": 0,
                              "TargetAnswerId": 0,
                              "Expression": "",
                              "ActionEnable": false
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
        public IActionResult GetAll(long? targetQuestionID, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new TemplateDependenciesRequestDto()
            {
                UserId = userId,
                TargetQuestionId = targetQuestionID,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/TemplateDependencies/{id} Get Template Dependency by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup Template Dependencies
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Dependency identifier
        * @apiSuccess {JSON} Result The Template Dependency JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": {
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
        * @api {post} api/v1/TemplateDependencies Post Template Dependency
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup Template Dependencies
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Object created of Template Dependency
        * @apiParamExample {json} Request-Example:
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
        * @apiSuccess {JSON} Result The Template Dependency JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": {
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
		                        "key": "TemplateGroupId",
		                        "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                        }
                  ],
                  "pager": null
                } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]TemplateDependencyFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }
        /**
         * @api {put} api/v1/TemplateDependencies/{id} Put Template Dependency
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Template Dependencies
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "ActionEnable,Expression,TemplateGroupId.TargetAnswerId,TargetQuestionId" 
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Template Dependency identifier
         * @apiParam {JSON} Object modified of Template Dependency
         * @apiParamExample {json} Request-Example:
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
         * @apiSuccess {JSON} Result The Template Dependency JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
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
		                        "key": "TemplateGroupId",
		                        "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                        }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TemplateDependencyFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
        * @api {delete} api/v1/TemplateDependencies/{id} Delete Template Dependency
        * @apiName Delete
        * @apiVersion 1.0.0
        * @apiGroup Template Dependencies
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Dependency identifier
        * @apiSuccess {JSON} Result The Template Dependency JSON object.
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
         * @api {get} api/v1/TemplateDependencies/{id}/sources Get Sources
         * @apiName GetSources
         * @apiVersion 1.0.0
         * @apiGroup Template Dependencies
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Template Dependency identifier
         * @apiSuccess {JSON} Result The Workflow Template JSON object.
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
        [HttpGet("{id}/sources")]
        [Route("{id}/sources")]
        public IActionResult GetSources(long id)
        {
            var result = Gateway.GetSources(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/TemplateDependencies/{id}/sources Post Sources
        * @apiName PostSources
        * @apiVersion 1.0.0
        * @apiGroup Template Dependencies
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Dependency identifier
        * @apiParam {JSON} Object created of Template Question
        * @apiParamExample {json} Request-Example:
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
        * @apiSuccess {JSON} Result The Template Answer JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": {
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
		                            "key": "DependencyID",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("{id}/sources")]
        [Route("{id}/sources")]
        public IActionResult PostSources(long id, [FromBody]IList<TemplateDependencySourceFullDto> sources)
        {
            var result = Gateway.SetSources(id, sources);
            return new OkObjectResult(result);
        }
    }
}
