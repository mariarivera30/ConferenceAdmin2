using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class companionMap : EntityTypeConfiguration<companion>
    {
        public companionMap()
        {
            // Primary Key
            this.HasKey(t => t.companionID);

            // Properties
            this.Property(t => t.companionKey)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("companion", "conferenceadmin");
            this.Property(t => t.companionID).HasColumnName("companionID");
            this.Property(t => t.userID).HasColumnName("userID");
            this.Property(t => t.companionKey).HasColumnName("companionKey");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.user)
                .WithMany(t => t.companions)
                .HasForeignKey(d => d.userID);

        }
    }
}
