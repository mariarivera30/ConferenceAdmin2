using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class evaluationsubmittedMap : EntityTypeConfiguration<evaluationsubmitted>
    {
        public evaluationsubmittedMap()
        {
            // Primary Key
            this.HasKey(t => t.evaluationsubmittedID);

            // Properties
            this.Property(t => t.evaluationName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.evaluationFile)
                .IsRequired()
                .HasMaxLength(1073741823);

            this.Property(t => t.publicFeedback)
                .HasMaxLength(16777215);

            this.Property(t => t.privateFeedback)
                .HasMaxLength(16777215);

            // Table & Column Mappings
            this.ToTable("evaluationsubmitted", "conferenceadmin");
            this.Property(t => t.evaluationsubmittedID).HasColumnName("evaluationsubmittedID");
            this.Property(t => t.evaluatiorSubmissionID).HasColumnName("evaluatiorSubmissionID");
            this.Property(t => t.evaluationName).HasColumnName("evaluationName");
            this.Property(t => t.evaluationFile).HasColumnName("evaluationFile");
            this.Property(t => t.score).HasColumnName("score");
            this.Property(t => t.publicFeedback).HasColumnName("publicFeedback");
            this.Property(t => t.privateFeedback).HasColumnName("privateFeedback");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.evaluatiorsubmission)
                .WithMany(t => t.evaluationsubmitteds)
                .HasForeignKey(d => d.evaluatiorSubmissionID);

        
        }
    }
}
