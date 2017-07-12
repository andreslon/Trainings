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
    public class SchedulingController : Controller
    {
        public ISchedulingGateway Gateway { get; set; }

        public SchedulingController(ISchedulingGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/scheduling/procedures Get scheduling procedures
        * @apiName GetSchedulingProcedures
        * @apiVersion 1.0.0
        * @apiGroup Scheduling
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     studyId The study id
        * @apiParam (Request Parameters) {Number}                                     scheduled get only scheduled procedures
        * @apiSuccess {JSON} Result                                                   The array of scheduling procedures JSON objects.
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
        public ResultInfo<IList<SchedulingProcedureFullDto>> GetProcedures(long studyId, bool? scheduled, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new SchedulingProceduresRequestDto()
            {
                UserId = userId,
                StudyId = studyId,
                Scheduled = scheduled,
                Page = page,
                PageSize = pageSize,
                Search = search
            };

            return Gateway.GetProcedures(request);
        }

    }
}
