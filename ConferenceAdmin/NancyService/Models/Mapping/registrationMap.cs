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
            this.Property(t => t.date3)
                .HasMaxLength(45);


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
