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
    public class SubjectsController : Controller
    {
        public ISubjectsGateway Gateway { get; set; }

        public SubjectsController(ISubjectsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/subjects Get subjects
        * @apiName GetSubjects
        * @apiVersion 1.0.0
        * @apiGroup Subjects
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active subjects?
        * @apiParam (Request Parameters) {Boolean=true,false}                         isRejected=false Get only rejected subjects?
        * @apiParam (Request Parameters) {Number}                                     studyId Study Id
        * @apiParam (Request Parameters) {Number}                                     siteId Site Id
        * @apiParam (Request Parameters) {Number}                                     groupId Subject Group Id
        * @apiParam (Request Parameters) {Number}                                     cohortId Subject Cohort Id
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiParam (Request Parameters) {String}                                     sort="recent" Sort by.
        * @apiSuccess {JSON} Result                                                   The paginated array of subject JSON objects.
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
        public IActionResult GetAll(long? studyId, long? siteId, long? affiliationId, long? groupId, long? cohortId, bool? isActive, bool? isRejected, int? page, int? pageSize, string search, string sort)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new SubjectsRequestDto()
            {
                UserId = userId,
                StudyId = studyId,
                SiteId = siteId,
                AffiliationId = affiliationId,
                GroupId = groupId,
                CohortId = cohortId,
                IsActive = isActive,
                IsRejected = isRejected,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/subjects/{id} Get subject by Id
         * @apiName GetSubjectById   
         * @apiVersion 1.0.0
         * @apiGroup Subjects
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
         * @api {post} api/v1/subjects Post subject
         * @apiName PostSubject
         * @apiVersion 1.0.0
         * @apiGroup Subjects
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Subject
         * @apiParamExample {json} Request-Example:
         *     {
                                  "RandomizedId": 0,
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
        public IActionResult Post([FromBody]SubjectFullDto request)
        {
            var validResult = Gateway.Valid(request);
            if (validResult.Result != null)
            {
                return new OkObjectResult(validResult);
            }
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/subjects/{id} Put subject
         * @apiName PutSubject
         * @apiVersion 1.0.0
         * @apiGroup Subjects
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "SiteId,RandomizedId,AlternativeRandomizedId,Laterality,Gender,BirthYear,EnrollmentDate,IsActive,IsValidated,IsRejected,IsTesting,IsDismissed" 
         *  }
         * @apiParam (Request Parameters) {Number}            Id=0 Subject identifier
         * @apiParam {JSON} Object modified of Subject
         * @apiParamExample {json} Request-Example:
         *     {
                                  "Id": 0,
                                  "RandomizedId": 0,
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
        public IActionResult Put(int id, [FromBody]SubjectFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var validResult = Gateway.Valid(request);
            if (validResult.Result != null)
            {
                return new OkObjectResult(validResult);
            }
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/subjects/{id} Delete subject
         * @apiName DeleteSubject
         * @apiVersion 1.0.0
         * @apiGroup Subjects
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                         Id=0 Subject identifier
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
