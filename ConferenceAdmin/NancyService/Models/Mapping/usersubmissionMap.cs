using NancyService.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Modules.Models.Mapping
{
    public class usersubmissionMap : EntityTypeConfiguration<usersubmission>
    {
        public usersubmissionMap()
        {
            // Primary Key
            this.HasKey(t => t.userSubmissionID);

            // Properties
            // Table & Column Mappings
            this.ToTable("usersubmission", "conferenceadmin");
            this.Property(t => t.userSubmissionID).HasColumnName("userSubmissionID");
            this.Property(t => t.userID).HasColumnName("userID");
            this.Property(t => t.initialSubmissionID).HasColumnName("initialSubmissionID");
            this.Property(t => t.finalSubmissionID).HasColumnName("finalSubmissionID");
            this.Property(t => t.allowFinalVersion).HasColumnName("allowFinalVersion");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasOptional(t => t.submission)
                .WithMany(t => t.usersubmissions)
                .HasForeignKey(d => d.finalSubmissionID);
            this.HasRequired(t => t.submission1)
                .WithMany(t => t.usersubmissions1)
                .HasForeignKey(d => d.initialSubmissionID);
            this.HasRequired(t => t.user)
                .WithMany(t => t.usersubmissions)
                .HasForeignKey(d => d.userID);

        }
    }
}