using Common.Interface;
using DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace DotNetCoreWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIStatusController : BaseController
    {
        private readonly ICustomLogger _logger;
        public APIStatusController(ICustomLogger logger)
        {
            _logger = logger;
        }

        [Route("/GetAPIStatus")]
        [HttpGet]
        public async Task<IActionResult> GetAPIStatus()
        {
            try
            {
                return Success("API is live and working.", "");
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAPIStatus= ", ex.Message);
                return Error(ex.Message);
            }
        }

    }
}
