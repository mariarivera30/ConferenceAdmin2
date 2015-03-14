using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class companionMap : EntityTypeConfiguration<companion>
    {
        public companionMap()
        {
            // Primary Key
            this.HasKey(t => t.companionID);

            // Properties
            this.Property(t => t.companionKey)
                .HasMaxLength(45);


            // Relationships
            this.HasRequired(t => t.user)
                .WithMany(t => t.companions)
                .HasForeignKey(d => d.userID);

        }
    }
}
