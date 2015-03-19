using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class workshopMap : EntityTypeConfiguration<workshop>
    {
        public workshopMap()
        {
            // Primary Key
            this.HasKey(t => t.workshopID);

            // Properties
            this.Property(t => t.duration)
                .HasMaxLength(45);

            this.Property(t => t.delivery)
                .HasMaxLength(45);

            this.Property(t => t.plan)
                .HasMaxLength(45);

            this.Property(t => t.necessary_equipment)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("workshops", "conferenceadmin");
            this.Property(t => t.workshopID).HasColumnName("workshopID");
            this.Property(t => t.submissionID).HasColumnName("submissionID");
            this.Property(t => t.duration).HasColumnName("duration");
            this.Property(t => t.delivery).HasColumnName("delivery");
            this.Property(t => t.plan).HasColumnName("plan");
            this.Property(t => t.necessary_equipment).HasColumnName("necessary equipment");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.submission)
                .WithMany(t => t.workshops)
                .HasForeignKey(d => d.submissionID);

        }
    }
}
