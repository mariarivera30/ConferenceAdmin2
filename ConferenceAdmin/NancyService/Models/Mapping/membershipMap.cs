using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class membershipMap : EntityTypeConfiguration<membership>
    {
        public membershipMap()
        {
            // Primary Key
            this.HasKey(t => t.membershipID);

            // Properties
            this.Property(t => t.email)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.password)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.confirmationKey)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("memberships", "conferenceadmin");
            this.Property(t => t.membershipID).HasColumnName("membershipID");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.password).HasColumnName("password");
            this.Property(t => t.emailConfirmation).HasColumnName("emailConfirmation");
            this.Property(t => t.confirmationKey).HasColumnName("confirmationKey");
            this.Property(t => t.deleted).HasColumnName("deleted");

        }
    }
}
