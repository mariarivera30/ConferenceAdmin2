using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class companionminorMap : EntityTypeConfiguration<companionminor>
    {
        public companionminorMap()
        {
            // Primary Key
            this.HasKey(t => t.companionminorID);

            // Properties

            // Relationships
            this.HasOptional(t => t.companion)
                .WithMany(t => t.companionminors)
                .HasForeignKey(d => d.companionID);
            this.HasOptional(t => t.minor)
                .WithMany(t => t.companionminors)
                .HasForeignKey(d => d.minorID);

        }
    }
}
