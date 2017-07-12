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
    public class AnswerValidationsController : Controller
    {
        public IAnswerValidationsGateway Gateway { get; set; }

        public AnswerValidationsController(IAnswerValidationsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
       * @api {get} api/v1/AnswerValidations Get all Answer Validations
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup Answer Validations
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  }
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
       * @apiParam (Request Parameters) {String}                                     [search] Search text.
       * @apiSuccess {JSON} Result                                                   The paginated array of Answer Validations JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
       *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": [
                               {
                                  "Id": 0,
                                  "StudyId": 0,
                                  "Name": "",
                                  "IsActive": false
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
        public IActionResult GetAll(int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new AnswerValidationsRequestDto()
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/AnswerValidations/{id} Get Answer Validation by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup Answer Validations
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Answer Validation identifier
        * @apiSuccess {JSON} Result The Answer Validation JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                        {
                          "Description": "",
                          "Control": "",
                          "Mask": "",
                          "Max": "",
                          "Min": "",
                          "Tick": "",
                          "ToolTip": "",
                          "Unit": "",
                          "Study": {
                            "OtherDrugs": "",
                            "ProtocolTitle": "",
                            "Phase": "",
                            "Arm": "",
                            "DiseaseType": "",
                            "SubjectSeg": "",
                            "TherapeuticClass": "",
                            "Locations": "",
                            "SubjectIdMask": "",
                            "SubjectAlternativeIdMask": "",
                            "SubjectNameCodeMask": "",
                            "FirstSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectVisitDate": "2016-10-04T03:19:57.2274001+00:00",
                            "FirstDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "IsValidated": false,
                            "IsSubjectNameCodeRequired": false,
                            "IsEligibilityIdUsed": false,
                            "IsCompletedPublic": false,
                            "IsTestingPhase": false,
                            "IsEligibilityCloningEnabled": false,
                            "ShouldEligibleLateralityBeDetermined": false,
                            "IsSubjectGenderRequired": false,
                            "IsSubjectBirthYearRequired": false,
                            "AlwaysVerifyMultipleGrades": false,
                            "NeedCertification": false,
                            "Id": 0,
                            "Name": "",
                            "Alias": "",
                            "PrimaryDrugs": "",
                            "IsActive": false,
                            "IsLocked": false,
                            "LockedDate": "2016-10-04T03:19:57.2274001+00:00",
                            "EndDate": "2016-10-04T03:19:57.2274001+00:00",
                            "StartDate": "2016-10-04T03:19:57.2274001+00:00",
                            "TotalSubjects": 0,
                            "AnimalSpeciesId": 0,
                            "ImpressionId": 0
                          },
                          "Id": 0,
                          "StudyId": 0,
                          "Name": "",
                          "IsActive": false
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
        * @api {post} api/v1/AnswerValidations Post Answer Validation
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup Answer Validations
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Object created of Answer Validation
        * @apiParamExample {json} Request-Example:
                        {
                          "Description": "",
                          "Control": "",
                          "Mask": "",
                          "Max": "",
                          "Min": "",
                          "Tick": "",
                          "ToolTip": "",
                          "Unit": "",
                          "Study": {
                            "OtherDrugs": "",
                            "ProtocolTitle": "",
                            "Phase": "",
                            "Arm": "",
                            "DiseaseType": "",
                            "SubjectSeg": "",
                            "TherapeuticClass": "",
                            "Locations": "",
                            "SubjectIdMask": "",
                            "SubjectAlternativeIdMask": "",
                            "SubjectNameCodeMask": "",
                            "FirstSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectVisitDate": "2016-10-04T03:19:57.2274001+00:00",
                            "FirstDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "IsValidated": false,
                            "IsSubjectNameCodeRequired": false,
                            "IsEligibilityIdUsed": false,
                            "IsCompletedPublic": false,
                            "IsTestingPhase": false,
                            "IsEligibilityCloningEnabled": false,
                            "ShouldEligibleLateralityBeDetermined": false,
                            "IsSubjectGenderRequired": false,
                            "IsSubjectBirthYearRequired": false,
                            "AlwaysVerifyMultipleGrades": false,
                            "NeedCertification": false,
                            "Id": 0,
                            "Name": "",
                            "Alias": "",
                            "PrimaryDrugs": "",
                            "IsActive": false,
                            "IsLocked": false,
                            "LockedDate": "2016-10-04T03:19:57.2274001+00:00",
                            "EndDate": "2016-10-04T03:19:57.2274001+00:00",
                            "StartDate": "2016-10-04T03:19:57.2274001+00:00",
                            "TotalSubjects": 0,
                            "AnimalSpeciesId": 0,
                            "ImpressionId": 0
                          },
                          "Id": 0,
                          "StudyId": 0,
                          "Name": "",
                          "IsActive": false
                        }
        * @apiSuccess {JSON} Result The Answer Validation JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                        {
                          "Description": "",
                          "Control": "",
                          "Mask": "",
                          "Max": "",
                          "Min": "",
                          "Tick": "",
                          "ToolTip": "",
                          "Unit": "",
                          "Study": {
                            "OtherDrugs": "",
                            "ProtocolTitle": "",
                            "Phase": "",
                            "Arm": "",
                            "DiseaseType": "",
                            "SubjectSeg": "",
                            "TherapeuticClass": "",
                            "Locations": "",
                            "SubjectIdMask": "",
                            "SubjectAlternativeIdMask": "",
                            "SubjectNameCodeMask": "",
                            "FirstSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectVisitDate": "2016-10-04T03:19:57.2274001+00:00",
                            "FirstDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "IsValidated": false,
                            "IsSubjectNameCodeRequired": false,
                            "IsEligibilityIdUsed": false,
                            "IsCompletedPublic": false,
                            "IsTestingPhase": false,
                            "IsEligibilityCloningEnabled": false,
                            "ShouldEligibleLateralityBeDetermined": false,
                            "IsSubjectGenderRequired": false,
                            "IsSubjectBirthYearRequired": false,
                            "AlwaysVerifyMultipleGrades": false,
                            "NeedCertification": false,
                            "Id": 0,
                            "Name": "",
                            "Alias": "",
                            "PrimaryDrugs": "",
                            "IsActive": false,
                            "IsLocked": false,
                            "LockedDate": "2016-10-04T03:19:57.2274001+00:00",
                            "EndDate": "2016-10-04T03:19:57.2274001+00:00",
                            "StartDate": "2016-10-04T03:19:57.2274001+00:00",
                            "TotalSubjects": 0,
                            "AnimalSpeciesId": 0,
                            "ImpressionId": 0
                          },
                          "Id": 0,
                          "StudyId": 0,
                          "Name": "",
                          "IsActive": false
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
                      "key": "Name",
                      "ErrorMessage": "The Name field is required."
                    }
                  ],
                  "pager": null
                }  
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]AnswerValidationFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/AnswerValidations/{id} Put Answer Validation
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup Answer Validations
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "StudyId,Name,IsActive" 
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Answer Validation identifier
        * @apiParam {JSON} Object modified of Answer Validation
        * @apiParamExample {json} Request-Example:
                        {
                          "Description": "",
                          "Control": "",
                          "Mask": "",
                          "Max": "",
                          "Min": "",
                          "Tick": "",
                          "ToolTip": "",
                          "Unit": "",
                          "Study": {
                            "OtherDrugs": "",
                            "ProtocolTitle": "",
                            "Phase": "",
                            "Arm": "",
                            "DiseaseType": "",
                            "SubjectSeg": "",
                            "TherapeuticClass": "",
                            "Locations": "",
                            "SubjectIdMask": "",
                            "SubjectAlternativeIdMask": "",
                            "SubjectNameCodeMask": "",
                            "FirstSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectVisitDate": "2016-10-04T03:19:57.2274001+00:00",
                            "FirstDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "IsValidated": false,
                            "IsSubjectNameCodeRequired": false,
                            "IsEligibilityIdUsed": false,
                            "IsCompletedPublic": false,
                            "IsTestingPhase": false,
                            "IsEligibilityCloningEnabled": false,
                            "ShouldEligibleLateralityBeDetermined": false,
                            "IsSubjectGenderRequired": false,
                            "IsSubjectBirthYearRequired": false,
                            "AlwaysVerifyMultipleGrades": false,
                            "NeedCertification": false,
                            "Id": 0,
                            "Name": "",
                            "Alias": "",
                            "PrimaryDrugs": "",
                            "IsActive": false,
                            "IsLocked": false,
                            "LockedDate": "2016-10-04T03:19:57.2274001+00:00",
                            "EndDate": "2016-10-04T03:19:57.2274001+00:00",
                            "StartDate": "2016-10-04T03:19:57.2274001+00:00",
                            "TotalSubjects": 0,
                            "AnimalSpeciesId": 0,
                            "ImpressionId": 0
                          },
                          "Id": 0,
                          "StudyId": 0,
                          "Name": "",
                          "IsActive": false
                        }
        * @apiSuccess {JSON} Result The Answer Validation JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                        {
                          "Description": "",
                          "Control": "",
                          "Mask": "",
                          "Max": "",
                          "Min": "",
                          "Tick": "",
                          "ToolTip": "",
                          "Unit": "",
                          "Study": {
                            "OtherDrugs": "",
                            "ProtocolTitle": "",
                            "Phase": "",
                            "Arm": "",
                            "DiseaseType": "",
                            "SubjectSeg": "",
                            "TherapeuticClass": "",
                            "Locations": "",
                            "SubjectIdMask": "",
                            "SubjectAlternativeIdMask": "",
                            "SubjectNameCodeMask": "",
                            "FirstSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectEnrollDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastSubjectVisitDate": "2016-10-04T03:19:57.2274001+00:00",
                            "FirstDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "LastDataExportDate": "2016-10-04T03:19:57.2274001+00:00",
                            "IsValidated": false,
                            "IsSubjectNameCodeRequired": false,
                            "IsEligibilityIdUsed": false,
                            "IsCompletedPublic": false,
                            "IsTestingPhase": false,
                            "IsEligibilityCloningEnabled": false,
                            "ShouldEligibleLateralityBeDetermined": false,
                            "IsSubjectGenderRequired": false,
                            "IsSubjectBirthYearRequired": false,
                            "AlwaysVerifyMultipleGrades": false,
                            "NeedCertification": false,
                            "Id": 0,
                            "Name": "",
                            "Alias": "",
                            "PrimaryDrugs": "",
                            "IsActive": false,
                            "IsLocked": false,
                            "LockedDate": "2016-10-04T03:19:57.2274001+00:00",
                            "EndDate": "2016-10-04T03:19:57.2274001+00:00",
                            "StartDate": "2016-10-04T03:19:57.2274001+00:00",
                            "TotalSubjects": 0,
                            "AnimalSpeciesId": 0,
                            "ImpressionId": 0
                          },
                          "Id": 0,
                          "StudyId": 0,
                          "Name": "",
                          "IsActive": false
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
                      "key": "Name",
                      "ErrorMessage": "The Name field is required."
                    }
                  ],
                  "pager": null
                }  
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]AnswerValidationFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
        * @api {delete} api/v1/AnswerValidations/{id} Delete Answer Validation
        * @apiName Delete
        * @apiVersion 1.0.0
        * @apiGroup Answer Validations
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Answer Validation identifier
        * @apiSuccess {JSON} Result The Answer Validation JSON object.
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
    }
}
