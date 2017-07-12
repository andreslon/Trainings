using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class StudiesController : Controller
    {
        public IStudiesGateway Gateway { get; set; }

        public StudiesController(IStudiesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/studies Get studies
        * @apiName GetStudies
        * @apiVersion 1.0.0
        * @apiGroup Studies
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active trials?
        * @apiParam (Request Parameters) {Boolean=true,false}                         isLocked=false Get only locked trials?
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of study JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                  "id": 0,
                                  "alias": null,
                                  "name": null,
                                  "primaryDrugs": null,
                                  "isActive": false,
                                  "isLocked": false,
                                  "lockedDate": null
                                  "endDate": null,
                                  "startDate": null,
                                  "totalSubjects": 0,
                                  "animalSpecies": null,
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
        public IActionResult GetAll(bool? isActive, bool? isLocked, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new StudiesRequestDto()
            {
                UserId = userId,
                IsActive = isActive,
                IsLocked = isLocked,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/studies/{id} Get study by Id
         * @apiName GetStudyById   
         * @apiVersion 1.0.0
         * @apiGroup Studies
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
                          "Result": {
                                      "Id": null,
                                      "Name": null,
                                      "Alias": null,
                                      "PrimaryDrugs": null,
                                      "IsActive": null,
                                      "IsLocked": null,
                                      "LockedDate": null,
                                      "EndDate": null,
                                      "StartDate": null,
                                      "TotalSubjects": null,
                                      "AnimalSpecies":{
                                                          "Id": null,
                                                          "Name": null,
                                                          "DisplayName": null
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
         * @api {post} api/v1/studies Post study
         * @apiName PostStudy
         * @apiVersion 1.0.0
         * @apiGroup Studies
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Study
         * @apiParamExample {json} Request-Example:
         *     {
                  "IsActive": null,
                  "IsLocked": null,
                  "Id": 0,
                  "Name": null,
                  "Alias": null,
                  "ProtocolTitle": null,
                  "Phase": null,
                  "Arm": null,
                  "DiseaseType": null,
                  "SubjectSeg": null,
                  "PrimaryDrugs": null,
                  "OtherDrugs": null,
                  "TherapeuticClass": null,
                  "Locations": null,
                  "NeedCertification": false,
                  "StartDate": null,
                  "FirstSubjectEnrollDate": null,
                  "LastSubjectEnrollDate": null,
                  "LastSubjectVisitDate": null,
                  "FirstDataExportDate": null,
                  "LastDataExportDate": null,
                  "EndDate": null,
                  "IsValidated": false,
                  "IsSubjectNameCodeRequired": false,
                  "IsEligibilityIdUsed": false,
                  "IsCompletedPublic": false,
                  "IsTestingPhase": false,
                  "IsEligibilityCloningEnabled": false,
                  "ShouldEligibleLateralityBeDetermined": false,
                  "IsSubjectGenderRequired": false,
                  "SubjectIdMask": null,
                  "SubjectAlternativeIdMask": null,
                  "SubjectNameCodeMask": null,
                  "IsSubjectBirthYearRequired": false,
                  "AnimalSpeciesId": null,
                  "ImpressionId": null,
                  "AlwaysVerifyMultipleGrades": false,
                  "LockedDate": null
                }
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": false
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
                                  "key": "Name",
                                  "ErrorMessage": "The Name field is required."
                                }
                  ],
                  "pager": null
                } 

         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]StudyFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/studies/{id} Put study
         * @apiName PutStudy
         * @apiVersion 1.0.0
         * @apiGroup Studies
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name,Alias,PrimaryDrugs,IsActive,IsLocked,LockedDate,EndDate,StartDate,AnimalSpeciesId,ImpressionId"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Series identifier
         * @apiParam {JSON} Object modified of Study
         * @apiParamExample {json} Request-Example:
         *     {
                  "IsActive": null,
                  "IsLocked": null,
                  "Id": 0,
                  "Name": null,
                  "Alias": null,
                  "ProtocolTitle": null,
                  "Phase": null,
                  "Arm": null,
                  "DiseaseType": null,
                  "SubjectSeg": null,
                  "PrimaryDrugs": null,
                  "OtherDrugs": null,
                  "TherapeuticClass": null,
                  "Locations": null,
                  "NeedCertification": false,
                  "StartDate": null,
                  "FirstSubjectEnrollDate": null,
                  "LastSubjectEnrollDate": null,
                  "LastSubjectVisitDate": null,
                  "FirstDataExportDate": null,
                  "LastDataExportDate": null,
                  "EndDate": null,
                  "IsValidated": false,
                  "IsSubjectNameCodeRequired": false,
                  "IsEligibilityIdUsed": false,
                  "IsCompletedPublic": false,
                  "IsTestingPhase": false,
                  "IsEligibilityCloningEnabled": false,
                  "ShouldEligibleLateralityBeDetermined": false,
                  "IsSubjectGenderRequired": false,
                  "SubjectIdMask": null,
                  "SubjectAlternativeIdMask": null,
                  "SubjectNameCodeMask": null,
                  "IsSubjectBirthYearRequired": false,
                  "AnimalSpeciesId": null,
                  "ImpressionId": null,
                  "AlwaysVerifyMultipleGrades": false,
                  "LockedDate": null
                }
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": false
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
                                  "key": "Name",
                                  "ErrorMessage": "The Name field is required."
                                }
                  ],
                  "pager": null
                } 

         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]StudyFullDto request, [FromHeader]string fields, [FromHeader]string password, [FromHeader]string reason)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields, password, reason);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/studies/{id} Delete study
         * @apiName DeleteStudy
         * @apiVersion 1.0.0
         * @apiGroup Studies
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
                          "Result": false
                        }
         *
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/studies/{id}/lock Lock study
         * @apiName LockStudy
         * @apiVersion 1.0.0
         * @apiGroup Studies
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name,Alias,PrimaryDrugs,IsActive,IsLocked,LockedDate,EndDate,StartDate,AnimalSpeciesId,ImpressionId"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Series identifier
         * @apiParam {JSON} Object modified of Study
         * @apiParamExample {json} Request-Example:
         *     {
                  "IsActive": null,
                  "IsLocked": null,
                  "Id": 0,
                  "Name": null,
                  "Alias": null,
                  "ProtocolTitle": null,
                  "Phase": null,
                  "Arm": null,
                  "DiseaseType": null,
                  "SubjectSeg": null,
                  "PrimaryDrugs": null,
                  "OtherDrugs": null,
                  "TherapeuticClass": null,
                  "Locations": null,
                  "NeedCertification": false,
                  "StartDate": null,
                  "FirstSubjectEnrollDate": null,
                  "LastSubjectEnrollDate": null,
                  "LastSubjectVisitDate": null,
                  "FirstDataExportDate": null,
                  "LastDataExportDate": null,
                  "EndDate": null,
                  "IsValidated": false,
                  "IsSubjectNameCodeRequired": false,
                  "IsEligibilityIdUsed": false,
                  "IsCompletedPublic": false,
                  "IsTestingPhase": false,
                  "IsEligibilityCloningEnabled": false,
                  "ShouldEligibleLateralityBeDetermined": false,
                  "IsSubjectGenderRequired": false,
                  "SubjectIdMask": null,
                  "SubjectAlternativeIdMask": null,
                  "SubjectNameCodeMask": null,
                  "IsSubjectBirthYearRequired": false,
                  "AnimalSpeciesId": null,
                  "ImpressionId": null,
                  "AlwaysVerifyMultipleGrades": false,
                  "LockedDate": null
                }
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": false
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
                                  "key": "Name",
                                  "ErrorMessage": "The Name field is required."
                                }
                  ],
                  "pager": null
                } 

         */
        [HttpPost("{id}/lock")]
        public IActionResult Lock(long id, [FromHeader]string password, [FromHeader]string reason)
        {
            var result = Gateway.SetIsLocked(id, true, password, reason);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/studies/{id}/unlock Unlock study
         * @apiName UnlockStudy
         * @apiVersion 1.0.0
         * @apiGroup Studies
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name,Alias,PrimaryDrugs,IsActive,IsLocked,LockedDate,EndDate,StartDate,AnimalSpeciesId,ImpressionId"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Series identifier
         * @apiParam {JSON} Object modified of Study
         * @apiParamExample {json} Request-Example:
         *     {
                  "IsActive": null,
                  "IsLocked": null,
                  "Id": 0,
                  "Name": null,
                  "Alias": null,
                  "ProtocolTitle": null,
                  "Phase": null,
                  "Arm": null,
                  "DiseaseType": null,
                  "SubjectSeg": null,
                  "PrimaryDrugs": null,
                  "OtherDrugs": null,
                  "TherapeuticClass": null,
                  "Locations": null,
                  "NeedCertification": false,
                  "StartDate": null,
                  "FirstSubjectEnrollDate": null,
                  "LastSubjectEnrollDate": null,
                  "LastSubjectVisitDate": null,
                  "FirstDataExportDate": null,
                  "LastDataExportDate": null,
                  "EndDate": null,
                  "IsValidated": false,
                  "IsSubjectNameCodeRequired": false,
                  "IsEligibilityIdUsed": false,
                  "IsCompletedPublic": false,
                  "IsTestingPhase": false,
                  "IsEligibilityCloningEnabled": false,
                  "ShouldEligibleLateralityBeDetermined": false,
                  "IsSubjectGenderRequired": false,
                  "SubjectIdMask": null,
                  "SubjectAlternativeIdMask": null,
                  "SubjectNameCodeMask": null,
                  "IsSubjectBirthYearRequired": false,
                  "AnimalSpeciesId": null,
                  "ImpressionId": null,
                  "AlwaysVerifyMultipleGrades": false,
                  "LockedDate": null
                }
         * @apiSuccess {JSON} Result The Study JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": false
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
                                  "key": "Name",
                                  "ErrorMessage": "The Name field is required."
                                }
                  ],
                  "pager": null
                } 

         */
        [HttpPost("{id}/unlock")]
        public IActionResult Unlock(long id, [FromHeader]string password, [FromHeader]string reason)
        {
            var result = Gateway.SetIsLocked(id, false, password, reason);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/Studies/{id}/procedures Get Procedures
         * @apiName GetProcedures
         * @apiVersion 1.0.0
         * @apiGroup Studies
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Study identifier
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
        [HttpGet("{id}/procedures")]
        [Route("{id}/procedures")]
        public IActionResult GetProcedures(long id)
        {
            var result = Gateway.GetProcedures(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/Studies/{id}/procedures Post Procedures
        * @apiName PostProcedures
        * @apiVersion 1.0.0
        * @apiGroup Studies
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Study identifier
        * @apiParam {JSON} Object created of Study
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
        [HttpPost("{id}/procedures")]
        [Route("{id}/procedures")]
        public IActionResult PostProcedures(long id, [FromBody]IList<ProcedureFullDto> procedures)
        {
            var result = Gateway.SetProcedures(id, procedures);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/Studies/{id}/procedures Add Procedures
        * @apiName AddProcedures
        * @apiVersion 1.0.0
        * @apiGroup Studies
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Study identifier
        * @apiParam {JSON} Object created of Study
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
        [HttpPut("{id}/procedures")]
        [Route("{id}/procedures")]
        public IActionResult PutProcedures(long id, [FromBody]IList<ProcedureFullDto> procedures)
        {
            var result = Gateway.AddProcedures(id, procedures);
            return new OkObjectResult(result);
        }

        /**
        * @api {delete} api/v1/Studies/{id}/procedures Remove Procedures
        * @apiName RemoveProcedures
        * @apiVersion 1.0.0
        * @apiGroup Studies
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Study identifier
        * @apiParam {JSON} Object created of Study
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
        [HttpDelete("{id}/procedures")]
        [Route("{id}/procedures")]
        public IActionResult DeleteProcedures(long id, [FromBody]IList<ProcedureFullDto> procedures)
        {
            var result = Gateway.RemoveProcedures(id, procedures);
            return new OkObjectResult(result);
        }
    }
}