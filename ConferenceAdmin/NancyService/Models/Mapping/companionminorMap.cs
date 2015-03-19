using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class companionminorMap : EntityTypeConfiguration<companionminor>
    {
        public companionminorMap()
        {
            // Primary Key
            this.HasKey(t => t.companionminorID);

            // Properties
            // Table & Column Mappings
            this.ToTable("companionminor", "conferenceadmin");
            this.Property(t => t.companionminorID).HasColumnName("companionminorID");
            this.Property(t => t.minorID).HasColumnName("minorID");
            this.Property(t => t.companionID).HasColumnName("companionID");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.companion)
                .WithMany(t => t.companionminors)
                .HasForeignKey(d => d.companionID);
            this.HasRequired(t => t.minor)
                .WithMany(t => t.companionminors)
                .HasForeignKey(d => d.minorID);

        }
    }
}
