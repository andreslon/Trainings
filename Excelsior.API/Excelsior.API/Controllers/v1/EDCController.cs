using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class EDCController : Controller
    {
        public IEDCGateway Gateway { get; set; }

        public EDCController(IEDCGateway gateway)
        {
            Gateway = gateway;
        }

        /**
         * @api {get} api/v1/EDC/gradingtemplate Get Grading Template For Procedure
         * @apiName gradingtemplate   
         * @apiVersion 1.0.0
         * @apiGroup EDC
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  procedureId=0 Procedure identifier
         * @apiParam (Request Parameters) {Number}                                  timePointListId=0 Time Point List identifier
         * @apiParam (Request Parameters) {Number}                                  isHierarchical=false Is Hierarchical
         * @apiSuccess {JSON} Result The Grading Templates JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                              "Groups": [
                                {
                                  "Questions": [
                                    {
                                      "Group": null,
                                      "Question": {
                                        "Tag": {
                                          "Id": 0,
                                          "Name": ""
                                        },
                                        "Answers": [
                                          {
                                            "Dependencies": [
                                              {
                                                "Id": 0,
                                                "IsActionEnable": false,
                                                "SourceAnswerID": 0,
                                                "TargetAnswerID": 0,
                                                "TargetQuestionID": 0
                                              }
                                            ],
                                            "Id": 0,
                                            "Text": "",
                                            "Index": 0,
                                            "TrialId": 0
                                          }
                                        ],
                                        "Id": 0,
                                        "Text": "",
                                        "Description": "",
                                        "IsAnswerMeasurement": false,
                                        "AnswerMask": ""
                                      },
                                      "Id": 0,
                                      "Index": 0
                                    }
                                  ],
                                  "Id": 0,
                                  "Name": "",
                                  "Index": 0
                                }
                              ],
                              "Questions": [
                                {
                                  "Group": null,
                                  "Question": {
                                    "Tag": {
                                      "Id": 0,
                                      "Name": ""
                                    },
                                    "Answers": [
                                      {
                                        "Dependencies": [
                                          {
                                            "Id": 0,
                                            "IsActionEnable": false,
                                            "SourceAnswerID": 0,
                                            "TargetAnswerID": 0,
                                            "TargetQuestionID": 0
                                          }
                                        ],
                                        "Id": 0,
                                        "Text": "",
                                        "Index": 0,
                                        "TrialId": 0
                                      }
                                    ],
                                    "Id": 0,
                                    "Text": "",
                                    "Description": "",
                                    "IsAnswerMeasurement": false,
                                    "AnswerMask": ""
                                  },
                                  "Id": 0,
                                  "Index": 0
                                }
                              ],
                              "Id": 0,
                              "Name": "",
                              "Description": "",
                              "IsActive": false,
                              "IsLocked": false
                            }
               }
         *
         */
        [HttpGet]
        [Route("gradingtemplate")]
        public ResultInfo<GradingTemplateFullDto> GetGradingTemplateForProcedure(long procedureId, long timePointListId, bool? isHierarchical)
        {
            return Gateway.GetGradingTemplateForProcedure(procedureId, timePointListId, isHierarchical.GetValueOrDefault(false));
        }

        /**
         * @api {get} api/v1/EDC/framelocation Get Frame Location
         * @apiName framelocation   
         * @apiVersion 1.0.0
         * @apiGroup EDC
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  subjectId=0 Subject identifier
         * @apiParam (Request Parameters) {Number}                                  timePointListId=0 Time Point List identifier
         * @apiParam (Request Parameters) {Number}                                  procedureId=0 Procedure identifier
         * @apiSuccess {JSON} Result The frame location JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": "Frame Location"
               }
         *
         */
        [HttpGet]
        [Route("framelocation")]
        public ResultInfo<string> GetFrameLocation(long subjectId, long timePointListId, long procedureId)
        {
            return Gateway.GetFrameLocation(subjectId, timePointListId, procedureId);
        }
    }
}
