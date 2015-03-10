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
                .HasMaxLength(500);

        }
    }
}
