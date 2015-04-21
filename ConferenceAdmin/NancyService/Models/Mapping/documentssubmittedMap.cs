using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class documentssubmittedMap : EntityTypeConfiguration<documentssubmitted>
    {
        public documentssubmittedMap()
        {
            // Primary Key
            this.HasKey(t => t.documentssubmittedID);

            // Properties
            this.Property(t => t.documentName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.document)
                .IsRequired()
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("documentssubmitted", "conferenceadmin");
            this.Property(t => t.documentssubmittedID).HasColumnName("documentssubmittedID");
            this.Property(t => t.submissionID).HasColumnName("submissionID");
            this.Property(t => t.documentName).HasColumnName("documentName");
            this.Property(t => t.document).HasColumnName("document");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.submission)
                .WithMany(t => t.documentssubmitteds)
                .HasForeignKey(d => d.submissionID);

        }
    }
}
