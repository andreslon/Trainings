using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AdminController : Controller
    {
        public ISchedulesGateway tpProcListGateway { get; set; }
        public IMediaTypesGateway mediaTypesGateway { get; set; }
        public IWorkflowStepsGateway wfStepsGateway { get; set; }

        public AdminController(
            IGradingTemplatesGateway grdtGateway,
            IWorkflowStepsGateway wfsGateway,
            IMediaTypesGateway dtGateway,
            ISchedulesGateway plGateway)
        {
            wfStepsGateway = wfsGateway;
            mediaTypesGateway = dtGateway;
            tpProcListGateway = plGateway;
        }

        /**
        * @api {get} api/v1/Admin Get Procedure Schedule
        * @apiName procedureschedules
        * @apiVersion 1.0.0
        * @apiGroup Admin
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                     studyId=0 Study Id
        * @apiSuccess {JSON} Result                                   The paginated array of time points  JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                  "Id": 0,
                                  "TimePointsListID": 0,
                                  "ProcedureID": 0,
                                  "WFTemplateID": 0,
                                  "GTemplateID": 0,
                                  "CRFTemplateID": 0,
                                  "IsGradeBothLaterality": false,
                                  "PercentSeriesForReview": 0,
                                  "CounterSeriesForReview": 0,
                                  "CounterSeriesSigned": 0,
                                  "GTemplateName": "",
                                  "WFTemplateName": ""
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
        [Route("procedureschedules")]
        public ResultInfo<IList<ScheduleBaseDto>> GetProcedureSchedule(long studyId)
        {
            var request = new SchedulesRequestDto()
            {
                StudyId = studyId
            };
            return tpProcListGateway.GetAll(request);
        }

        /**
        * @api {get} api/v1/Admin/mediatypes Get Media Types
        * @apiName mediatypes
        * @apiVersion 1.0.0
        * @apiGroup Admin
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiSuccess {JSON} Result                                   The paginated array of data types JSON objects.
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
        [Route("mediatypes")]
        public ResultInfo<IList<MediaTypeBaseDto>> GetMediaTypes()
        {
            var request = new MediaTypesRequestDto()
            {
            };
            return mediaTypesGateway.GetAll(request);
        }

        /**
        * @api {get} api/v1/Admin/workflowsteps Get Work flow Steps
        * @apiName workflowsteps
        * @apiVersion 1.0.0
        * @apiGroup Admin
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiSuccess {JSON} Result                                   The paginated array of work flow steps JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                              { 
                                  "Id": 0,
                                  "Description": "",
                                  "Index": 0
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
        [Route("workflowsteps")]
        public ResultInfo<IList<WorkflowStepBaseDto>> GetWorkflowSteps()
        {
            var request = new WorkflowStepsRequestDto()
            {
            };
            return wfStepsGateway.GetAll(request);
        }
    }
}
