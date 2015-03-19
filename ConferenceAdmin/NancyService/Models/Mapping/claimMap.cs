using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class claimMap : EntityTypeConfiguration<claim>
    {
        public claimMap()
        {
            // Primary Key
            this.HasKey(t => t.claimsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("claims", "conferenceadmin");
            this.Property(t => t.claimsID).HasColumnName("claimsID");
            this.Property(t => t.privilegesID).HasColumnName("privilegesID");
            this.Property(t => t.userID).HasColumnName("userID");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasOptional(t => t.privilege)
                .WithMany(t => t.claims)
                .HasForeignKey(d => d.privilegesID);
            this.HasOptional(t => t.user)
                .WithMany(t => t.claims)
                .HasForeignKey(d => d.userID);

        }
    }
}
