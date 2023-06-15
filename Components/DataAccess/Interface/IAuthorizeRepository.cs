using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IAuthorizeRepository
    {
        bool AuthenticateUser(string pUserName, string pPassword);
        Task<AuthenticationModel> CreateToken(string userName);
    }
}
