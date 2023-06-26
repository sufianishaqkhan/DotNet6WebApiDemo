using Common.Pagination;
using DataAccess.Context;
using Entities.DTOS;
using Entities.DTOS.UsersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IUsersRepository
    {
        Task<UserDTO> GetUserByGuid(string guid);
        Task<UserDTO> GetUserByEmail(string email);
        Task<PagedResponse<List<UserDTO>>> GeAllUsers(PaginationFilter filter);
        Task<bool> CheckUserExists(string guid);
        Task<UserDTO> AddUser(UserDTO user);
        Task<string> UpdateUser(UserDTO user, string guid);
        Task<string> UpdateUserEmail(string guid, string email, string new_email);
        Task<string> UpdateAccountStatus(string guid, bool? Status);
    }
}
