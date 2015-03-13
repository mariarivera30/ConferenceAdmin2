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
