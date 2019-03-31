using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeTravelMVC.Models;

namespace GrandeTravelMVC.Services
{
    public class MyDbContext: IdentityDbContext
    {
        public DbSet<CustomerProfile> CustomerProfileTbl { get; set; }
        public DbSet<ProviderProfile> ProviderProfileTbl { get; set; }
        public DbSet<Location> LocationTbl { get; set; }
        public DbSet<Package> PackageTbl { get; set; }
        public DbSet<Order> OrderTbl { get; set; }
        public DbSet<Feedback> FeedbackTbl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB ; Database=TravelDB; Trusted_Connection=True");
        }
    }
}
