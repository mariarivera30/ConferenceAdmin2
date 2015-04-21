using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class usertypeMap : EntityTypeConfiguration<usertype>
    {
        public usertypeMap()
        {
            // Primary Key
            this.HasKey(t => t.userTypeID);

            // Properties
            this.Property(t => t.userTypeName)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.description)
                .HasMaxLength(16777215);

            // Table & Column Mappings
            this.ToTable("usertype", "conferenceadmin");
            this.Property(t => t.userTypeID).HasColumnName("userTypeID");
            this.Property(t => t.userTypeName).HasColumnName("userTypeName");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.registrationCost).HasColumnName("registrationCost");
            this.Property(t => t.registrationLateFee).HasColumnName("registrationLateFee");
        }
    }
}
