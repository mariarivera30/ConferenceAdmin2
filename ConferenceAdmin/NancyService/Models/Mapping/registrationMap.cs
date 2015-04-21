using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class registrationMap : EntityTypeConfiguration<registration>
    {
        public registrationMap()
        {
            // Primary Key
            this.HasKey(t => t.registrationID);

            // Properties
            this.Property(t => t.note)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("registration", "conferenceadmin");
            this.Property(t => t.registrationID).HasColumnName("registrationID");
            this.Property(t => t.userID).HasColumnName("userID");
            this.Property(t => t.paymentID).HasColumnName("paymentID");
            this.Property(t => t.note).HasColumnName("note");
            this.Property(t => t.date1).HasColumnName("date1");
            this.Property(t => t.date2).HasColumnName("date2");
            this.Property(t => t.date3).HasColumnName("date3");
            this.Property(t => t.byAdmin).HasColumnName("byAdmin");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.payment)
                .WithMany(t => t.registrations)
                .HasForeignKey(d => d.paymentID);
            this.HasRequired(t => t.user)
                .WithMany(t => t.registrations)
                .HasForeignKey(d => d.userID);

        }
    }
}
