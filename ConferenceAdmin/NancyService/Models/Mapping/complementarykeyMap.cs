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
            this.Property(t => t.key)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("complementarykey", "conferenceadmin");
            this.Property(t => t.complementarykeyID).HasColumnName("complementarykeyID");
            this.Property(t => t.sponsorID2).HasColumnName("sponsorID2");
            this.Property(t => t.key).HasColumnName("key");
            this.Property(t => t.isUsed).HasColumnName("isUsed");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
           
            this.HasRequired(t => t.sponsor2)
                .WithMany(t => t.complementarykeys)
                .HasForeignKey(d => d.sponsorID2);
        }
    }
}
