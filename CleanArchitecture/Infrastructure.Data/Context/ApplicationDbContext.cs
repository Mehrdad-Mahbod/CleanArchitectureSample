using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }        
        public DbSet<Menu> Menus { get; set; }
        public DbSet<UserMenu> UserMenus { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<GeneralOffice> GeneralOffices { get; set; }
        

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            //Builder.SetQueryFilterOnAllEntities<BaseEntity>(A => A.IsDeleted == false);

            base.OnModelCreating(Builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //Install-Package Microsoft.EntityFrameworkCore.Tools -Version 2.1.4
            //Install-Package Microsoft.EntityFrameworkCore.Tools -Version 2.1.8
            DatabaseSeed.Seed(Builder);
        }


    }
}
