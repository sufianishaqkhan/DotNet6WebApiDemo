using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        [NonAction]
        protected IActionResult Success<T>(T data, string message = "")
        {
            return Ok(new ResponseModel
            {
                status_code = this.HttpContext.Response.StatusCode = 200,
                data = data,
                error = message
            });
        }

        [NonAction]
        protected IActionResult Error<T>(T data, string message = "")
        {
            return BadRequest(new ResponseModel
            {
                status_code = this.HttpContext.Response.StatusCode = 401,
                data = data,
                error = message
            });
        }
    }
}
