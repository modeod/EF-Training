using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Training.Models
{
    public class Pasport
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTimeOffset? DOB { get; set; }
        public string? Citizenship { get; set; }
    }

    public class PasportEntityTypeConfiguration : IEntityTypeConfiguration<Pasport>
    {
        public void Configure(EntityTypeBuilder<Pasport> builder)
        {
            builder
               .ToTable("Clients");
        }
    }
}
