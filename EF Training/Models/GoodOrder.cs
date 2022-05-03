using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Training.Models
{

    public class GoodOrder
    {
        public int GoodId { get; set; }
        public Good Good { get; set; }

        public int HowMany { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }

    public class GoodOrderEntityTypeConfiguration : IEntityTypeConfiguration<GoodOrder>
    {
        public void Configure(EntityTypeBuilder<GoodOrder> builder)
        {
            builder
                // ВАЖНО - СОСТАВНОЙ КЛЮЧ
                // ВАЖНО - СОСТАВНОЙ КЛЮЧ
                // ВАЖНО - СОСТАВНОЙ КЛЮЧ
                .HasKey(x => new { x.OrderId, x.GoodId });

            builder
                .ToTable("GoodsInOrder");
                

            builder
                .Property(x => x.HowMany)
                .IsRequired();

            builder
                .HasOne(x => x.Good)
                .WithMany(x => x.GoodOrders)
                .HasForeignKey(x => x.GoodId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();

            builder
                .HasOne(x => x.Order)
                .WithMany(x => x.Goods)
                .HasForeignKey(x => x.OrderId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();
        }
    }
}
