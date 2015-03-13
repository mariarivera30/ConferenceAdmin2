using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class addressMap : EntityTypeConfiguration<address>
    {
        public addressMap()
        {
            // Primary Key
            this.HasKey(t => t.addressID);

            // Properties
            this.Property(t => t.line1)
                .HasMaxLength(80);

            this.Property(t => t.line2)
                .HasMaxLength(80);

            this.Property(t => t.city)
                .HasMaxLength(45);

            this.Property(t => t.state)
                .HasMaxLength(45);

            this.Property(t => t.country)
                .HasMaxLength(45);

            this.Property(t => t.zipcode)
                .HasMaxLength(45);

        }
    }
}
