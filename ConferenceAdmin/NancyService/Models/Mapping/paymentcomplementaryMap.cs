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
            // Table & Column Mappings
            this.ToTable("paymentcomplementary", "conferenceadmin");
            this.Property(t => t.paymentcomplementaryID).HasColumnName("paymentcomplementaryID");
            this.Property(t => t.paymentID).HasColumnName("paymentID");
            this.Property(t => t.complementaryKeyID).HasColumnName("complementaryKeyID");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.complementarykey)
                .WithMany(t => t.paymentcomplementaries)
                .HasForeignKey(d => d.complementaryKeyID);
            this.HasOptional(t => t.payment)
                .WithMany(t => t.paymentcomplementaries)
                .HasForeignKey(d => d.paymentID);

        
        }
    }
}
