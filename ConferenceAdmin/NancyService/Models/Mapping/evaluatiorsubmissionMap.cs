using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class evaluatiorsubmissionMap : EntityTypeConfiguration<evaluatiorsubmission>
    {
        public evaluatiorsubmissionMap()
        {
            // Primary Key
            this.HasKey(t => t.evaluationsubmissionID);

            // Properties
            this.Property(t => t.statusEvaluation)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("evaluatiorsubmission", "conferenceadmin");
            this.Property(t => t.evaluationsubmissionID).HasColumnName("evaluationsubmissionID");
            this.Property(t => t.evaluatorID).HasColumnName("evaluatorID");
            this.Property(t => t.submissionID).HasColumnName("submissionID");
            this.Property(t => t.statusEvaluation).HasColumnName("statusEvaluation");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.evaluator)
                .WithMany(t => t.evaluatiorsubmissions)
                .HasForeignKey(d => d.evaluatorID);
            this.HasRequired(t => t.submission)
                .WithMany(t => t.evaluatiorsubmissions)
                .HasForeignKey(d => d.submissionID);

        }
    }
}
