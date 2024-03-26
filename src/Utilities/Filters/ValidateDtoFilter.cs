using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookARoom.Utilities;



/// <summary>
/// A filter to validate the Model States of Dto objects sent from a client
/// </summary>
public class ValidateDtoFilter : IActionFilter
{
    /// <summary>
    /// A delegate anonymous method to select the parameter
    /// </summary>
    private readonly Func<KeyValuePair<string, object?>, bool> selectParameter = arg =>
        arg.Value != null && arg.Value.ToString().Contains("Dto");

    public ValidateDtoFilter() { }

    /// <summary>
    /// Executes before the controller action is executed 
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var actionParameter = context.ActionArguments
            .SingleOrDefault(selectParameter)
            .Value;

        if (actionParameter is null)
        {
            context.Result = new BadRequestObjectResult($"Object sent from client is null");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }

    }
    public void OnActionExecuted(ActionExecutedContext context)
    { }
}
