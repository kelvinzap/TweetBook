using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TweetBook.Contracts.V1.Response;

namespace TweetBook.Filter;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //before controller
        if (!context.ModelState.IsValid)
        {
            var errorsInModelState = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            var errorResponse = new ErrorResponse();

            foreach (var errors in errorsInModelState)
            {
                foreach (var subError in errors.Value)
                {
                    var errorModel = new ErrorModel
                    {
                        FieldName = errors.Key,
                        Message = subError
                    };
                    errorResponse.Errors.Add(errorModel);
                }   
            }

            context.Result = new BadRequestObjectResult(errorResponse);
            return;
        }

        await next();
        
        //after controller
    }
}