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
            this.Property(t => t.document)
                .IsRequired()
                .HasMaxLength(2000);

            this.Property(t => t.documentssubmittedcol)
                .HasMaxLength(45);


            // Relationships
            this.HasRequired(t => t.submission)
                .WithMany(t => t.documentssubmitteds)
                .HasForeignKey(d => d.submissionID);

        }
    }
}
