using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class sponsor2Map : EntityTypeConfiguration<sponsor2>
    {
        public sponsor2Map()
        {
            // Primary Key
            this.HasKey(t => t.sponsorID);

            // Properties
            this.Property(t => t.logo)
                .HasMaxLength(1073741823);

          

            // Table & Column Mappings
            this.ToTable("sponsor2", "conferenceadmin");
            this.Property(t => t.sponsorID).HasColumnName("sponsorID");
            this.Property(t => t.logo).HasColumnName("logo");
            this.Property(t => t.deleted).HasColumnName("deleted");
            this.Property(t => t.active).HasColumnName("active");
            this.Property(t => t.sponsorType).HasColumnName("sponsorType");
            this.Property(t => t.userID).HasColumnName("userID");
            this.Property(t => t.paymentID).HasColumnName("paymentID");
            this.Property(t => t.totalAmount).HasColumnName("totalAmount");
            this.Property(t => t.byAdmin).HasColumnName("byAdmin");
            this.Property(t => t.emailInfo).HasColumnName("emailInfo");
            // Relationships
            this.HasOptional(t => t.sponsortype1)
                .WithMany(t => t.sponsors2)
                .HasForeignKey(d => d.sponsorType);
            this.HasOptional(t => t.user)
                .WithMany(t => t.sponsors2)
                .HasForeignKey(d => d.userID);
            this.HasRequired(t => t.payment)
                .WithMany(t => t.sponsors2)
                .HasForeignKey(d => d.paymentID);

        }
    }
}
