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
            this.Property(t => t.evaluationFile)
                .IsRequired()
                .HasMaxLength(2000);

            this.Property(t => t.publicFeedback)
                .HasMaxLength(1000);

            this.Property(t => t.privateFeedback)
                .HasMaxLength(1000);


            // Relationships
            this.HasRequired(t => t.evaluatiorsubmission)
                .WithMany(t => t.evaluationsubmitteds)
                .HasForeignKey(d => d.evaluatiorSubmissionID);

        }
    }
}
