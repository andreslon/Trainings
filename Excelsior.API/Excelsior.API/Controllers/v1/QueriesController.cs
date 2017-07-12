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
    public class QueriesController : Controller
    {
        public IQueriesGateway Gateway { get; set; }

        public QueriesController(IQueriesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/Queries Get all queries
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Queries
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of queries JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                    "Id" : 0,
                                    "IsActive" : false
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
        public IActionResult GetAll(long? studyId, long? siteId, long? seriesId, long? certUserId, long? certEquipmentId, string queryType, string queryStatus, bool? isActive, int? page, int? pageSize, string search, string sort)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new QueriesRequestDto()
            {
                UserId = userId,
                StudyId = studyId,
                SiteId = siteId,
                SeriesId = seriesId,
                CertUserId = certUserId,
                CertEquipmentId = certEquipmentId,
                QueryType = queryType,
                QueryStatus = queryStatus,
                IsActive = isActive,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/Queries/{id} Get query by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Queries
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 query identifier
         * @apiSuccess {JSON} Result The query JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "Id" : 0,
                                    "IsActive" : false
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
         * @api {post} api/v1/Queries Post query
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Queries
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of LibraryQuery
         * @apiParamExample {json} Request-Example:
                            {
                                   "IsActive" : false
                           }
         * @apiSuccess {JSON} Result The Query JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "Id" : 0,
                                    "IsActive" : false
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
        public IActionResult Post([FromBody]QueryFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/Queries/{id} Put query
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Queries
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name,IsActive"  
         *  }
         * @apiParam (Request Parameters) {Number}   Id=0 query identifier
         * @apiParam {JSON} Object modified of LibraryQuery
         * @apiParamExample {json} Request-Example:
                            {
                                   "IsActive" : false
                           }
         * @apiSuccess {JSON} Result The Query JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "Id" : 0,
                                    "IsActive" : false
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
         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]QueryFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/Queries/{id} Delete query
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Queries
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 query identifier
         * @apiSuccess {JSON} Result The Query JSON object.
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
         * @api {post} api/v1/Queries/{id}/resolve Resolve query
         * @apiName ResolveQuery   
         * @apiVersion 1.0.0
         * @apiGroup Queries
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *      "Password": "xxxxxxxx"
         *      "Reason": ""
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 query identifier
         * @apiSuccess {JSON} Result The query JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "Id" : 0,
                                    "IsActive" : false
                            }
               }
         *
         */
        [HttpPost("{id}/resolve")]
        [Route("{id}/resolve")]
        public IActionResult Resolve(long id, [FromHeader]string password, [FromHeader]string reason)
        {
            var result = Gateway.Resolve(id, password, reason);
            return new OkObjectResult(result);
        }
    }
}
