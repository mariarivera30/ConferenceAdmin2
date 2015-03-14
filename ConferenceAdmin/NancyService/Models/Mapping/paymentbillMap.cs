using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class paymentbillMap : EntityTypeConfiguration<paymentbill>
    {
        public paymentbillMap()
        {
            // Primary Key
            this.HasKey(t => t.paymentBillID);

            // Properties
            this.Property(t => t.transactionid)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.methodOfPayment)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.creditCardNumber)
                .HasMaxLength(45);


            // Relationships
            this.HasOptional(t => t.payment)
                .WithMany(t => t.paymentbills)
                .HasForeignKey(d => d.paymentID);

        }
    }
}
