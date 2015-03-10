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

        }
    }
}
