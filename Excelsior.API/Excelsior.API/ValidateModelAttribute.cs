using Excelsior.Business.DtoEntities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.API
{
    public class ResultObject
    {
        public string key { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (context.ModelState.IsValid == false)
            {

                List<ResultObject> errors = new List<ResultObject>();
                foreach (var model in context.ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors))
                {
                    if (model.Value != null)
                    {
                        ResultObject resultObject = new ResultObject();
                        foreach (var error in model.Value)
                        {
                            resultObject.key = model.Key;
                            if (!string.IsNullOrWhiteSpace(error.ErrorMessage))
                            {
                                resultObject.ErrorMessage = error.ErrorMessage;
                            }
                            if (!string.IsNullOrWhiteSpace(error.Exception?.Message))
                            {
                                resultObject.ErrorMessage = error.Exception?.Message;
                            }
                        }
                        if (!string.IsNullOrEmpty(resultObject.key))
                        {
                            errors.Add(resultObject);
                        } 
                    }
                }
                ResultInfo<IList<ResultObject>> result = new ResultInfo<IList<ResultObject>>();
                result.IsSuccess = false;
                result.Message = "Invalid Model";
                result.Result = errors;
                context.Result = new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(result);
            }
        }

    }
}