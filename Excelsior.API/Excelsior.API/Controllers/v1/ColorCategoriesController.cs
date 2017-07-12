using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ColorCategoriesController : Controller
    {
        public IColorCategoriesGateway Gateway { get; set; }

        public ColorCategoriesController(IColorCategoriesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/ColorCategories Get all Color Categories
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Color Categories
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of answer types JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                    "Id" : 0,
                                    "Name" : "Red",            
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
            var request = new ColorCategoriesRequestDto()
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
         * @api {get} api/v1/ColorCategories/{id} Get color category by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Color Categories
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Color Category identifier
         * @apiSuccess {JSON} Result The Color Category JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "Id" : 0,
                                    "Name" : "Color Category Name",
                                    "IsActive" : false
                            }
               }
         *
         */
        [HttpGet("{id}")]
        [Route("{id}")]
        public IActionResult GetSingle(long id)
        {
            var result = Gateway.GetSingle(id);
            return new OkObjectResult(result);
        }
    }
}
