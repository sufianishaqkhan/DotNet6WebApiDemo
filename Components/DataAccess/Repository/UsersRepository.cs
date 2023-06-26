using Common.Interface;
using Common.Pagination;
using Common.Repository;
using DataAccess.Context;
using DataAccess.Interface;
using Entities.DTOS;
using Entities.DTOS.UsersDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UsersRepository  : GenericRepository<User>,  IUsersRepository
    {
        private ICustomLogger _logger;
        public UsersRepository(DemoDbContext context, ICustomLogger logger) : base(context)
        {
            _logger = logger;
        }
        
        public async Task<UserDTO> AddUser(UserDTO userDto)
        {
            var response = new UserDTO();
            try
            {
                if (string.IsNullOrEmpty(userDto.accountGuid) || string.IsNullOrEmpty(userDto.EmailAddress))
                {
                    return response;
                }
                _logger.LogInfo("AddUser", "User Registration Started in Users");

                Guid guid = new Guid(userDto.accountGuid);
                var chkuse = await _context.Users.Where(x => x.AccountGuid == guid || x.EmailAddress == userDto.EmailAddress).FirstOrDefaultAsync();

                if (chkuse != null)
                {
                    return response;
                }

                #region Mapping Values to DB Model
                User user = new User();
                user.EmailAddress = userDto.EmailAddress;
                user.UserName = userDto.UserName;
                user.Prefix = userDto.Prefix;
                user.FirstName = userDto.FirstName;
                user.MiddleName = userDto.MiddleName;
                user.LastName = userDto.LastName;
                user.OrganizationName = userDto.OrganizationName;
                user.City = userDto.City;
                user.State = userDto.State;
                user.ZipCode = userDto.ZipCode;
                user.Country = userDto.Country;
                user.ContactMobile = userDto.ContactMobile;
                user.ContactPhone = userDto.ContactPhone;
                user.ContactFax = userDto.ContactFax;
                user.IsWorkAddress = userDto.IsWorkAddress;
                user.IsDeleted = false;
                user.DateAdded = DateTime.UtcNow;
                user.DateUpdated = DateTime.UtcNow;
                user.Status = userDto.Status;
                user.AddressLine1 = userDto.AddressLine1;
                user.AddressLine2 = userDto.AddressLine2;
                user.WebUrl = userDto.WebUrl;
                user.Description = userDto.Description;
                user.DateAccessed = DateTime.UtcNow;

                _logger.LogInfo("AddUser", "User Registration Model Binded");

                #endregion

                var added = _context?.Users?.Add(user);
               
                if (_context != null)
                {
                    await _context.SaveChangesAsync();
                }
                var map = _context.Users.Where(x => x.UserId == added.Entity.UserId).FirstOrDefault();
                response = MapGetDataDto(map);

                _logger.LogInfo("AddUser", "User Registration completed successfully");
            }
            catch (Exception ex)
            {
                response = null;
                _logger.LogError("AddUser", ex.Message.ToString());
            }
            return response;
        }

        private UserDTO MapGetDataDto(User user)
        {
            var obj = new UserDTO();
            obj.AccountGuid = user.AccountGuid;
            obj.UserId = user.UserId;   
            obj.StatusId = user.StatusId;
            obj.EmailAddress = user.EmailAddress;
            obj.UserName = user.UserName;
            obj.Prefix = user.Prefix;
            obj.FirstName = user.FirstName;
            obj.MiddleName = user.MiddleName;
            obj.LastName = user.LastName;
            obj.SecurityStamp = user.SecurityStamp;
            obj.Discriminator = user.Discriminator;
            obj.OrganizationName = user.OrganizationName;
            obj.City = user.City;
            obj.State = user.State;
            obj.ZipCode = user.ZipCode;
            obj.Country = user.Country;
            obj.ContactMobile = user.ContactMobile;
            obj.ContactPhone = user.ContactPhone;
            obj.ContactFax = user.ContactFax;
            obj.IsWorkAddress = user.IsWorkAddress ?? false;
            obj.AccessFailedCount = user.AccessFailedCount;
            obj.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            obj.IsEmailConfirmed = user.IsEmailConfirmed;
            obj.IsDeleted = user.IsDeleted;
            obj.DateAdded = user.DateAdded;
            obj.DateUpdated = user.DateUpdated;
            obj.Status = user.Status;
            obj.AddressLine1 = user.AddressLine1;
            obj.AddressLine2 = user.AddressLine2;
            obj.WebUrl = user.WebUrl;
            obj.Description = user.Description;
            obj.RoleId = user.RoleId;
            obj.DateAccessed = user.DateAccessed;

            return obj;
        }

        public async Task<bool> CheckUserExists(string guid)
        {
            Guid cGuid = new Guid(guid);
            if (!string.IsNullOrEmpty(guid))
            {
                var isexist = await _context.Users.Where(x => x.AccountGuid == cGuid).FirstOrDefaultAsync();
                if (isexist != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<PagedResponse<List<UserDTO>>> GeAllUsers(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Users
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Users.CountAsync();

            if (totalRecords > 0)
            {
                List<UserDTO> getdto = new List<UserDTO>();
                foreach (var item in pagedData)
                {
                    var obj = MapGetDataDto(item);
                    getdto.Add(obj);
                }

                return (new PagedResponse<List<UserDTO>>(getdto, validFilter.PageNumber, validFilter.PageSize, totalRecords));
            }
            else
            {
                return null;
            }
        }

        public async Task<UserDTO> GetUserByGuid(string guid)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                Guid cGuid = new Guid(guid);
                var Data = await _context.Users.Where(x => x.AccountGuid == cGuid).FirstOrDefaultAsync();
                if (Data != null)
                {
                    var retData = MapGetDataDto(Data);
                    return retData;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {           
            if (!string.IsNullOrEmpty(email))
            {
                email = WebUtility.UrlDecode(email);
                var mapget = await _context.Users.Where(x => x.EmailAddress == email).FirstOrDefaultAsync();
                if (mapget != null)
                {
                    var Data = MapGetDataDto(mapget);
                
                    return Data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<string> UpdateUser(UserDTO userDto, string guid)
        {
            _logger.LogInfo("UpdateUser", "update user operation has been started");
            string ret = string.Empty;

            if (string.IsNullOrWhiteSpace(guid))
            {
                guid = userDto.accountGuid;
            }

            if (!string.IsNullOrEmpty(guid))
            {
                Guid gid = new Guid(guid);
                var user = await _context.Users.Where(x => x.AccountGuid == gid).FirstOrDefaultAsync();
                if (user != null)
                {
                    _logger.LogInfo("UpdateUser", "User Found in USER based on ACCOUNT_GUID=" + gid);
                }
                else
                {
                    _logger.LogInfo("UpdateUser", "User Not Found in ADM_USER based on ACCOUNT_GUID=" + gid);

                    userDto.EmailAddress = WebUtility.UrlDecode(userDto.EmailAddress);
                    user = await _context.Users.Where(x => x.EmailAddress == userDto.EmailAddress).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        _logger.LogInfo("UpdateUser", "User Found in USER based on email address = " + userDto.EmailAddress);
                    }
                    else
                    {
                        _logger.LogInfo("UpdateUser", "User Not Found in USER based on email address = " + userDto.EmailAddress);
                    }
                }

                if (user != null)
                {
                    user.AccountGuid = gid;
                    user.Prefix = userDto.Prefix;
                    user.FirstName = userDto.FirstName;
                    user.MiddleName = userDto.MiddleName;
                    user.LastName = userDto.LastName;
                    user.OrganizationName = userDto.OrganizationName;
                    user.AddressLine1 = userDto.AddressLine1;
                    user.AddressLine2 = userDto.AddressLine2;
                    user.City = userDto.City;
                    user.State = userDto.State;
                    user.ZipCode = userDto.ZipCode;
                    user.Country = userDto.Country;
                    user.ContactMobile = userDto.ContactMobile;
                    user.ContactPhone = userDto.ContactPhone;
                    user.ContactFax = userDto.ContactFax;
                    user.IsWorkAddress = userDto.IsWorkAddress;
                    user.DateUpdated = DateTime.UtcNow;

                    _logger.LogInfo("UpdateUser", "update user map the entity parameters sucessful");
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    _logger.LogInfo("UpdateUser", "update user completed successful");

                    ret = "User Updated Successfully";
                }
                else
                {
                    ret = "User Not Found";
                    _logger.LogInfo("UpdateUser", "User Not Found, ACCOUNT_GUID = " + gid + ", email address = " + userDto.EmailAddress);
                }
            }
            else
            {
                ret = "Guid is required";
                _logger.LogInfo("UpdateUser", "Guid is required");
            }

            return ret;
        }

        public async Task<string> UpdateUserEmail(string Guid, string email, string newEmail)
        {
            string ret = string.Empty;
            Guid gid = new Guid(Guid);
            _logger.LogInfo("UpdateUserEmail", "User Update email precess started");

            email = WebUtility.UrlDecode(email);
            newEmail = WebUtility.UrlDecode(newEmail);

            _logger.LogInfo("UpdateUserEmail", "User Old Email: "+email);
            _logger.LogInfo("UpdateUserEmail", "User New Email: " + newEmail);

            var user = await _context.Users.Where(x => x.AccountGuid == gid && x.EmailAddress == email).FirstOrDefaultAsync();
            if (user != null)
            {
                user.EmailAddress = newEmail;

                _context.Users.Add(user);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                 _logger.LogInfo("UpdateUserEmail", "User Email updated successfully in AdmUsers EMAIL_ADDRESS=" +  email + ":: to New EMAIL_ADDRESS = "+newEmail);

                ret = "Email Updated Successfully";
               
            }
            else
            {
                ret = "Email Not Found";
                _logger.LogInfo("UpdateUserEmail", "User Email not found in AdmUsers EMAIL_ADDRESS=" + email);
            }
            return ret;
        }

        public async Task<string> UpdateAccountStatus(string Guid, bool? Status)
        {
            string ret = string.Empty;
            Guid gid = new Guid(Guid);
            var user = await _context.Users.Where(x => x.AccountGuid == gid).FirstOrDefaultAsync();
            if (user != null && Status.HasValue)
            {
                //Column is IS_DELETED in db, reversing Status parameter
                bool isDeleted = (bool)Status ? false : true;

                _logger.LogInfo("UpdateAccountStatus", "update user account status operation has been started");

                user.IsDeleted = isDeleted;
                user.DateUpdated = DateTime.UtcNow;

                _context.Users.Add(user);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _logger.LogInfo("UpdateAccountStatus", "update user account status completed successfully.");

                ret = "User Account Status Updated Successfully";
            }
            else
            {
                if (!Status.HasValue)
                {
                    ret = "Status cannot be empty";
                    _logger.LogInfo("UpdateAccountStatus", "User was not update because status was empty, ACCOUNT_GUID = " + gid);
                }
                else
                {
                    ret = "User Not Found";
                    _logger.LogInfo("UpdateAccountStatus", "User Not Found, ACCOUNT_GUID = " + gid);
                }
            }

            return ret;
        }
    }
}
