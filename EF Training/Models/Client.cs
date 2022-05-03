using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Training.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nickname { get; set; }

        public Pasport? Pasport { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .ToTable("Clients")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Nickname)
                .IsRequired();

            builder
                .HasOne(x => x.Pasport)
                .WithOne()
                .HasForeignKey<Pasport>(x => x.Id);
        }
    }
}
