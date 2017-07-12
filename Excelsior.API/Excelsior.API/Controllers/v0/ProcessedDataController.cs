using Excelsior.API.Repositories;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Domain;
using Excelsior.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.API.Controllers.v0
{
    [Authorize]
    [Route("api/v0/[controller]")]
    public class ProcessedDataController : Controller
    {
        public DataModel Context { get; set; }
        public IUsersRepository UsersRepository { get; set; }

        public ProcessedDataController(DataModel context, IUsersRepository usersRepository)
        {
            Context = context;
            UsersRepository = usersRepository;
        }

        /**
        * @api {post} api/v0/ProcessedData Post Processed Data
        * @apiName Post   
        * @apiVersion 0.0.0
        * @apiGroup ProcessedData
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                       RawDataID=0 RawData Identifier
        * @apiParam (Request Parameters) {String}                       ProcessedDataLabel Label of Processed data
        * @apiParam (Request Parameters) {String}                       ProcessDataXMLString String XML of Processed data
        * 
        * @apiParamExample {json} Request-Example:
            {
              "RawDataID": 0,
              "ProcessedDataLabel": "Layers",
              "ProcessDataXMLString": "",
            } 
        * 
        * @apiSuccess {JSON} Result                                    boolean result  of operation included in json object 
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                      "IsSuccess": false,
                      "Message": "successful"
                      "Exception": "Error",
                      "Result": false
                 }
        *
        */
        [HttpPost]
        public ResultInfo<bool> Post([FromBody]ProcessedDataRequestDto value)
        {
            var response = new ResultInfo<bool>();
            try
            {
                string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
                var user = UsersRepository.GetSingle(x => x.AspUserID == new Guid(userId));
                value.UserID = user.UserID;

                var repository = new Repositories.ProcessedDataRepository(Context);
                bool result = repository.SetProcessedData(value);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Message = "Processed data not saved";
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Exception = ex.Message;
                response.IsSuccess = false;
                response.Message = "Exception";
            }

            return response;

        }

        /**
        * @api {get} api/v0/ProcessedData Get Processed Data
        * @apiName Get   
        * @apiVersion 0.0.0
        * @apiGroup ProcessedData
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                       RawDataID=0 RawData Identifier
        * 
        * @apiSuccess {JSON} Result                                    boolean result  of operation included in json object 
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                      "IsSuccess": false,
                      "Message": "successful"
                      "Exception": "Error",
                      "Result": {
                              "RawDataID": 0,
                              "ProcessedDataLabel": "Layers",
                              "ProcessDataXMLString": "",
                            } 
                 }
        *
        */
        [HttpGet]
        public ResultInfo<IList<ProcessedDataResponseDto>> Get(long RawDataID)
        {
            var response = new ResultInfo<IList<ProcessedDataResponseDto>>();
            try
            {
                var repository = new Repositories.ProcessedDataRepository(Context);
                var result = repository.GetProcessedData(RawDataID);
                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Error getting Processed data";
                }
                else
                {
                    response.Result = result;
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Exception = ex.Message;
                response.IsSuccess = false;
                response.Message = "Exception";
            }

            return response;

        }
    }
}
