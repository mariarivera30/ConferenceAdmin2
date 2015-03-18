using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class privilegeMap : EntityTypeConfiguration<privilege>
    {
        public privilegeMap()
        {
            // Primary Key
            this.HasKey(t => t.privilegesID);

            // Properties
            this.Property(t => t.privilegestType)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("privileges", "conferenceadmin");
            this.Property(t => t.privilegesID).HasColumnName("privilegesID");
            this.Property(t => t.privilegestType).HasColumnName("privilegestType");
        }
    }
}
