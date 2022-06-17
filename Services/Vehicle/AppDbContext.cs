using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vehicle.Models;

namespace Vehicle
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {

        }
        public DbSet<VehicleModel> Vehicles {get;set;}
    }
}