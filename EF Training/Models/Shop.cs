using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Training.Models
{
    public class Shop
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Director { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<GoodShop> ShopGoods { get; set; }

    }

    public class ShopEntityTypeConfiguration : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder
                .ToTable("Shops")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();
        }
    }
}
