using Excelsior.API.Repositories;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Excelsior.API.Controllers.v0
{
    [Authorize]
    [Route("api/v0/[controller]")]
    public class RawDataController : Controller
    {
        public DataModel db { get; set; }
        public RawDataController(DataModel context)
        {
            db = context;
        }

        #region Raw Data

        /**
        * @api {get} api/v0/RawData/GetRawDataBySeriesID Get Raw Data By Series ID
        * @apiName GetRawDataBySeriesID   
        * @apiVersion 0.0.0
        * @apiGroup RawData
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                         seriesID=0 series Identifier
        * @apiSuccess {JSON} Result                                       The paginated array of RawData JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": [
                                   {
                                      "RawDataID": 0,
                                      "SeriesID": null,
                                      "DataTypeID": null,
                                      "ThumbImage": null,
                                      "ThumbImageLocation": null,
                                      "DCMInstanceUID": null,
                                      "DCMFileLocation": null,
                                      "Laterality": null,
                                      "IsActive": false,
                                      "LastError": null,
                                      "StatusID": null,
                                      "HasError": false
                                    }
                                   ]
                       }
        *
        */
        [HttpGet]
        [Route("getrawdatabyseriesid")]
        public ResultInfo<IList<RawDataResponseDto>> GetRawDataBySeriesID(long seriesID, int? page, int? pageSize, string filter, string search, string sort)
        {
            RawDataRequestDto dto = new RawDataRequestDto
            {
                SeriesId = seriesID,
                Filter = filter,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort,
            };

            var repository = new RawDataRepository(db);
            ResultInfo<IList<RawDataResponseDto>> result = repository.GetRawDataBySeriesID(dto);

            return result;
        }

        /**
        * @api {get} api/v0/RawData/GetRawDataByID Get Raw Data By ID
        * @apiName GetRawDataByID   
        * @apiVersion 0.0.0
        * @apiGroup RawData
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                         Id rawdata Identifier
        * @apiSuccess {JSON} Result                                       The RawData JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": 
                                   {
                                      "RawDataID": 0,
                                      "SeriesID": null,
                                      "DataTypeID": null,
                                      "ThumbImage": null,
                                      "ThumbImageLocation": null,
                                      "DCMInstanceUID": null,
                                      "DCMFileLocation": null,
                                      "Laterality": null,
                                      "IsActive": false,
                                      "LastError": null,
                                      "StatusID": null,
                                      "HasError": false
                                    }
                       }
        *
        */
        [HttpGet]
        [Route("getrawdatabyid")]
        public ResultInfo<RawDataResponseDto> GetRawDataByID(long Id)
        {
            CommonRequestDto dto = new CommonRequestDto()
            {
                CommonId = Id,
            };

            var repository = new RawDataRepository(db);
            ResultInfo<RawDataResponseDto> result = repository.GetRawDataByID(dto);

            return result;
        }

        /**
        * @api {post} api/v0/RawData/UpdateSatatusAndLoadRawData Post Update Satatus And Load RawData
        * @apiName UpdateSatatusAndLoadRawData   
        * @apiVersion 0.0.0
        * @apiGroup RawData
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {String}                         status="" RawData status
        * @apiParam (Request Parameters) {Number}                         RawDataID=0 RawData Identifier
        * @apiParam (Request Parameters) {Boolean}                        hasError=false RawData has Error
        * 
        * 
        * @apiSuccess {JSON} Result                                       The paginated array of RawData JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                      "IsSuccess": false,
                      "Message": "successful"
                      "Exception": "Error",
                      "Result": [
                                    {
                                      "RawDataID": 0,
                                      "SeriesID": null,
                                      "DataTypeID": null,
                                      "ThumbImage": null,
                                      "ThumbImageLocation": null,
                                      "DCMInstanceUID": null,
                                      "DCMFileLocation": null,
                                      "Laterality": null,
                                      "IsActive": false,
                                      "LastError": null,
                                      "StatusID": null,
                                      "HasError": false
                                    }
                        ]
                 }
        *
        */

        [HttpPost]
        [Route("updatesatatusandloadrawdata")]
        public List<Domain.PACS_RawDatum> UpdateSatatusAndLoadRawData([FromBody]JObject value)
        {
            var rawDataID = value["rawDataID"].ToObject<long>();
            var status = value["status"].ToObject<string>();
            var hasError = value["hasError"].ToObject<bool?>();
            var errorMsg = value["errorMsg"].ToObject<string>();
            return new RawDataRepository(db).UpdateSatatusAndLoadRawData(rawDataID, status, hasError);
        }

        #endregion
    }
}
