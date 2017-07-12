using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class WorkflowTemplatesController : Controller
    {
        public IWorkflowTemplatesGateway Gateway { get; set; }

        public WorkflowTemplatesController(IWorkflowTemplatesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/WorkflowTemplates Get all Work flow Templates
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Workflow Templates
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
        * @apiSuccess {JSON} Result                                                   The paginated array of Work flow Templates JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                                
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
        public ResultInfo<IList<WorkflowTemplateBaseDto>> GetAll(long? studyId, int? page, int? pageSize, string search)
        {
            var request = new WorkflowTemplatesRequestDto()
            {
                StudyId = studyId,
                Page = page,
                PageSize = pageSize,
                Search = search
            };
            return Gateway.GetAll(request);
        }

        /**
         * @api {get} api/v1/WorkflowTemplates/{id} Get Workflow Template by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Workflow Templates
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Workflow Template identifier
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
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSingle(long id)
        {
            var result = Gateway.GetSingle(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/WorkflowTemplates Post Workflow Template
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Workflow Templates
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Workflow Template
         * @apiParamExample {json} Request-Example:
                            {
                                   
                           }
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
        public IActionResult Post([FromBody]WorkflowTemplateFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/WorkflowTemplates/{id} Put Workflow Template
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Workflow Templates
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name,Type,Note,IsLocked,IsActive,StudyId" 
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Workflow Template identifier
         * @apiParam {JSON} Object modified of Workflow Template
         * @apiParamExample {json} Request-Example:
                            {
                                   
                           }
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
        [HttpPut("{id}")]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]WorkflowTemplateFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/WorkflowTemplates/{id} Delete Workflow Template
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Workflow Templates
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Workflow Template identifier
         * @apiSuccess {JSON} Result The Workflow Template JSON object.
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

        /**
         * @api {get} api/v1/WorkflowTemplates/{id}/steps Get Steps
         * @apiName GetSteps
         * @apiVersion 1.0.0
         * @apiGroup Workflow Templates
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Workflow Template identifier
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
        [HttpGet("{id}/steps")]
        [Route("{id}/steps")]
        public IActionResult GetSteps(long id)
        {
            var result = Gateway.GetSteps(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/WorkflowTemplates/{id}/steps Post Step
         * @apiName PostSteps
         * @apiVersion 1.0.0
         * @apiGroup Workflow Templates
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Workflow Template identifier
         * @apiParam {JSON} Object created of Workflow Template
         * @apiParamExample {json} Request-Example:
                            {
                                    
                           }
         * @apiSuccess {JSON} Result The Workflow Template steps JSON object.
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
		                            "key": "TemplateId",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("{id}/steps")]
        [Route("{id}/steps")]
        public IActionResult PostSteps(long id, [FromBody]IList<WorkflowTemplateStepFullDto> steps)
        {
            var result = Gateway.SetSteps(id, steps);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/WorkflowTemplates/{id}/clone Clone Workflow Template
         * @apiName PostClone
         * @apiVersion 1.0.0
         * @apiGroup Workflow Templates
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Workflow Template
         * @apiParamExample {json} Request-Example:
                            {
                                   "id": 0,  //the trial id
                           }
         * @apiSuccess {JSON} Result The Workflow Template JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                        "steps": [
                            {
                            "id": 0,
                            "templateId": 0,
                            "workflowStepId": 0,
                            "description": "Step1",
                            "index": 1,
                            "shouldSkip": false
                            }
                        ],
                        "id": 132,
                        "name": "Cloned Workflow template",
                        "type": "Type",
                        "note": "Note",
                        "isLocked": false,
                        "isActive": true,
                        "studyId": 0
                    }
               }
         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        [Route("{id}/clone")]
        public IActionResult PostClone(long id, [FromBody]CommonRequestDto request)
        {
            var result = Gateway.Clone(id, request);
            return new OkObjectResult(result);
        }
    }
}
