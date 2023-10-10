using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MealOrdering.Server.Data.Context
{
    public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<MealOrderingDbContext>
    {
        public MealOrderingDbContext CreateDbContext(string[] args)
        {
            String connectionString = "User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=mealordering;SearchPath=public";

            var builder = new DbContextOptionsBuilder<MealOrderingDbContext>();

            builder.UseNpgsql(connectionString);
            return new MealOrderingDbContext(builder.Options);
        }

    }
}
