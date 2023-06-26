using Common.Pagination;
using DataAccess.Context;
using DataAccess.Interface;
using DataAccess.Repository;
using Entities.DTOS;
using Entities.DTOS.UsersDto;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class UsersService : IUsersService
    {
        private readonly IStoreUnitOfWork _storeUnitofwork;

        public UsersService(IStoreUnitOfWork storeUnitofwork)
        {
            _storeUnitofwork = storeUnitofwork;
        }

        public Task<UserDTO> AddUser(UserDTO user)
        {
            var result = _storeUnitofwork.Users.AddUser(user);
            return result;
        }

        public async Task<bool> CheckUserExists(string guid)
        {
            bool IsExist = await _storeUnitofwork.Users.CheckUserExists(guid);
            return IsExist;
        }

        public async Task<PagedResponse<List<UserDTO>>> GeAllUsers(PaginationFilter filter)
        {
            var users = await _storeUnitofwork.Users.GeAllUsers(filter);
            return users;
        }

        public async Task<UserDTO> GetUserByGuid(string guid)
        {
            var user = await _storeUnitofwork.Users.GetUserByGuid(guid);
            return user;
        }
        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var user = await _storeUnitofwork.Users.GetUserByEmail(email);
            return user;
        }

        public async Task<string> UpdateUser(UserDTO userDto, string guid)
        {
            var user = await _storeUnitofwork.Users.UpdateUser(userDto, guid);
            return user;
        }

        public async Task<string> UpdateUserEmail(string guid, string email, string newEmail)
        {
            var user = await _storeUnitofwork.Users.UpdateUserEmail(guid, email, newEmail);
            return user;
        }

        public async Task<string> UpdateAccountStatus(string guid, bool? Status)
        {
            var user = await _storeUnitofwork.Users.UpdateAccountStatus(guid, Status);
            return user;
        }
    }
}
