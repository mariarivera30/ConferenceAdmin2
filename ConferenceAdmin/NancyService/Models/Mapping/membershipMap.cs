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
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("memberships", "conferenceadmin");
            this.Property(t => t.membershipID).HasColumnName("membershipID");
            this.Property(t => t.membershipTypeID).HasColumnName("membershipTypeID");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.password).HasColumnName("password");
            this.Property(t => t.creationDate).HasColumnName("creationDate");
            this.Property(t => t.deletionDate).HasColumnName("deletionDate");
            this.Property(t => t.emailConfirmation).HasColumnName("emailConfirmation");

            // Relationships
            this.HasRequired(t => t.membershiptype)
                .WithMany(t => t.memberships)
                .HasForeignKey(d => d.membershipTypeID);

        }
    }
}
