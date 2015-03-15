using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class paymenttypeMap : EntityTypeConfiguration<paymenttype>
    {
        public paymenttypeMap()
        {
            // Primary Key
            this.HasKey(t => t.paymentTypeID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("paymenttype", "conferenceadmin");
            this.Property(t => t.paymentTypeID).HasColumnName("paymentTypeID");
            this.Property(t => t.name).HasColumnName("name");
        }
    }
}
