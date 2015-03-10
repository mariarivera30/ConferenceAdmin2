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


            // Relationships
            this.HasOptional(t => t.membershiptype)
                .WithMany(t => t.memberships)
                .HasForeignKey(d => d.membershipTypeID);

        }
    }
}
