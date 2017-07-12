using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AffiliationsController : Controller
    {
        public IAffiliationsGateway Gateway { get; set; }

        public AffiliationsController(IAffiliationsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/affiliations Get affiliations
        * @apiName GetAffiliations
        * @apiVersion 1.0.0
        * @apiGroup Affiliations
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active affiliations?
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of affiliation JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "",
                 "Exception": "",
                 "Result": [
                              {
                                "Id": 0,
                                "Name": "",
                                "IsActive": false,
                                "Country":{
                                            "Id": 0,
                                            "Name": ""
                                        }
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
        public IActionResult GetAll(bool? isActive, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new AffiliationsRequestDto()
            {
                UserId = userId,
                IsActive = isActive,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/affiliations/{id} Get affiliation by Id
         * @apiName GetAffiliationById   
         * @apiVersion 1.0.0
         * @apiGroup Affiliations
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Series identifier
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": 
                        {
                                "Id": 0,
                                "Name": "",
                                "IsActive": false,
                                "Country":{
                                            "Id": 0,
                                            "Name": ""
                                        }
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
         * @api {post} api/v1/affiliations Post affiliation
         * @apiName PostAffiliation
         * @apiVersion 1.0.0
         * @apiGroup Affiliations
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Affiliation
         * @apiParamExample {json} Request-Example:
         *     {
                                }
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": 
                        {
                                "Id": 0,
                                "Name": "",
                                "IsActive": false,
                                "Country":{
                                            "Id": 0,
                                            "Name": ""
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
		                    "key": "StudyId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                    }
                      ],
                      "pager": null
                    }
         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]AffiliationFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/affiliations/{id} Put affiliation
         * @apiName PutAffiliation
         * @apiVersion 1.0.0
         * @apiGroup Affiliations
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name,IsActive,CountryId" 
         *  }
         * @apiParam (Request Parameters) {Number}            Id=0 Affiliation identifier
         * @apiParam {JSON} Object modified of Affiliation
         * @apiParamExample {json} Request-Example:
         *     {
                                  "Id": 0,
                                }
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": true,
                    "Message": "successful"
                    "Exception": "Exception Message",
                    "Result": 
                        {
                                "Id": 0,
                                "Name": "",
                                "IsActive": false,
                                "Country":{
                                            "Id": 0,
                                            "Name": ""
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
		                    "key": "StudyId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                    }
                      ],
                      "pager": null
                    }
         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AffiliationFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/affiliations/{id} Delete affiliation
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
