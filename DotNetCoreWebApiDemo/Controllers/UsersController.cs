using Common.Interface;
using Common.Pagination;
using DataAccess.Context;
using Entities.DTOS.UsersDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interface;

namespace DotNetCoreWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUsersService _UsersService;
        private readonly ICustomLogger _logger;

        public UsersController(IUsersService usersService, ICustomLogger logger)
        {
            _UsersService = usersService;
            _logger = logger;
        }

        [Route("/GetUserByGUID")]
        [HttpGet]
        public async Task<IActionResult> GetUserByGUID(string guid)
        {
            try
            {
                var user = await _UsersService.GetUserByGuid(guid);
                return Success(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetSingleUser=", ex.Message, "");
                return Error(ex.Message);
            }
        }

        [Route("/GetUserByEmail")]
        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _UsersService.GetUserByEmail(email);
                return Success(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetSingleUser=", ex.Message, "");
                return Error(ex.Message);
            }
        }

        [Route("/GetAllUsers")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationFilter filter)
        {
            try
            {
                var users = await _UsersService.GeAllUsers(filter);
                return Success(users);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllUsers=", ex.Message, "");
                return Error(ex.Message);
            }
        }

        [Route("/CheckUserExists")]
        [HttpGet]
        public async Task<IActionResult> CheckUserExists(string guid)
        {
            try
            {
                var IFExist = await _UsersService.CheckUserExists(guid);
                return Success(IFExist);
            }
            catch (Exception ex)
            {
                _logger.LogError("CheckUserExists=", ex.Message, "");
                return Error(ex.Message);
                //return Json(_exceptionService.ControllerException(ex));
            }
        }

        [Route("/UpdateUser")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user_dto, string guid)
        {
            try
            {
                _logger.LogInfo("UpdateUser=", "Json: " + JsonConvert.SerializeObject(user_dto).ToString(), "Guid: " + guid);

                var Result = await _UsersService.UpdateUser(user_dto, guid);
                return Success(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateUser=", ex.Message, guid);
                return Error(ex.Message);
            }
        }

        [Route("/AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDTO user_dto)
        {
            try
            {
                var Result = await _UsersService.AddUser(user_dto);
                if (Result == null)
                {
                    return Success("User with GUID already exists.","");
                }
                return Success(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError("AddUser=", ex.Message);
                return Error(ex.Message);
            }
        }

        [Route("/ChangeEmailAddress")]
        [HttpPost]
        public async Task<IActionResult> ChangeEmailAddress(string guid, string email, string new_email)
        {
            try
            {
                var Result = await _UsersService.UpdateUserEmail(guid, email, new_email);
                return Success(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError("ChangeEmailAddress = ", ex.Message, guid);
                return Error(ex.Message);
            }
        }

        [Route("/UpdateAccountStatus")]
        [HttpPost]
        public async Task<IActionResult> UpdateAccountStatus(string guid, bool? status)
        {
            try
            {
                var Result = await _UsersService.UpdateAccountStatus(guid, status);
                return Success(Result);
            }
            catch (Exception ex)
            {
                _logger.LogError("UpdateAccountStatus = ", ex.Message, guid);
                return Error(ex.Message);
            }
        }

    }
}
