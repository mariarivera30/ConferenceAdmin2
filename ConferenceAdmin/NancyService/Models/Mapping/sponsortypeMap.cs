using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class sponsortypeMap : EntityTypeConfiguration<sponsortype>
    {
        public sponsortypeMap()
        {
            // Primary Key
            this.HasKey(t => t.sponsortypeID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.amount)
                .HasMaxLength(45);

            this.Property(t => t.benefit1)
                .HasMaxLength(45);

            this.Property(t => t.benefit2)
                .HasMaxLength(45);

            this.Property(t => t.benefit3)
                .HasMaxLength(45);

            this.Property(t => t.benefit4)
                .HasMaxLength(45);

            this.Property(t => t.benefit5)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("sponsortype", "conferenceadmin");
            this.Property(t => t.sponsortypeID).HasColumnName("sponsortypeID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.amount).HasColumnName("amount");
            this.Property(t => t.benefit1).HasColumnName("benefit1");
            this.Property(t => t.benefit2).HasColumnName("benefit2");
            this.Property(t => t.benefit3).HasColumnName("benefit3");
            this.Property(t => t.benefit4).HasColumnName("benefit4");
            this.Property(t => t.benefit5).HasColumnName("benefit5");
        }
    }
}
