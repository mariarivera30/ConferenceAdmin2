using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class paymentcomplementaryMap : EntityTypeConfiguration<paymentcomplementary>
    {
        public paymentcomplementaryMap()
        {
            // Primary Key
            this.HasKey(t => t.paymentcomplementaryID);

            // Properties

            // Relationships
            this.HasRequired(t => t.complementarykey)
                .WithOptional(t => t.paymentcomplementary);
            this.HasRequired(t => t.payment)
                .WithMany(t => t.paymentcomplementaries)
                .HasForeignKey(d => d.paymentID);

        }
    }
}
