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
                .HasMaxLength(2000);

            this.Property(t => t.documentName)
                .IsRequired()
                .HasMaxLength(45);


            // Relationships
            this.HasRequired(t => t.minor)
                .WithMany(t => t.authorizationsubmitteds)
                .HasForeignKey(d => d.minorID);

        }
    }
}
