using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Excelsior.Business.DtoEntities;

using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Base;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class GradingTemplatesController : Controller
    {
        public IGradingTemplatesGateway Gateway { get; set; }

        public GradingTemplatesController(IGradingTemplatesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
         * @api {get} api/v1/GradingTemplates Get Grading Templates
         * @apiName GetAll   
         * @apiVersion 1.0.0
         * @apiGroup GradingTemplates
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                     trialId=0 Procedure identifier
         * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
         * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
         * @apiParam (Request Parameters) {String}                                     [search] Search text.
         * @apiSuccess {JSON} Result The Grading Templates JSON object.
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
                                  "Description": "",
                                  "IsActive": false,
                                  "IsLocked": false
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
        public ResultInfo<IList<GradingTemplateBaseDto>> GetAll(long studyId, int? page, int? pageSize, string search)
        {
            var request = new GradingTemplatesRequestDto()
            {
                StudyId = studyId,
                Page = page,
                PageSize = pageSize,
                Search = search
            };
            return Gateway.GetAll(request);
        }

        /**
         * @api {get} api/v1/GradingTemplates/{id} Get Grading Template By Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup GradingTemplates
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  id=0 Grading template identifier
         * @apiSuccess {JSON} Result The Grading Template JSON object.
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
        [Route("{id}")]
        public IActionResult GetSingle(long id)
        {
            var result = Gateway.GetSingle(id);
            return new OkObjectResult(result);
        }
    }
}
