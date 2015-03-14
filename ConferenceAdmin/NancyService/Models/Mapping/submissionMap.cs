using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class submissionMap : EntityTypeConfiguration<submission>
    {
        public submissionMap()
        {
            // Primary Key
            this.HasKey(t => t.submissionID);

            // Properties
            this.Property(t => t.hasApplied)
                .HasMaxLength(45);

            this.Property(t => t.title)
                .HasMaxLength(45);

            this.Property(t => t.status)
                .HasMaxLength(45);


            // Relationships
            this.HasRequired(t => t.submissiontype)
                .WithMany(t => t.submissions)
                .HasForeignKey(d => d.submissionTypeID);
            this.HasRequired(t => t.topiccategory)
                .WithMany(t => t.submissions)
                .HasForeignKey(d => d.topicID);
            this.HasRequired(t => t.user)
                .WithMany(t => t.submissions)
                .HasForeignKey(d => d.userID);

        }
    }
}
