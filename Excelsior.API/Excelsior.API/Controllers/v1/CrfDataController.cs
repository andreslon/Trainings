using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{

    [Authorize]
    [Route("api/v1/[controller]")]
    public class CrfDataController : Controller
    {
        public ICrfDataGateway Gateway { get; set; }

        public CrfDataController(ICrfDataGateway gateway)
        {
            Gateway = gateway;
        }

        /**
       * @api {get} api/v1/CrfData Get all CRF Data
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup CRF Data 
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
       * @apiSuccess {JSON} Result                                                   The paginated array of Template Questions JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
       *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": [
                                {
                                  "Id": 0,
                                  "TemplateGroupId": 0,
                                  "AnswerTypeId": 0,
                                  "AnswerValidationId": 0,
                                  "Text": "",
                                  "CDashVariable": "",
                                  "SDTMVariable": "",
                                  "Description": "",
                                  "IsActive": false,
                                  "Index": 0,
                                  "AnswerType": {
                                    "Id": 0,
                                    "Name": "",
                                    "IsActive": false
                                  },
                                  "AnswerValidation": {
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
                                      "FirstSubjectEnrollDate": "2016-10-04T03:07:58.4892996+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T03:07:58.4892996+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T03:07:58.4892996+00:00",
                                      "FirstDataExportDate": "2016-10-04T03:07:58.4892996+00:00",
                                      "LastDataExportDate": "2016-10-04T03:07:58.4892996+00:00",
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
                                      "LockedDate": "2016-10-04T03:07:58.4892996+00:00",
                                      "EndDate": "2016-10-04T03:07:58.4892996+00:00",
                                      "StartDate": "2016-10-04T03:07:58.4892996+00:00",
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
        public IActionResult GetAll(long? studyId, long? seriesId, long? subjectId, long? timePointId, long? procedureId, bool? isActive, int? page, int? pageSize)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new CrfDataRequestDto()
            {
                StudyId = studyId,
                SeriesId = seriesId,
                SubjectId = subjectId,
                TimePointId = timePointId,
                ProcedureId = procedureId,
                IsActive = isActive,
                UserId = userId,
                Page = page,
                PageSize = pageSize,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/CrfData/{id} Get CRF Data by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup CRF Data
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Question identifier
        * @apiSuccess {JSON} Result The CRF Data JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "Answers": [
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
                                  ],
                                  "Id": 0,
                                  "TemplateGroupId": 0,
                                  "AnswerTypeId": 0,
                                  "AnswerValidationId": 0,
                                  "Text": "",
                                  "CDashVariable": "",
                                  "SDTMVariable": "",
                                  "Description": "",
                                  "IsActive": false,
                                  "Index": 0,
                                  "AnswerType": {
                                    "Id": 0,
                                    "Name": "",
                                    "IsActive": false
                                  },
                                  "AnswerValidation": {
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
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
                                      "LockedDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "EndDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "StartDate": "2016-10-04T02:56:36.8758987+00:00",
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
        * @api {post} api/v1/CrfData Post CRF Data
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup CRF Data
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Object created of Template Question
        * @apiParamExample {json} Request-Example:
                               {
                                  "Answers": [
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
                                  ],
                                  "Id": 0,
                                  "TemplateGroupId": 0,
                                  "AnswerTypeId": 0,
                                  "AnswerValidationId": 0,
                                  "Text": "",
                                  "CDashVariable": "",
                                  "SDTMVariable": "",
                                  "Description": "",
                                  "IsActive": false,
                                  "Index": 0,
                                  "AnswerType": {
                                    "Id": 0,
                                    "Name": "",
                                    "IsActive": false
                                  },
                                  "AnswerValidation": {
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
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
                                      "LockedDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "EndDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "StartDate": "2016-10-04T02:56:36.8758987+00:00",
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
        * @apiSuccess {JSON} Result The Template Question JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "Answers": [
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
                                  ],
                                  "Id": 0,
                                  "TemplateGroupId": 0,
                                  "AnswerTypeId": 0,
                                  "AnswerValidationId": 0,
                                  "Text": "",
                                  "CDashVariable": "",
                                  "SDTMVariable": "",
                                  "Description": "",
                                  "IsActive": false,
                                  "Index": 0,
                                  "AnswerType": {
                                    "Id": 0,
                                    "Name": "",
                                    "IsActive": false
                                  },
                                  "AnswerValidation": {
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
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
                                      "LockedDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "EndDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "StartDate": "2016-10-04T02:56:36.8758987+00:00",
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
		                    "key": "SeriesId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
	                    },
                        {
		                    "key": "TemplateId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
	                    }
                      ],
                      "pager": null
                    }
        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]CrfDataFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/CrfData/{id} Put CRF Data
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup CRF Data
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "SeriesId,TemplateId,VerifiedById,SignedById,DateVerified,DateSigned,IsActive" 
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Question identifier
        * @apiParam {JSON} Object modified of Template Question
        * @apiParamExample {json} Request-Example:
                               {
                                  "Answers": [
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
                                  ],
                                  "Id": 0,
                                  "TemplateGroupId": 0,
                                  "AnswerTypeId": 0,
                                  "AnswerValidationId": 0,
                                  "Text": "",
                                  "CDashVariable": "",
                                  "SDTMVariable": "",
                                  "Description": "",
                                  "IsActive": false,
                                  "Index": 0,
                                  "AnswerType": {
                                    "Id": 0,
                                    "Name": "",
                                    "IsActive": false
                                  },
                                  "AnswerValidation": {
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
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
                                      "LockedDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "EndDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "StartDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "TotalSubjects": 0,
                                      "AnimalSpeciesId": 0,
                                      "ImpressionId": 0
                                    },
                                    "Id": 0,
                                    "StudyId": 0,
                                    "Name": "",
                                    "IsActive": false
                                  }
                                }        * @apiSuccess {JSON} Result The Template Question JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                                  "Answers": [
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
                                  ],
                                  "Id": 0,
                                  "TemplateGroupId": 0,
                                  "AnswerTypeId": 0,
                                  "AnswerValidationId": 0,
                                  "Text": "",
                                  "CDashVariable": "",
                                  "SDTMVariable": "",
                                  "Description": "",
                                  "IsActive": false,
                                  "Index": 0,
                                  "AnswerType": {
                                    "Id": 0,
                                    "Name": "",
                                    "IsActive": false
                                  },
                                  "AnswerValidation": {
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "LastDataExportDate": "2016-10-04T02:56:36.8758987+00:00",
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
                                      "LockedDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "EndDate": "2016-10-04T02:56:36.8758987+00:00",
                                      "StartDate": "2016-10-04T02:56:36.8758987+00:00",
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
		                    "key": "SeriesId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
	                    },
                        {
		                    "key": "TemplateId",
		                    "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.Nullable`"
	                    }
                      ],
                      "pager": null
                    }
        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]CrfDataFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
        * @api {delete} api/v1/CrfData/{id} Delete CRF Data
        * @apiName Delete
        * @apiVersion 1.0.0
        * @apiGroup CRF Data
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Question identifier
        * @apiSuccess {JSON} Result The Template Question JSON object.
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
         * @api {get} api/v1/CrfData/{id}/results Get Results
         * @apiName GetResults
         * @apiVersion 1.0.0
         * @apiGroup CRF Data
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Template Question identifier
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
        [HttpGet("{id}/results")]
        [Route("{id}/results")]
        public IActionResult GetResults(long id)
        {
            var result = Gateway.GetResults(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/CrfData/{id}/results Post Results
        * @apiName PostResults
        * @apiVersion 1.0.0
        * @apiGroup CRF Data
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Question identifier
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
        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("{id}/results")]
        [Route("{id}/results")]
        public IActionResult PostResults(long id, [FromBody]IList<CrfDataResultFullDto> results)
        {
            var result = Gateway.SetResults(id, results);
            return new OkObjectResult(result);
        }
    }
}
