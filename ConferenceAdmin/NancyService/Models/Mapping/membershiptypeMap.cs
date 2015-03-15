using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class membershiptypeMap : EntityTypeConfiguration<membershiptype>
    {
        public membershiptypeMap()
        {
            // Primary Key
            this.HasKey(t => t.membershipTypeID);

            // Properties
            this.Property(t => t.membershipTypeName)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("membershiptype", "conferenceadmin");
            this.Property(t => t.membershipTypeID).HasColumnName("membershipTypeID");
            this.Property(t => t.membershipTypeName).HasColumnName("membershipTypeName");
        }
    }
}
