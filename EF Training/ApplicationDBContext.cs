using EF_Training.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Training
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<GoodShop> GoodsShops { get; set; }
        public DbSet<GoodOrder> GoodsOrders { get; set; }
        public DbSet<Client> Clients { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder modelBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<ApplicationDBContext>()
                .Build();

            Console.WriteLine(configuration.GetDebugView());
            var connectionString = configuration.GetConnectionString("DefaultDB");

            modelBuilder
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(
                Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);

            var goodsTagsValueCompare = new ValueComparer<string[]>(
            (x, y) => x.SequenceEqual(y, StringComparer.OrdinalIgnoreCase),
            x => x.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode(StringComparison.OrdinalIgnoreCase))),
            x => x.ToArray()
            );

            var goodsTagsConverter = new ValueConverter<string[], string>(
                x => string.Join(";", x),
                x => x.Split(';', StringSplitOptions.RemoveEmptyEntries).ToArray())
                ;

            modelBuilder
                .Entity<Good>()
                .Property(x => x.Tags)
                .HasConversion(goodsTagsConverter, goodsTagsValueCompare);

        }

    }
}
