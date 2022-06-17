using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryItems.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryItems
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {

        }
        public DbSet<BagModel> Bags {get;set;}
        public DbSet<PackageModel> Packages {get;set;}

    }
}