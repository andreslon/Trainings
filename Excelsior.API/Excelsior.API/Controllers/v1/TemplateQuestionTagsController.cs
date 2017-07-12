
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

 
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{

    [Authorize]
    [Route("api/v1/[controller]")]
    public class TemplateQuestionTagsController : Controller
    {
        public ITemplateQuestionTagsGateway Gateway { get; set; }

        public TemplateQuestionTagsController(ITemplateQuestionTagsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/TemplateQuestionTags Get all Template Question Tags
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Template Question Tags
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                     trialQuestionTagName="" trial question tag name
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of Template Question Tags JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                  "Id": 0,
                                  "Name": ""
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
        public IActionResult GetAll(string trialQuestionTagName, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new TemplateQuestionTagsRequestDto()
            {
                UserId = userId,
                Name = trialQuestionTagName,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/TemplateQuestionTags/{id} Get Template Question Tag by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Template Question Tags
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Template Question Tag identifier
         * @apiSuccess {JSON} Result The Template Question Tag JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": 
                                {
                                      "StudyQuestions": [
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
                                              "FirstSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectVisitDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "FirstDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                              "LockedDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "EndDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "StartDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                      "Id": 0,
                                      "Name": ""
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
         * @api {post} api/v1/TemplateQuestionTags Post Template Question Tag
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Template Question Tags
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Template Question Tag
         * @apiParamExample {json} Request-Example:
                                {
                                      "StudyQuestions": [
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
                                              "FirstSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectVisitDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "FirstDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                              "LockedDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "EndDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "StartDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                      "Id": 0,
                                      "Name": ""
                                    }
         * @apiSuccess {JSON} Result The Template Question Tag JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": 
                                {
                                      "StudyQuestions": [
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
                                              "FirstSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectVisitDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "FirstDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                              "LockedDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "EndDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "StartDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                      "Id": 0,
                                      "Name": ""
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
        public IActionResult Post([FromBody]TemplateQuestionTagFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/TemplateQuestionTags/{id} Put Template Question Tag
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Template Question Tags
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name" 
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Template Question Tag identifier
         * @apiParam {JSON} Object modified of Template Question Tag
         * @apiParamExample {json} Request-Example:
                                {
                                      "StudyQuestions": [
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
                                              "FirstSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectVisitDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "FirstDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                              "LockedDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "EndDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "StartDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                      "Id": 0,
                                      "Name": ""
                                    }
         * @apiSuccess {JSON} Result The Template Question Tag JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result":
                                {
                                      "StudyQuestions": [
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
                                              "FirstSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectEnrollDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastSubjectVisitDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "FirstDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "LastDataExportDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                              "LockedDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "EndDate": "2016-10-04T03:00:44.5632476+00:00",
                                              "StartDate": "2016-10-04T03:00:44.5632476+00:00",
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
                                      "Id": 0,
                                      "Name": ""
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
        public IActionResult Put(int id, [FromBody]TemplateQuestionTagFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/TemplateQuestionTags/{id} Delete Template Question Tag
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Template Question Tags
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Template Question Tag identifier
         * @apiSuccess {JSON} Result The Template Question Tag JSON object.
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
        public IActionResult Delete(int id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }
    }

}
