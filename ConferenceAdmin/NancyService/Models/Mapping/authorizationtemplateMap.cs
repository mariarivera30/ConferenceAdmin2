using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class authorizationtemplateMap : EntityTypeConfiguration<authorizationtemplate>
    {
        public authorizationtemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.authorizationID);

            // Properties
            this.Property(t => t.authorizationName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.authorizationDocument)
                .IsRequired()
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("authorizationtemplates", "conferenceadmin");
            this.Property(t => t.authorizationID).HasColumnName("authorizationID");
            this.Property(t => t.authorizationName).HasColumnName("authorizationName");
            this.Property(t => t.authorizationDocument).HasColumnName("authorizationDocument");
            this.Property(t => t.deleted).HasColumnName("deleted");
        }
    }
}
