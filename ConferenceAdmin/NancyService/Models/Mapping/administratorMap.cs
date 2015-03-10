using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class administratorMap : EntityTypeConfiguration<administrator>
    {
        public administratorMap()
        {
            // Primary Key
            this.HasKey(t => t.administratorsID);

            // Properties

            // Relationships
            this.HasRequired(t => t.membership)
                .WithMany(t => t.administrators)
                .HasForeignKey(d => d.membershipID);
            this.HasRequired(t => t.priviledge)
                .WithMany(t => t.administrators)
                .HasForeignKey(d => d.priviledgesID);

        }
    }
}
