using Excelsior.API.Helpers;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class StencilsController : Controller
    {
        public IStencilsGateway Gateway { get; set; }

        public StencilsController(IStencilsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/stencils Get stencils
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Stencils
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     trialId=33 Get stencils in study 33.
        * @apiSuccess {JSON} Result                                                   The paginated array of attachments JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                 "id": 1,
                                 "tag": "test",
                                 "trialId": 33,
                                 "color": "black"
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
        public IActionResult GetAll(long studyId)
        {
            var request = new StencilsRequestDto()
            {
                StudyId = studyId
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }



        /**
         * @api {get} api/v1/stencils/{id} Get stencil by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Stencils
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Stencil identifier
         * @apiSuccess {JSON} Result The attachment JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": {
                                      "id": 1,
                                      "tag": "test",
                                      "trialId": 33,
                                      "color": "black"
                                      },
                                      "isActive": false
                                    }
                        }
         *
         */
        [HttpGet("{id}")]
        public IActionResult GetSingle(long id)
        {
            ResultInfo<StencilsFullDto> result = null;
            result = Gateway.GetSingle(id);
          
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/stencils Post Stencil
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Stencils
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Attachment
         * @apiParamExample {json} Request-Example:
         *     {
	                "trialId": 33,
	                "tag" : "test",
	                "color" : "black"
               }
         * @apiSuccess {JSON} Result The Attachment JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *      {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": {
	                  "trialId": 33,
	                  "tag" : "test",
	                  "color" : "black"
                  },
                  "pager": null
                }
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                {
                  "isSuccess": false,
                  "message": "Invalid Model",
                  "exception": "",
                  "result": [
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]StencilsFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/stencils/{id} Put Stencil
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Stencils
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Stencil identifier
         * @apiParam {JSON} Object modified of Attachment
         * @apiParamExample {json} Request-Example:
         *     {
	                "trialId": 33,
	                "tag" : "test",
	                "color" : "black"
                }
         * @apiSuccess {JSON} Result The Attachment JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
	                    "trialId": 33,
	                    "tag" : "test",
	                    "color" : "black"
                      },
                      "pager": null
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
        public IActionResult Put(long id, [FromBody]StencilsFullDto request)
        {
            request.Id = id;
            var result = Gateway.Update(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/stencils/{id} Delete Stencil
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Stencils
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Stencil identifier
         * @apiSuccess {JSON} Result The Attachment JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": true,
                          "Message": "successful"
                          "Exception": "",
                          "Result": true
                        }
         *
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }
    }
}
