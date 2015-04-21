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
            // Table & Column Mappings
            this.ToTable("payment", "conferenceadmin");
            this.Property(t => t.paymentID).HasColumnName("paymentID");
            this.Property(t => t.paymentTypeID).HasColumnName("paymentTypeID");
            this.Property(t => t.creationDate).HasColumnName("creationDate");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.paymenttype)
                .WithMany(t => t.payments)
                .HasForeignKey(d => d.paymentTypeID);

        }
    }
}
