using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class templatesubmissionMap : EntityTypeConfiguration<templatesubmission>
    {
        public templatesubmissionMap()
        {
            // Primary Key
            this.HasKey(t => t.templatesubmissionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("templatesubmission", "conferenceadmin");
            this.Property(t => t.templatesubmissionID).HasColumnName("templatesubmissionID");
            this.Property(t => t.templateID).HasColumnName("templateID");
            this.Property(t => t.submissionID).HasColumnName("submissionID");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.submission)
                .WithMany(t => t.templatesubmissions)
                .HasForeignKey(d => d.submissionID);
            this.HasRequired(t => t.template)
                .WithMany(t => t.templatesubmissions)
                .HasForeignKey(d => d.templateID);

        }
    }
}
