using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ProcessedDataController : Controller
    {
        public IProcessedDataGateway Gateway { get; set; }

        public ProcessedDataController(IProcessedDataGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/ProcessedData Get Processed Data
        * @apiName Get   
        * @apiVersion 0.0.0
        * @apiGroup ProcessedData
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                       MediaId=0 MediaId Identifier
        * 
        * @apiSuccess {JSON} Result                                    boolean result  of operation included in json object 
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                      "IsSuccess": false,
                      "Message": "successful"
                      "Exception": "Error",
                      "Result": {
                              "mediaId": 0,
                              "label": "Layers",
                              "xmlString": "",
                            } 
                 }
        *
        */
        [HttpGet]
        public IActionResult GetAll(long mediaId)
        {
            var request = new ProcessedDataRequestDto()
            {
                MediaId = mediaId,
            };
            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/ProcessedData Post Processed Data
        * @apiName Post   
        * @apiVersion 0.0.0
        * @apiGroup ProcessedData
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                       MediaId=0 mediaId Identifier
        * @apiParam (Request Parameters) {String}                       Label Label of Processed data
        * @apiParam (Request Parameters) {String}                       XmlString String XML of Processed data
        * 
        * @apiParamExample {json} Request-Example:
            {
              "MediaId": 0,
              "Label": "Layers",
              "ProcessDataXMLString": "",
            } 
        * 
        * @apiSuccess {JSON} Result                                    boolean result  of operation included in json object 
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                      "id": 20536,
                      "mediaId": 6,
                      "label": "Test",
                      "xmlString": "TEST",
                      "userId": 2,
                      "frameId": null,
                      "dateCreated": "2017-03-03T22:52:47.86",
                      "dateModified": "2017-03-03T22:52:47.86"
                   },
                   "pager": null
                 }
        *
        */

        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]ProcessedDataFullDto request)
        {
            string currentUserId = null;
            if (!request.UserId.HasValue)
            {
                currentUserId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            }
            var result = Gateway.Add(request, currentUserId);
            return new OkObjectResult(result);
        }
        
        
    }
}
