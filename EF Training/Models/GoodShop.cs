using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Training.Models
{
    public class GoodShop
    {
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
  
        public int HowMany { get; set; }

        public int GoodId { get; set; }
        public Good Good { get; set; }
    }

    public class GoodShopEntityTypeConfiguration : IEntityTypeConfiguration<GoodShop>
    {
        public void Configure(EntityTypeBuilder<GoodShop> builder)
        {
                // ВАЖНО - СОСТАВНОЙ КЛЮЧ
                // ВАЖНО - СОСТАВНОЙ КЛЮЧ
                // ВАЖНО - СОСТАВНОЙ КЛЮЧ
            builder
                .HasKey(x => new {x.GoodId, x.ShopId});
            builder
                .ToTable("GoodsInShops");


            builder
                .HasOne(x => x.Shop)
                .WithMany(x => x.ShopGoods)
                .HasForeignKey(x => x.ShopId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();

            builder
                .HasOne(x => x.Good)
                .WithMany(x => x.GoodShops)
                .HasForeignKey(x => x.GoodId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();
        }
    }
}
