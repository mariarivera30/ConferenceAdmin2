using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class authorizationsubmittedMap : EntityTypeConfiguration<authorizationsubmitted>
    {
        public authorizationsubmittedMap()
        {
            // Primary Key
            this.HasKey(t => t.authorizationSubmittedID);

            // Properties
            this.Property(t => t.documentFile)
                .IsRequired()
                .HasMaxLength(1073741823);

            this.Property(t => t.documentName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("authorizationsubmitted", "conferenceadmin");
            this.Property(t => t.authorizationSubmittedID).HasColumnName("authorizationSubmittedID");
            this.Property(t => t.minorID).HasColumnName("minorID");
            this.Property(t => t.documentFile).HasColumnName("documentFile");
            this.Property(t => t.documentName).HasColumnName("documentName");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.minor)
                .WithMany(t => t.authorizationsubmitteds)
                .HasForeignKey(d => d.minorID);

        }
    }
}
