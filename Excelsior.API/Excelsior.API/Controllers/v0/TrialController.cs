using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Business.Repositories;
using Excelsior.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.API.Controllers.v0
{
    [Authorize]
    [Route("api/v0/[controller]")]
    public class TrialController : Controller
    {
        public DataModel db { get; set; }
        public TrialController(DataModel context)
        {
            db = context;
        }


        /**
        * @api {get} api/v0/Trial/GetTrialsByUserId Get Trials By User Identifier
        * @apiName GetTrialsByUserId
        * @apiVersion 0.0.0
        * @apiGroup Trial
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Boolean=true,false}                         isactive="false" Get only active trials?
        * @apiParam (Request Parameters) {Boolean=true,false}                         islocked="false" Get only locked trials?
        * @apiParam (Request Parameters) {Number}                                     page_size=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String="fieldName"}                         filter="all" Filter by.
        * @apiParam (Request Parameters) {String="fieldName"}                         sort="recent" Sort by.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of trial JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": false,
                 "Message": "successful"
                 "Exception": "Error",
                 "Result": [
                               {
                                  "TrialID": 0,
                                  "TrialAlias": null,
                                  "TrialEndDate": null,
                                  "TrialName": null,
                                  "TrialStartDate": null,
                                  "AnimalSpeciesDisplayName": null,
                                  "TotalSubjects": 0,
                                  "PrimaryDrugs": null,
                                  "IsActive": false,
                                  "IsLocked": false,
                                  "TrialLockedDate": null
                                }
                           ]
               }
        *
        */
        [HttpGet]
        [Route("gettrialsbyuserid")]
        public ResultInfo<IList<TrialsResponseDto>> GetTrialsByUserId(bool? isActive, bool? isLocked, int? page, int? pageSize, string filter, string search, string sort)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;

            TrialsRequestDto trialDto = new TrialsRequestDto
            {
                UserId = userId,
                Filter = filter,
                IsActive = isActive,
                IsLocked = isLocked,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort,
            };

            var trialRepository = new TrialRepository(db);
            ResultInfo<IList<TrialsResponseDto>> result = trialRepository.GetTrialsByUserId(trialDto);

            return result;
        }
    }
}
