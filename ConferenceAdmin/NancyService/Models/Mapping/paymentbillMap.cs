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
                .HasMaxLength(255);

            this.Property(t => t.methodOfPayment)
                .HasMaxLength(255);

            this.Property(t => t.firstName)
                .HasMaxLength(255);

            this.Property(t => t.lastName)
                .HasMaxLength(255);

            this.Property(t => t.email)
                .HasMaxLength(255);

            this.Property(t => t.telephone)
                .HasMaxLength(45);

            this.Property(t => t.ip)
                .HasMaxLength(45);

            this.Property(t => t.tandemID)
                .HasMaxLength(255);

            this.Property(t => t.batchID)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("paymentbill", "conferenceadmin");
            this.Property(t => t.paymentBillID).HasColumnName("paymentBillID");
            this.Property(t => t.paymentID).HasColumnName("paymentID");
            this.Property(t => t.addressID).HasColumnName("addressID");
            this.Property(t => t.transactionid).HasColumnName("transactionid");
            this.Property(t => t.AmountPaid).HasColumnName("AmountPaid");
            this.Property(t => t.methodOfPayment).HasColumnName("methodOfPayment");
            this.Property(t => t.deleted).HasColumnName("deleted");
            this.Property(t => t.firstName).HasColumnName("firstName");
            this.Property(t => t.lastName).HasColumnName("lastName");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.telephone).HasColumnName("telephone");
            this.Property(t => t.ip).HasColumnName("ip");
            this.Property(t => t.tandemID).HasColumnName("tandemID");
            this.Property(t => t.batchID).HasColumnName("batchID");
            this.Property(t => t.quantity).HasColumnName("quantity");
            this.Property(t => t.date).HasColumnName("date");
            this.Property(t => t.completed).HasColumnName("completed");

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
