using System;
using System.Collections.Generic;

namespace DataAccess.Context
{
    public partial class User
    {
        public User()
        {

        }

        public long UserId { get; set; }
        public Guid? AccountGuid { get; set; }
        public long? StatusId { get; set; }
        public long? RoleId { get; set; }
        public string? EmailAddress { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string? UserName { get; set; }
        public string? Prefix { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? AccountType { get; set; }
        public string? WebUrl { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? OrganizationName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public bool? IsWorkAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? ContactMobile { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactFax { get; set; }
        public bool IsDeleted { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; } = null!;
        public string? Discriminator { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockOutEnabled { get; set; }
        public DateTime? LockOutEndDateUtc { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime? DateAccessed { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
