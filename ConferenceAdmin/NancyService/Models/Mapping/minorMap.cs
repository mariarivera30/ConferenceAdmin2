using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class minorMap : EntityTypeConfiguration<minor>
    {
        public minorMap()
        {
            // Primary Key
            this.HasKey(t => t.minorsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("minors", "conferenceadmin");
            this.Property(t => t.minorsID).HasColumnName("minorsID");
            this.Property(t => t.userID).HasColumnName("userID");
            this.Property(t => t.authorizationStatus).HasColumnName("authorizationStatus");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.user)
                .WithMany(t => t.minors)
                .HasForeignKey(d => d.userID);

        }
    }
}
