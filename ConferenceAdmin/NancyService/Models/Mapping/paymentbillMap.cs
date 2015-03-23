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
                .HasMaxLength(1000);

            this.Property(t => t.methodOfPayment)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.creditCardNumber)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("paymentbill", "conferenceadmin");
            this.Property(t => t.paymentBillID).HasColumnName("paymentBillID");
            this.Property(t => t.paymentID).HasColumnName("paymentID");
            this.Property(t => t.addressID).HasColumnName("addressID");
            this.Property(t => t.transactionid).HasColumnName("transactionid");
            this.Property(t => t.AmountPaid).HasColumnName("AmountPaid");
            this.Property(t => t.methodOfPayment).HasColumnName("methodOfPayment");
            this.Property(t => t.creditCardNumber).HasColumnName("creditCardNumber");
            this.Property(t => t.deleted).HasColumnName("deleted");
            this.Property(t => t.cardExpirationDate).HasColumnName("cardExpirationDate");

            // Relationships
            this.HasOptional(t => t.address)
                .WithMany(t => t.paymentbills)
                .HasForeignKey(d => d.addressID);
            this.HasRequired(t => t.payment)
                .WithMany(t => t.paymentbills)
                .HasForeignKey(d => d.paymentID);

        }
    }
}
