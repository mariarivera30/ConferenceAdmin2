using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class complementarykeyMap : EntityTypeConfiguration<complementarykey>
    {
        public complementarykeyMap()
        {
            // Primary Key
            this.HasKey(t => t.complementarykeyID);

            // Properties
            this.Property(t => t.isUsed)
                .HasMaxLength(45);


            // Relationships
            this.HasRequired(t => t.sponsor)
                .WithMany(t => t.complementarykeys)
                .HasForeignKey(d => d.sponsorID);

        }
    }
}
