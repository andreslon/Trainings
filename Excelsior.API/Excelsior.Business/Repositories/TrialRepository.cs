using System;
using Excelsior.Business.DtoEntities;
using System.Linq;
using Excelsior.Domain;
using System.Collections.Generic;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Business.Helpers;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Domain.Helpers;

namespace Excelsior.Business.Repositories
{
    public class TrialRepository
    {
        public DataModel db { get; set; }
        public TrialRepository(DataModel context)
        {
            db = context;
        }
        public ResultInfo<IList<TrialsResponseDto>> GetTrialsByUserId(TrialsRequestDto trial)
        {
            var result = new ResultInfo<IList<TrialsResponseDto>>();
            try
            {
                List<TrialsResponseDto> trialsResult = new List<TrialsResponseDto>();
                Logic.TrialHandler handler = new Logic.TrialHandler(db);
                var trials = handler.GetTrialsByUserId(trial);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return trials.Count();
                });
                result.SetPager(count, trial.Page, trial.PageSize);
                var trialsPaged = GeneralHelper.GetPagedList(trials.OrderBy(x=> x.TrialName), result.Pager);
                if (trialsPaged != null)
                {
                    foreach (var item in trialsPaged)
                    {
                        item.TotalSubjects = handler.GetTotalSubjects(item.TrialID, item.IsTestingPhase);
                        var dto = new TrialsResponseDto()
                        {
                            TrialID = item.TrialID,
                            TrialAlias = item.TrialAlias,
                            TrialEndDate = item.TrialEndDate,
                            TrialName = item.TrialName,
                            TrialStartDate = item.TrialStartDate,
                            AnimalSpeciesDisplayName = item.CFGAnimalSpecy?.AnimalSpeciesDisplayName,
                            TotalSubjects = item.TotalSubjects,
                            PrimaryDrugs = item.PrimaryDrugs,
                            IsActive = item.IsActive,
                            IsLocked = item.IsLocked,
                            TrialLockedDate = item.TrialLockedDate
                        };
                        trialsResult.Add(dto);
                    }
                }

                result.Result = trialsResult;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

    }
}