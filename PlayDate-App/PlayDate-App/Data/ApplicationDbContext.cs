using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayDate_App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDate_App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
<<<<<<< HEAD
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Name = "Parent",
                NormalizedName = "PARENT"
            }
            );
        }
=======

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Kid> Kids { get; set; }
>>>>>>> 75d92ec7156e0d28d03d97b997bc240edf644a36
    }
}
