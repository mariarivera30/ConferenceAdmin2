using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class userMap : EntityTypeConfiguration<user>
    {
        public userMap()
        {
            // Primary Key
            this.HasKey(t => t.userID);

            // Properties
            this.Property(t => t.firstName)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.lastName)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.title)
                .HasMaxLength(45);

            this.Property(t => t.affiliationName)
                .HasMaxLength(45);

            this.Property(t => t.phone)
                .HasMaxLength(45);

            this.Property(t => t.userFax)
                .HasMaxLength(45);

            this.Property(t => t.registrationStatus)
                .HasMaxLength(45);

            this.Property(t => t.hasApplied)
                .HasMaxLength(45);

            this.Property(t => t.acceptanceStatus)
                .HasMaxLength(45);


            // Relationships
            this.HasRequired(t => t.address)
                .WithMany(t => t.users)
                .HasForeignKey(d => d.addressID);
            this.HasRequired(t => t.membership)
                .WithMany(t => t.users)
                .HasForeignKey(d => d.membershipID);
            this.HasRequired(t => t.usertype)
                .WithMany(t => t.users)
                .HasForeignKey(d => d.userTypeID);

        }
    }
}
