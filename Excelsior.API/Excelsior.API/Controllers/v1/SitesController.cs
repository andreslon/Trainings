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
    public class SitesController : Controller
    {
        public ISitesGateway Gateway { get; set; }

        public SitesController(ISitesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/sites Get sites
        * @apiName GetSites
        * @apiVersion 1.0.0
        * @apiGroup Sites
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active trials?
        * @apiParam (Request Parameters) {Number}                                     studyId Study Id
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiParam (Request Parameters) {String}                                     sort="recent" Sort by.
        * @apiSuccess {JSON} Result                                                   The paginated array of site JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "",
                 "Exception": "",
                 "Result": [
                              {
                                  "Id": 0,
                                  "RandomizedId": 0,
                                  "PrincipalInvestigator": "",
                                  "IsActive": false,
                                  "IsIRB": false,
                                  "IsTesting": false,
                                  "TotalSubjects": 0,
                                  "Affiliation":{
                                              "Id": 0,
                                              "Name": "",
                                              "IsActive": false,
                                                "Country":{
                                                          "Id": 0,
                                                          "Name": ""
                                                        }
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
        public IActionResult GetAll(bool? isActive, long? studyId, int? page, int? pageSize, string search, string sort)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new SitesRequestDto()
            {
                UserId = userId,
                StudyId = studyId,
                IsActive = isActive,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/sites/{id} Get site by Id
         * @apiName GetSiteById   
         * @apiVersion 1.0.0
         * @apiGroup Sites
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
                                  "RandomizedId": 0,
                                  "PrincipalInvestigator": "",
                                  "IsActive": false,
                                  "IsIRB": false,
                                  "IsTesting": false,
                                  "TotalSubjects": 0,
                                  "Affiliation":{
                                              "Id": 0,
                                              "Name": "",
                                              "IsActive": false,
                                                "Country":{
                                                          "Id": 0,
                                                          "Name": ""
                                                        }
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
         * @api {post} api/v1/sites Post site
         * @apiName PostSite
         * @apiVersion 1.0.0
         * @apiGroup Sites
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Site
         * @apiParamExample {json} Request-Example:
         *     {
                                  "RandomizedId": 0,
                                  "PrincipalInvestigator": "",
                                  "IsActive": false,
                                  "IsIRB": false,
                                  "IsTesting": false,
                                  "TotalSubjects": 0,
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
                                  "RandomizedId": 0,
                                  "PrincipalInvestigator": "",
                                  "IsActive": false,
                                  "IsIRB": false,
                                  "IsTesting": false,
                                  "TotalSubjects": 0,
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
        public IActionResult Post([FromBody]SiteFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/sites/{id} Put site
         * @apiName PutSite
         * @apiVersion 1.0.0
         * @apiGroup Sites
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "RandomizedId,PrincipalInvestigator,IsActive,IsIRB,IsTesting,StudyId,AffiliationId" 
         *  }
         * @apiParam (Request Parameters) {Number}            Id=0 Site identifier
         * @apiParam {JSON} Object modified of Site
         * @apiParamExample {json} Request-Example:
         *     {
                                  "Id": 0,
                                  "RandomizedId": 0,
                                  "PrincipalInvestigator": "",
                                  "IsActive": false,
                                  "IsIRB": false,
                                  "IsTesting": false,
                                  "TotalSubjects": 0,
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
                                  "RandomizedId": 0,
                                  "PrincipalInvestigator": "",
                                  "IsActive": false,
                                  "IsIRB": false,
                                  "IsTesting": false,
                                  "TotalSubjects": 0,
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
        public IActionResult Put(int id, [FromBody]SiteFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/sites/{id} Delete site
         * @apiName DeleteSite
         * @apiVersion 1.0.0
         * @apiGroup Sites
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                         Id=0 Site identifier
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
