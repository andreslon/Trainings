using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Excelsior.Business.Gateways;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.DtoEntities.Full;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ProceduresController : Controller
    {
        public IProceduresGateway Gateway { get; set; }

        public ProceduresController(IProceduresGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/Procedures Get all procedures
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Procedures
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
        * @apiSuccess {JSON} Result                                                   The paginated array of procedures JSON objects.
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
                              "DICOMAcquisitionDeviceCodeSchemeDesignator": "",
                              "DICOMAcquisitionDeviceCodeValue": "",
                              "DICOMAcquisitionDeviceCodeMeaning": "",
                              "DICOMAnatomicStructureCodeSchemeDesignator": "",
                              "DICOMAnatomicStructureCodeValue": "",
                              "DICOMAnatomicStructureCodeMeaning": "",
                              "DataTypeID": 0
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
        public ResultInfo<IList<ProcedureBaseDto>> GetAll(int? page, int? pageSize, string search)
        {
            var request = new ProceduresRequestDto()
            {
                Page = page,
                PageSize = pageSize,
                Search = search
            };
            return Gateway.GetAll(request);
        }

        /**
         * @api {get} api/v1/Procedures/{id} Get procedure by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Procedures
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 procedure identifier
         * @apiSuccess {JSON} Result The procedure JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                              "DataType": {
                                "Id": 0,
                                "Name": ""
                              },
                              "Id": 0,
                              "Name": "",
                              "Description": "",
                              "DICOMAcquisitionDeviceCodeSchemeDesignator": "",
                              "DICOMAcquisitionDeviceCodeValue": "",
                              "DICOMAcquisitionDeviceCodeMeaning": "",
                              "DICOMAnatomicStructureCodeSchemeDesignator": "",
                              "DICOMAnatomicStructureCodeValue": "",
                              "DICOMAnatomicStructureCodeMeaning": "",
                              "DataTypeID": 0
                            }
               }
         *
         */
        [HttpGet("{id}")]
        public IActionResult GetSingle(long id)
        {
            var result = Gateway.GetSingle(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/Procedures Post procedure
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Procedures
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Procedure
         * @apiParamExample {json} Request-Example:
                            {
                              "DataType": {
                                "Id": 0,
                                "Name": ""
                              },
                              "Id": 0,
                              "Name": "",
                              "Description": "",
                              "DICOMAcquisitionDeviceCodeSchemeDesignator": "",
                              "DICOMAcquisitionDeviceCodeValue": "",
                              "DICOMAcquisitionDeviceCodeMeaning": "",
                              "DICOMAnatomicStructureCodeSchemeDesignator": "",
                              "DICOMAnatomicStructureCodeValue": "",
                              "DICOMAnatomicStructureCodeMeaning": "",
                              "DataTypeID": 0
                            }
         * @apiSuccess {JSON} Result The procedure JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                              "DataType": {
                                "Id": 0,
                                "Name": ""
                              },
                              "Id": 0,
                              "Name": "",
                              "Description": "",
                              "DICOMAcquisitionDeviceCodeSchemeDesignator": "",
                              "DICOMAcquisitionDeviceCodeValue": "",
                              "DICOMAcquisitionDeviceCodeMeaning": "",
                              "DICOMAnatomicStructureCodeSchemeDesignator": "",
                              "DICOMAnatomicStructureCodeValue": "",
                              "DICOMAnatomicStructureCodeMeaning": "",
                              "DataTypeID": 0
                            }
               }
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                    {
                      "isSuccess": false,
                      "message": "Invalid Model",
                      "exception": "",
                      "result": [
	                    {
                            "key": "Name",
                            "ErrorMessage": "The Name field is required."
	                    }
                      ],
                      "pager": null
                    }
         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]ProcedureFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/Procedures/{id} Put procedure
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Procedures
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name,Description,MediaTypeId" 
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 procedure identifier
         * @apiParam {JSON} Object modified of LibraryAnswerType
         * @apiParamExample {json} Request-Example:
                            {
                              "DataType": {
                                "Id": 0,
                                "Name": ""
                              },
                              "Id": 0,
                              "Name": "",
                              "Description": "",
                              "DICOMAcquisitionDeviceCodeSchemeDesignator": "",
                              "DICOMAcquisitionDeviceCodeValue": "",
                              "DICOMAcquisitionDeviceCodeMeaning": "",
                              "DICOMAnatomicStructureCodeSchemeDesignator": "",
                              "DICOMAnatomicStructureCodeValue": "",
                              "DICOMAnatomicStructureCodeMeaning": "",
                              "DataTypeID": 0
                            }
         * @apiSuccess {JSON} Result The procedure JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                              "DataType": {
                                "Id": 0,
                                "Name": ""
                              },
                              "Id": 0,
                              "Name": "",
                              "Description": "",
                              "DICOMAcquisitionDeviceCodeSchemeDesignator": "",
                              "DICOMAcquisitionDeviceCodeValue": "",
                              "DICOMAcquisitionDeviceCodeMeaning": "",
                              "DICOMAnatomicStructureCodeSchemeDesignator": "",
                              "DICOMAnatomicStructureCodeValue": "",
                              "DICOMAnatomicStructureCodeMeaning": "",
                              "DataTypeID": 0
                            }
               }
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                    {
                      "isSuccess": false,
                      "message": "Invalid Model",
                      "exception": "",
                      "result": [
	                    {
                            "key": "Name",
                            "ErrorMessage": "The Name field is required."
	                    }
                      ],
                      "pager": null
                    }
         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProcedureFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/Procedures/{id} Delete procedure
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Procedures
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 procedure identifier
         * @apiSuccess {JSON} Result The procedure JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": true,
                    "Message": "successful"
                    "Exception": "Exception Message",
                    "Result": true
                }
         *
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }
    }
}