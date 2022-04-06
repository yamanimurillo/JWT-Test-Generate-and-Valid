using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace webapi.test;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // If [AllowAnonymous] attribute then skip
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<CustomAllowAnonymousAttribute>().Any();
        if (allowAnonymous) return;

        // If user its found then authorize
        var user = context.HttpContext.Items["UserID"];
        if (user == null) {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}