using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NutritionalRecipeBook.Api.Filters;

public sealed class RequireUserIdAttribute : Attribute, IAsyncActionFilter
{
    public const string UserIdItemKey = "UserId";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        var claim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("sub");
        if (claim == null || !Guid.TryParse(claim.Value, out var userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        context.HttpContext.Items[UserIdItemKey] = userId;
        await next();
    }
}
