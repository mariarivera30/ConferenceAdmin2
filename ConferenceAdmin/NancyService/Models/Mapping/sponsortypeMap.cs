using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class sponsortypeMap : EntityTypeConfiguration<sponsortype>
    {
        public sponsortypeMap()
        {
            // Primary Key
            this.HasKey(t => t.sponsortypeID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.amount)
                .HasMaxLength(45);

            this.Property(t => t.benefit1)
                .HasMaxLength(45);

            this.Property(t => t.benefit2)
                .HasMaxLength(45);

            this.Property(t => t.benefit3)
                .HasMaxLength(45);

            this.Property(t => t.benefit4)
                .HasMaxLength(45);

            this.Property(t => t.benefit5)
                .HasMaxLength(45);

            this.Property(t => t.sponsortypecol)
                .HasMaxLength(45);
            

        }
    }
}
