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
    public class TemplateQuestionsController : Controller
    {
        public ITemplateQuestionsGateway Gateway { get; set; }

        public TemplateQuestionsController(ITemplateQuestionsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
       * @api {get} api/v1/TemplateQuestions Get all Template Questions
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup Template Questions
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
        public IActionResult GetAll(long? templateId, bool? isActive, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new TemplateQuestionsRequestDto()
            {
                TemplateId = templateId,
                IsActive = isActive,
                UserId = userId,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/TemplateQuestions/{id} Get Template Question by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup Template Questions
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
        * @api {post} api/v1/TemplateQuestions Post Template Question
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup Template Questions
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
        [Route("")]
        public IActionResult Post([FromBody]TemplateQuestionFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/TemplateQuestions/{id} Put Template Question
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup Template Questions
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "TemplateGroupId,AnswerTypeId,AnswerValidationId,Text,CDashVariable,SDTMVariable,Description,IsLaterality,IsActive,Index" 
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
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]TemplateQuestionFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
        * @api {delete} api/v1/TemplateQuestions/{id} Delete Template Question
        * @apiName Delete
        * @apiVersion 1.0.0
        * @apiGroup Template Questions
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
         * @api {get} api/v1/TemplateQuestions/{id}/answers Get Answers
         * @apiName GetAnswers
         * @apiVersion 1.0.0
         * @apiGroup Template Questions
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
        [HttpGet("{id}/answers")]
        [Route("{id}/answers")]
        public IActionResult GetAnswers(long id)
        {
            var result = Gateway.GetAnswers(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/TemplateQuestions/{id}/answers Post Answers
        * @apiName PostAnswers
        * @apiVersion 1.0.0
        * @apiGroup Template Questions
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
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                {
                  "isSuccess": false,
                  "message": "Invalid Model",
                  "exception": "",
                  "result": [
	                            {
		                            "key": "TemplateQuestionId",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("{id}/answers")]
        [Route("{id}/answers")]
        public IActionResult PostAnswers(long id, [FromBody]IList<TemplateAnswerFullDto> answers)
        {
            var result = Gateway.SetAnswers(id, answers);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/TemplateQuestions/{id}/tags Get Tags
         * @apiName GetTags
         * @apiVersion 1.0.0
         * @apiGroup Template Questions
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
        [HttpGet("{id}/tags")]
        [Route("{id}/tags")]
        public IActionResult GetTags(long id)
        {
            var result = Gateway.GetTags(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/TemplateQuestions/{id}/tags Post Tags
        * @apiName PostTags
        * @apiVersion 1.0.0
        * @apiGroup Template Questions
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
        [HttpPost("{id}/tags")]
        [Route("{id}/tags")]
        public IActionResult PostTags(long id, [FromBody]IList<TemplateQuestionTagFullDto> tags)
        {
            var result = Gateway.SetTags(id, tags);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/TemplateQuestions/{id}/validation Post Answer Validation
        * @apiName PostValidation
        * @apiVersion 1.0.0
        * @apiGroup Template Questions
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
        [HttpPost("{id}/validation")]
        [Route("{id}/validation")]
        public IActionResult PostValidation(long id, [FromBody]AnswerValidationFullDto validation)
        {
            var result = Gateway.SetValidation(id, validation);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/TemplateQuestions/{id}/clone Clone Template Questions
         * @apiName PostClone
         * @apiVersion 1.0.0
         * @apiGroup Template Questions
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Workflow Template
         * @apiParamExample {json} Request-Example:
                            {
                                   "id": 0,  //the group id
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
