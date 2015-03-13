using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class panelMap : EntityTypeConfiguration<panel>
    {
        public panelMap()
        {
            // Primary Key
            this.HasKey(t => t.panelsID);

            // Properties
            this.Property(t => t.panelistNames)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.plan)
                .HasMaxLength(500);

            this.Property(t => t.guideQuestion)
                .HasMaxLength(1000);

            this.Property(t => t.formatDescription)
                .HasMaxLength(500);

            this.Property(t => t.necessaryEquipment)
                .HasMaxLength(500);


            // Relationships
            this.HasRequired(t => t.submission)
                .WithMany(t => t.panels)
                .HasForeignKey(d => d.submissionID);

        }
    }
}
