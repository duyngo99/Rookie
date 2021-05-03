using System;
using System.Collections.Generic;
using System.Text;
using BackEnd.Models;
using BackEnd.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
