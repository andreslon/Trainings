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
    public class FramesController : Controller
    {
        public DataModel db { get; set; }
        public FramesController(DataModel context)
        {
            db = context;
        }

        #region Frames

        /**
        * @api {get} api/v0/frames/getframes Get frames by Raw Data ID
        * @apiName GetFrames   
        * @apiVersion 0.0.0
        * @apiGroup Frames
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                         rawDataID=0 Raw Data Identifier
        * @apiSuccess {JSON} Result                                       The paginated array of Frame JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": [
                                   {
                                      "DicomFrameID": 0,
                                      "FrameImageLocation": null,
                                      "FrameIndex": 0,
                                      "ImageHeight": 0,
                                      "ImageWidth": 0,
                                      "IsActive": false,
                                      "IsKeyFrame": false,
                                      "RawDataID": null,
                                      "RefLineCoordinates": null,
                                      "RefLineType": null
                                    }
                                   ]
                       }
        *
        */

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
        [Route("getframes")]
        public ResultInfo<IList<FramesResponseDto>> GetFrames(long rawDataID, int? page, int? pageSize, string filter, string search, string sort)
        {
            FramesRequestDto dto = new FramesRequestDto
            {
                RawDataId = rawDataID,
                Filter = filter,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort,
            };

            var repository = new FramesRepository(db);
            ResultInfo<IList<FramesResponseDto>> result = repository.GetFrames(dto);

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
        public List<PACS_RawDatum> UpdateSatatusAndLoadRawData([FromBody]JObject value)
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
