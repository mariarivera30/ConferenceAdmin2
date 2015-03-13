using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class paymentMap : EntityTypeConfiguration<payment>
    {
        public paymentMap()
        {
            // Primary Key
            this.HasKey(t => t.paymentID);

            // Properties

            // Relationships
            this.HasRequired(t => t.paymenttype)
                .WithMany(t => t.payments)
                .HasForeignKey(d => d.paymentTypeID);

        }
    }
}
