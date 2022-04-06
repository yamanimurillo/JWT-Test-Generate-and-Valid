
namespace webapi.test;

public class CustomAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Token"].FirstOrDefault();

        if (!String.IsNullOrEmpty(token))
        {
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                context.Items["UserID"] = userId.ToString();
            }
        }

        await _next(context);
    }
}