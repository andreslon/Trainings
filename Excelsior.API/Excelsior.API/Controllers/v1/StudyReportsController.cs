using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Excelsior.Business.Gateways.Interfaces;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.DtoEntities.Full;
namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class StudyReportsController : Controller
    {
        public IStudyReportsGateway Gateway { get; set; }

        public StudyReportsController(IStudyReportsGateway gateway)
        {
            Gateway = gateway;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll(long? studyId, int? page, int? pageSize)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new StudyReportRequestDto()
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize,
                StudyId = studyId
            };
            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(long id)
        {
            var result = Gateway.GetSingle(id);
            return new OkObjectResult(result);
        }
    }
}
