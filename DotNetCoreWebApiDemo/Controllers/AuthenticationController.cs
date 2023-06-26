using DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthorizeService _authorizeService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthorizeService authorizeService, IConfiguration configuration)
        {
            _authorizeService = authorizeService;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("/GetAuthenticationToken")]
        public async Task<IActionResult> GetAuthenticationToken(string username, string password)
        {
            string IP_Address = HttpContext.Connection.RemoteIpAddress.ToString();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                bool bAuthenticate = _authorizeService.AuthenticateUser(username, password);
                if (bAuthenticate)
                {
                    var token = _authorizeService.CreateToken(username);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Provided username and/or password is incorrect.");
                }
            }
            else
            {
                return BadRequest("Provided username and/or password is incorrect.");
            }
        }
    }
}
