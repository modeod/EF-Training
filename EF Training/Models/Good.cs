using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Training.Models
{
    public class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string?[] Tags { get; set; }
        public ICollection<GoodShop> GoodShops { get; set; }
        public ICollection<GoodOrder> GoodOrders { get; set; }

    }

    public class GoodEntityTypeConfiguration : IEntityTypeConfiguration<Good>
    {
        public void Configure(EntityTypeBuilder<Good> builder)
        {
            builder
                .ToTable("Goods")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

        }
    }
}
