using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace webapi.test.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{

    private readonly ILogger<TestController> _logger;
    private readonly IJwtUtils _jwtUtils;

    public TestController(ILogger<TestController> logger, IJwtUtils jwtUtils)
    {
        _logger = logger;
        _jwtUtils = jwtUtils;
    }

    [HttpGet]
    [Route("GenerateToken")]
    [CustomAllowAnonymous]
    public String GenerateToken()
    {
        return _jwtUtils.GenerateToken();
    }

    [HttpPost]
    [Route("GetUserID")]
    [CustomAuthorize]
    public String? GetUserID()
    {
        String? userID = Convert.ToString(Request.HttpContext.Items["UserID"]);
        return !String.IsNullOrEmpty(userID) ? userID : null;
    }

}
