using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BookShop_MVC.Models;
using BookShop_MVC.Utility;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace BookShop_MVC.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly ConnectionSetting _connection;
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IOptions<ConnectionSetting> connectOptions)
            : base(options)
        {
            _connection = connectOptions.Value;
        }
        public string ConnectString  =  @"Data Source=localhost,1433;Initial Catalog=BookShopMVC;User ID=SA;Password=Password789";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connection.DefaultConnection);
            optionsBuilder.UseSqlServer(ConnectString);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}