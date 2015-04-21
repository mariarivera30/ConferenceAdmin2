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
                .HasMaxLength(200);

            this.Property(t => t.line2)
                .HasMaxLength(200);

            this.Property(t => t.city)
                .HasMaxLength(100);

            this.Property(t => t.state)
                .HasMaxLength(100);

            this.Property(t => t.country)
                .HasMaxLength(100);

            this.Property(t => t.zipcode)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("address", "conferenceadmin");
            this.Property(t => t.addressID).HasColumnName("addressID");
            this.Property(t => t.line1).HasColumnName("line1");
            this.Property(t => t.line2).HasColumnName("line2");
            this.Property(t => t.city).HasColumnName("city");
            this.Property(t => t.state).HasColumnName("state");
            this.Property(t => t.country).HasColumnName("country");
            this.Property(t => t.zipcode).HasColumnName("zipcode");
        }
    }
}
