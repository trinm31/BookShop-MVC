using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BookShop_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BookShop_MVC.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public string ConnectString  =  @"Data Source=localhost,1433;Initial Catalog=BookShopMVC;User ID=SA;Password=Password789";
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectString);
            }
        }

        public DbSet<Category> Categories { get; set; }
    }
}