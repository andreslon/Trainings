using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;

using Excelsior.Business.Gateways;
using Excelsior.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class VisitMatrixController : Controller
    {
        public IVisitMatrixGateway Gateway { get; set; }

        public VisitMatrixController(IVisitMatrixGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/visitmatrix/subjects Get visit matrix subjects
        * @apiName GetVisitsMatrixSubjects
        * @apiVersion 1.0.0
        * @apiGroup Visit Matrix
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     siteId The unique identifier for the site
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of visit matrix subject JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": [
                    {
                      "id": 0,
                      "randomizedId": "",
                      "alternativeRandomizedId": "",
                      "nameCode": "",
                      "laterality": "",
                      "gender": "",
                      "birthYear": "",
                      "enrollmentDate": "",
                      "isActive": true,
                      "isValidated": false,
                      "isRejected": false,
                      "isTesting": false,
                      "isDismissed": false,
                      "timePoints": [
                        {
                          "id": 10121,
                          "description": "",
                          "index": 1,
                          "isInitial": false,
                          "isTerminal": false,
                          "isEligibility": false,
                          "expectedVisitStartDay": null,
                          "expectedVisitEndDay": null,
                          "status": "Completed"
                        }
                      ],
                      "group": null,
                      "cohort": null
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
        [Route("subjects")]
        public ResultInfo<IList<VisitMatrixSubjectFullDto>> GetSubjects(long? siteId, long? timePointId, long? procedureId, long? stepId, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new VisitMatrixSubjectsRequestDto()
            {
                UserId = userId,
                SiteId = siteId,
                TimePointId = timePointId,
                ProcedureId = procedureId,
                StepId = stepId,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            return Gateway.GetSubjects(request);
        }

        /**
        * @api {get} api/v1/visitmatrix/procedures Get visit matrix procedures
        * @apiName GetVisitsMatrixProcedures
        * @apiVersion 1.0.0
        * @apiGroup Visit Matrix
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     subjectId The unique identifier for the subject
        * @apiParam (Request Parameters) {Number}                                     timePointId The unique identifier for the timepoint
        * @apiSuccess {JSON} Result                                                   The array of visit matrix procedures JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": [
                    {
                        "id": 5,
                        "name": "FA",
                        "description": "Fluorescein Angiogram",
                        "status": "Completed",
                        "seriesId": 100906
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
        [Route("procedures")]
        public ResultInfo<IList<VisitMatrixProcedureFullDto>> GetProcedures(long? siteId, long? subjectId, long? timePointId)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new VisitMatrixProceduresRequestDto()
            {
                UserId = userId,
                SiteId = siteId,
                SubjectId = subjectId,
                TimePointId = timePointId
            };

            return Gateway.GetProcedures(request);
        }

        /**
        * @api {get} api/v1/visitmatrix/PendingTimePoints Get visit matrix Pending Time Points
        * @apiName GetVisitsMatrixPendingTimePoints
        * @apiVersion 1.0.0
        * @apiGroup Visit Matrix
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     subjectId The unique identifier for the subject
        * @apiParam (Request Parameters) {Number}                                     siteId The unique identifier for the site
        * @apiSuccess {JSON} Result                                                   The array of visit matrix procedures JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": [
                    {
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
        [Route("pendingtimepoints")]
        public ResultInfo<IList<TimePointFullDto>> GetPendingTimePoints(long? siteId, long? subjectId)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new VisitMatrixProceduresRequestDto()
            {
                UserId = userId,
                SiteId = siteId,
                SubjectId = subjectId,
            };

            return Gateway.GetPendingTimePoints(request);
        }

    }
}
