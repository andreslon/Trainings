using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Excelsior.Business.Gateways.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class CommonController : Controller
    {
        public ICommonGateway Gateway { get; set; }
        public CommonController(ICommonGateway gateway)
        {
            Gateway = gateway;
        }
        /**
      * @api {get} api/v1/Common/GetCountries GetCountries
      * @apiName GetCountries
      * @apiVersion 1.0.0
      * @apiGroup Common
      *
      * @apiHeader (Header) {String} Authorization Authorization Bearer token.
      * @apiHeaderExample Header Example
      *  {
      *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
      *  }
      *
      * @apiSuccess {JSON} Result                                                   The paginated array of countries JSON objects.
      * @apiSuccessExample Success-Response
      *  HTTP/1.1 200 OK
      *       {
               "IsSuccess": true,
               "Message": "Successful",
               "Exception": "Exception Message",
               "Result": [
                             {
                                "Id": 0,
                                "Name": "Unite States"
                                }

                         ]
               "pager": null
             }
      *
      */
        [HttpGet]
        [Route("GetCountries")]
        public IActionResult GetCountries()
        {
            var result = Gateway.GetCountries();
            return new OkObjectResult(result);
        }
        /**
        * @api {get} api/v1/Common/GetSecurityQuestions Get Security Questions
        * @apiName GetSecurityQuestions
        * @apiVersion 1.0.0
        * @apiGroup Common
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiSuccess {JSON} Result                                                   The paginated array of countries JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
               "IsSuccess": true,
               "Message": "Successful",
               "Exception": "Exception Message",
               "Result": [
                             "????","?????"
                         ]
               "pager": null
             }
        *
        */
        [HttpGet]
        [Route("GetSecurityQuestions")]
        public IActionResult GetSecurityQuestions()
        {
            var result = Gateway.GetSecurityQuestions();
            return new OkObjectResult(result);
        }

    }
}
