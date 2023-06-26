using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public partial class DemoDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DemoDbContext()
        {
        }

        public DemoDbContext(DbContextOptions<DemoDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connStr = _configuration.GetConnectionString("DemoDbConnStr");
                if (!string.IsNullOrEmpty(connStr))
                    optionsBuilder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__USER__F3BEEBFFF852D25D");

                entity.ToTable("USER");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.AccessFailedCount).HasColumnName("ACCESS_FAILED_COUNT");

                entity.Property(e => e.AccountGuid).HasColumnName("ACCOUNT_GUID");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT_TYPE");


                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS_LINE_1");

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS_LINE_2");

                entity.Property(e => e.City)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.ContactFax)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_FAX");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_PHONE");

                entity.Property(e => e.ContactMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_MOBILE");

                entity.Property(e => e.Country)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.DateAccessed)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_ACCESSED");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_ADDED");

                entity.Property(e => e.DateUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_UPDATED");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Discriminator)
                    .HasMaxLength(250)
                    .HasColumnName("DISCRIMINATOR");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_ADDRESS");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");

                entity.Property(e => e.IsWorkAddress).HasColumnName("IS_WORK_ADDRESS");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.LockOutEnabled).HasColumnName("LOCK_OUT_ENABLED");

                entity.Property(e => e.LockOutEndDateUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("LOCK_OUT_END_DATE_UTC");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MIDDLE_NAME");

                entity.Property(e => e.OrganizationName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ORGANIZATION_NAME");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(250)
                    .HasColumnName("PASSWORD_HASH");

                entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("PHONE_NUMBER_CONFIRMED");

                entity.Property(e => e.Prefix)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PREFIX");

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.SecurityStamp)
                    .HasMaxLength(250)
                    .HasColumnName("SECURITY_STAMP")
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.State)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

                entity.Property(e => e.TwoFactorEnabled).HasColumnName("TWO_FACTOR_ENABLED");

                entity.Property(e => e.UserName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.WebUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("WEB_URL");

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("ZIP_CODE");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
