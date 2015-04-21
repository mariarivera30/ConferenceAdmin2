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
                .HasMaxLength(255);

            this.Property(t => t.lastName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.title)
                .HasMaxLength(255);

            this.Property(t => t.affiliationName)
                .HasMaxLength(255);

            this.Property(t => t.phone)
                .HasMaxLength(45);

            this.Property(t => t.userFax)
                .HasMaxLength(45);

            this.Property(t => t.registrationStatus)
                .HasMaxLength(45);

            this.Property(t => t.acceptanceStatus)
                .HasMaxLength(45);

            this.Property(t => t.evaluatorStatus)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("user", "conferenceadmin");
            this.Property(t => t.userID).HasColumnName("userID");
            this.Property(t => t.membershipID).HasColumnName("membershipID");
            this.Property(t => t.userTypeID).HasColumnName("userTypeID");
            this.Property(t => t.firstName).HasColumnName("firstName");
            this.Property(t => t.lastName).HasColumnName("lastName");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.affiliationName).HasColumnName("affiliationName");
            this.Property(t => t.phone).HasColumnName("phone");
            this.Property(t => t.addressID).HasColumnName("addressID");
            this.Property(t => t.userFax).HasColumnName("userFax");
            this.Property(t => t.registrationStatus).HasColumnName("registrationStatus");
            this.Property(t => t.hasApplied).HasColumnName("hasApplied");
            this.Property(t => t.acceptanceStatus).HasColumnName("acceptanceStatus");
            this.Property(t => t.evaluatorStatus).HasColumnName("evaluatorStatus");
            this.Property(t => t.deleted).HasColumnName("deleted");

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
