using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Training.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public IEnumerable<GoodOrder> Goods { get; set; }

    }

    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .ToTable("Orders")
                .HasKey(t => t.Id);

            builder
                .Property(x => x.Date)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.Shop)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ShopId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();

            builder
                .HasOne(x => x.Client)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ClientId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();
        }
    }
}
