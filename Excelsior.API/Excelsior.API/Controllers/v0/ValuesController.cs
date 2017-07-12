using Excelsior.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.API.Controllers.v0
{
    [Authorize]
    [Route("api/v0/[controller]")]
    //[Authorize]
    public class ValuesController : Controller
    {

        public ValuesController(DataModel context)
        {
            var u = context.CONTACT_Users.ToList();
            string da = "";
        }
        /**
        * @api {get} api/v0/value Request values information
        * @apiName Get        
        * @apiVersion 0.0.0
        * @apiGroup Value
        * @apiSuccess {Object[]} string List of values
        * @apiSuccessExample Success-Response:
        *     HTTP/1.1 200 OK
        *     [
        *       "value1",
        *       "value2"
        *     ]
        */
        [HttpGet]
        public IEnumerable<string> Get()
        {
          
            return new string[] { "value1", "value2" };
        }

        /**
        * @api {get} api/v0/value/{id} Request value information by id
        * @apiName GetById
        * @apiVersion 0.0.0
        * @apiGroup Value
        * @apiParam {Number} id Value identifier
        * @apiSuccess {String} string value finded
        * @apiSuccessExample Success-Response:
        *     HTTP/1.1 200 OK
        *       "value1"
        */
        [HttpGet("{id}")]
        public string GetById(int id)
        {
            return "value";
        }

        /**
        * @api {post} api/v0/value/ Add value information
        * @apiName Post
        * @apiVersion 0.0.0
        * @apiGroup Value
        * @apiParam {String} value description.
        * @apiSuccessExample Success-Response:
        *     HTTP/1.1 200 OK
        */
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /**
        * @api {put} api/v0/value/:id Modify value information
        * @apiName Put
        * @apiVersion 0.0.0
        * @apiGroup Value
        * @apiParam {Number} value identifier
        * @apiParam {String} value description.
        * @apiSuccessExample Success-Response:
        *     HTTP/1.1 200 OK
        */

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /**
        * @api {delete} api/v0/value/:id Delete value information by id
        * @apiVersion 0.0.0
        * @apiName Delete
        * @apiGroup Value
        * @apiParam {Number} value identifier
        * @apiSuccessExample Success-Response:
        *     HTTP/1.1 200 OK
        *       "value1"
        */
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
