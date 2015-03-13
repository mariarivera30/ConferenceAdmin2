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

        }
    }
}
