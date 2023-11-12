using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pocket.API.DTO;
using System.Security.Claims;

namespace Pocket.API.Handlers.Attributes
{
    public class ValidateTokenAndEmailAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userCalim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userCalim == null)
            {
                context.Result = new BadRequestObjectResult(Constants.Errors.EmailNotPresentInToken);
                return;
            }

            string userEmail = userCalim.Value;

            //getting email from the paramters
            if (!context.ActionArguments.TryGetValue("email", out var userEmailFromArgs))
            {
                context.Result = new BadRequestObjectResult(Constants.Errors.EmailNotPresentInArgument);
                return;
            }

            if (userEmail != (string)userEmailFromArgs)
            {
                context.Result = new BadRequestObjectResult(Constants.Errors.EmailMismatch);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
