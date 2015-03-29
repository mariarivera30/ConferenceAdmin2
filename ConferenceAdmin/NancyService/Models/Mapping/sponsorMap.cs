using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class sponsorMap : EntityTypeConfiguration<sponsor>
    {
        public sponsorMap()
        {
            // Primary Key
            this.HasKey(t => t.sponsorID);

            // Properties
            this.Property(t => t.firstName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.lastName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.title)
                .HasMaxLength(100);

            this.Property(t => t.company)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.phone)
                .HasMaxLength(45);

            this.Property(t => t.email)
                .HasMaxLength(100);

            this.Property(t => t.logo)
                .HasMaxLength(1073741823);


            // Relationships
            this.HasOptional(t => t.address)
                .WithMany(t => t.sponsors)
                .HasForeignKey(d => d.addressID);
            this.HasRequired(t => t.payment)
                .WithMany(t => t.sponsors)
                .HasForeignKey(d => d.paymentID);
            this.HasRequired(t => t.sponsortype1)
                .WithMany(t => t.sponsors)
                .HasForeignKey(d => d.sponsorType);

            // Table & Column Mappings
            this.ToTable("sponsor", "conferenceadmin");
            this.Property(t => t.sponsorID).HasColumnName("sponsorID");
            this.Property(t => t.paymentID).HasColumnName("paymentID");
            this.Property(t => t.sponsorType).HasColumnName("sponsorType");
            this.Property(t => t.addressID).HasColumnName("addressID");
            this.Property(t => t.firstName).HasColumnName("firstName");
            this.Property(t => t.lastName).HasColumnName("lastName");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.company).HasColumnName("company");
            this.Property(t => t.phone).HasColumnName("phone");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.logo).HasColumnName("logo");
            this.Property(t => t.deleted).HasColumnName("deleted");
            this.Property(t => t.quantityComplemetary).HasColumnName("quantityComplemetary");
        }
    }
}
