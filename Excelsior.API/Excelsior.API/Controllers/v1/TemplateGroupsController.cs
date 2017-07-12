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
    public class TemplateGroupsController : Controller
    {
        public ITemplateGroupsGateway Gateway { get; set; }

        public TemplateGroupsController(ITemplateGroupsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
       * @api {get} api/v1/TemplateGroups Get all Template Groups
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup Template Groups
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
       * @apiSuccess {JSON} Result                                                   The paginated array of Template Groups JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
       *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": [
                              {
                                  "Id": 0,
                                  "Name": "",
                                  "Index": 0,
                                  "TemplateId": 0
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
            var request = new TemplateGroupsRequestDto()
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
        * @api {get} api/v1/TemplateGroups/{id} Get Template Group by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup Template Groups
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Group identifier
        * @apiSuccess {JSON} Result The Template Group JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": {
                              "TemplateQuestions": [
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
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
                                      "LockedDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "EndDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "StartDate": "2016-10-04T02:53:58.8612752+00:00",
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
                              "Name": "",
                              "Index": 0,
                              "TemplateId": 0
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
        * @api {post} api/v1/TemplateGroups Post Template Group
        * @apiName Post
        * @apiVersion 1.0.0
        * @apiGroup Template Groups
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {JSON} Object created of Template Group
        * @apiParamExample {json} Request-Example:
                           {
                              "TemplateQuestions": [
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
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
                                      "LockedDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "EndDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "StartDate": "2016-10-04T02:53:58.8612752+00:00",
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
                              "Name": "",
                              "Index": 0,
                              "TemplateId": 0
                            }
        * @apiSuccess {JSON} Result The Template Group JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": {
                              "TemplateQuestions": [
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
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
                                      "LockedDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "EndDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "StartDate": "2016-10-04T02:53:58.8612752+00:00",
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
                              "Name": "",
                              "Index": 0,
                              "TemplateId": 0
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
        public IActionResult Post([FromBody]TemplateGroupFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/TemplateGroups/{id} Put Template Group
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup Template Groups
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "Name,Index,TemplateId" 
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Group identifier
        * @apiParam {JSON} Object modified of Template Group
        * @apiParamExample {json} Request-Example:
                           {
                              "TemplateQuestions": [
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
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
                                      "LockedDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "EndDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "StartDate": "2016-10-04T02:53:58.8612752+00:00",
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
                              "Name": "",
                              "Index": 0,
                              "TemplateId": 0
                            }
        * @apiSuccess {JSON} Result The Template Group JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": {
                              "TemplateQuestions": [
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
                                      "FirstSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectEnrollDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastSubjectVisitDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "FirstDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "LastDataExportDate": "2016-10-04T02:53:58.8612752+00:00",
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
                                      "LockedDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "EndDate": "2016-10-04T02:53:58.8612752+00:00",
                                      "StartDate": "2016-10-04T02:53:58.8612752+00:00",
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
                              "Name": "",
                              "Index": 0,
                              "TemplateId": 0
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
        public IActionResult Put(int id, [FromBody]TemplateGroupFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/TemplateGroups/{id} Delete Template Group
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Template Groups
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Template Group identifier
         * @apiSuccess {JSON} Result The Template Group JSON object.
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

        /**
         * @api {get} api/v1/TemplateGroups/{id}/questions Get Questions
         * @apiName GetQuestions
         * @apiVersion 1.0.0
         * @apiGroup Template Groups
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Template identifier
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
        [HttpGet("{id}/questions")]
        [Route("{id}/questions")]
        public IActionResult GetQuestions(long id)
        {
            var result = Gateway.GetQuestions(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/TemplateGroups/{id}/questions Post Questions
        * @apiName PostQuestions
        * @apiVersion 1.0.0
        * @apiGroup Template Groups
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
		                            "key": "TemplateGroupId",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("{id}/questions")]
        [Route("{id}/questions")]
        public IActionResult PostQuestions(long id, [FromBody]IList<TemplateQuestionFullDto> questions)
        {
            var result = Gateway.SetQuestions(id, questions);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/TemplateGroups/{id}/dependencies Get Dependencies
         * @apiName GetDependencies
         * @apiVersion 1.0.0
         * @apiGroup Template Groups
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id Template Group identifier
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
        [HttpGet("{id}/dependencies")]
        [Route("{id}/dependencies")]
        public IActionResult GetDependencies(long id)
        {
            var result = Gateway.GetDependencies(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/TemplateGroups/{id}/dependencies Post Dependencies
        * @apiName PostDependencies
        * @apiVersion 1.0.0
        * @apiGroup Template Groups
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id=0 Template Group identifier
        * @apiParam {JSON} Object created of Template Group
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
		                            "key": "TemplateGroupId",
		                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
	                            }
                  ],
                  "pager": null
                } 

        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost("{id}/dependencies")]
        [Route("{id}/dependencies")]
        public IActionResult PostDependencies(long id, [FromBody]IList<TemplateDependencyFullDto> dependencies)
        {
            var result = Gateway.SetDependencies(id, dependencies);
            return new OkObjectResult(result);
        }
    }
}
