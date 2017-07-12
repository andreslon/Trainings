using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Business.Repositories;
using Excelsior.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.API.Controllers.v0
{
    [Authorize]
    [Route("api/v0/[controller]")]
    public class SeriesController : Controller
    {
        public DataModel db { get; set; }
        public SeriesController(DataModel context)
        {
            db = context;
        }

        #region Pool 

        /**
        * @api {get} api/v0/series/getpool Get Series in Pool
        * @apiName GetSeriesPool   
        * @apiVersion 0.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                                       trialId=0 Trial identifier
        * @apiParam (Request Parameters) {String=["upload","check-in","grade","verify","completed"]}    [step] Workflow step name
        * @apiParam (Request Parameters) {Boolean}                                                      [assigned] true to get assigned, false for unassigned, null for all
        * @apiParam (Request Parameters) {Number}                                                       page_size=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                                       page=1 Current page.
        * @apiParam (Request Parameters) {String}                                                       sort="recent" Sort by.
        * @apiParam (Request Parameters) {String}                                                       [search] Search text.
        * @apiParam (Request Parameters) {String}                                                       [subjectId] Subject Id.
        * @apiParam (Request Parameters) {String}                                                       [timePointId] Time Point Id.
        * @apiParam (Request Parameters) {String}                                                       [ProcedureId] Procedure Id.
        * @apiSuccess {JSON} Result The paginated array of Sequences JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": [
                                   {
                                         "CategoryDes": null,
                                         "SeriesGroupID": null,
                                         "LastStepCompletionDate": null,
                                         "SiteName": null,
                                         "RandomizedSubjectID": null,
                                         "AlternativeRandomizedSubjectID": null,
                                         "IsEligibilityIDUsed": false,
                                         "NameCode": null,
                                         "Laterality": null,
                                         "StudyDate": null,
                                         "TimePointsDescription": null,
                                         "ImgProcedureName": null,
                                         "ContactUserName": null,
                                         "ContactEquipmentName": null,
                                         "AssignedToName": null,
                                         "AssignedToID": null,
                                         "SeriesID": null,
                                         "IsTechnicianCerified": false,
                                         "IsTestingSubject": null,
                                         "IsEquipmentCerified": false
                                       }
                                   ]
                       }
        *
        */
        [HttpGet]
        [Route("getpool")]
        public ResultInfo<IList<WFSequencesResponseDto>> GetPool(long trialId, string step, long? subjectId, long? timePointListId, long? procedureId, bool? assigned, int? page, int? pageSize, string search, string sort, string filter)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            SeriesRequestDto dto = new SeriesRequestDto
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort,
                Filter = filter,
                StudyId = trialId,
                Step = step,
                Assigned = assigned,
                SubjectId = subjectId,
                TimePointListId = timePointListId,
                ProcedureId = procedureId,
            };

            var repository = new SeriesRepository(db);
            var result = repository.GetPool(dto);
            return result;
        }

        /**
        * @api {get} api/v0/series/getseriebyid Get a single serie by id
        * @apiName GetSeriesPool   
        * @apiVersion 0.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                                       Id serie identifier
        * @apiSuccess {JSON} Result The Sequence JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": 
                                   {
                                         "CategoryDes": null,
                                         "SeriesGroupID": null,
                                         "LastStepCompletionDate": null,
                                         "SiteName": null,
                                         "RandomizedSubjectID": null,
                                         "AlternativeRandomizedSubjectID": null,
                                         "IsEligibilityIDUsed": false,
                                         "NameCode": null,
                                         "Laterality": null,
                                         "StudyDate": null,
                                         "TimePointsDescription": null,
                                         "ImgProcedureName": null,
                                         "ContactUserName": null,
                                         "ContactEquipmentName": null,
                                         "AssignedToName": null,
                                         "AssignedToID": null,
                                         "SeriesID": null,
                                         "IsTechnicianCerified": false,
                                         "IsTestingSubject": null,
                                         "IsEquipmentCerified": false
                                       }
                       }
        *
        */
        [HttpGet]
        [Route("getseriebyid")]
        public ResultInfo<WFSequencesResponseDto> GetSerieByID(long Id)
        {
            CommonRequestDto dto = new CommonRequestDto()
            {
                CommonId = Id,
            };

            var repository = new SeriesRepository(db);
            ResultInfo<WFSequencesResponseDto> result = repository.GetSerieByID(dto);

            return result;
        }

        /**
        * @api {post} api/v0/series/assignseries Post Assign Series
        * @apiName AssignSeries   
        * @apiVersion 0.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                         CommonID=0 Common Identifier
        * @apiParam (Request Parameters) {Number[]}                       CommonList Common list
        * 
        * @apiParamExample {json} Request-Example:
            {
              "CommonID": 0,
              "CommonList": [2,2,3]
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
        [Route("assignseries")]
        public ResultInfo<bool> AssignSeries([FromBody]CommonRequestDto commonDto)
        {
            var result = new ResultInfo<bool>();

            try
            {
                string UserId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
                var seriesRepository = new SeriesRepository(db);
                var rta = seriesRepository.AssignSerie(commonDto, UserId);

                result.Result = rta;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
                result.Result = false;
            }

            return result;
        }

        /**
        * @api {post} api/v0/series/unassignseries Post Unassign Series
        * @apiName UnassignSeries  
        * @apiVersion 0.0.0
        * @apiGroup Series
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                         CommonID=0 Common Identifier
        * @apiParam (Request Parameters) {Number[]}                       CommonList Common list
        * 
        * @apiParamExample {json} Request-Example:
            {
              "CommonID": 0,
              "CommonList": [2,2,3]
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
        [Route("unassignseries")]
        public ResultInfo<bool> UnassignSeries([FromBody]CommonRequestDto commonDto)
        {
            var result = new ResultInfo<bool>();

            try
            {
                string UserId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
                var seriesRepository = new SeriesRepository(db);
                var rta = seriesRepository.UnassignSerie(commonDto, UserId);

                result.Result = rta;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
                result.Result = false;
            }

            return result;
        }

        [HttpGet]
        [Route("getcategoryflags")]
        public ResultInfo<IList<WFCategoryFlagResponseDto>> GetWFCategoryFlags()
        {
            var repository = new SeriesRepository(db);
            ResultInfo<IList<WFCategoryFlagResponseDto>> result = repository.GetWFCategoryFlags();
            return result;
        }

        [HttpPost]
        [Route("setcategory")]
        public ResultInfo<IList<WFSequencesResponseDto>> SetSeriesCategory([FromBody]CommonRequestDto commonDto)
        {
            var result = new ResultInfo<IList<WFSequencesResponseDto>>();

            try
            {
                string UserId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
                var seriesRepository = new SeriesRepository(db);
                result = seriesRepository.SetSeriesCategory(commonDto, UserId);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex.Message;
                result.Result = null;
            }

            return result;
        }

        #endregion
    }
}
