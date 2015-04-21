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
                .HasMaxLength(16777215);

            this.Property(t => t.plan)
                .HasMaxLength(600);

            this.Property(t => t.guideQuestion)
                .HasMaxLength(16777215);

            this.Property(t => t.formatDescription)
                .HasMaxLength(16777215);

            this.Property(t => t.necessaryEquipment)
                .HasMaxLength(16777215);

            // Table & Column Mappings
            this.ToTable("panels", "conferenceadmin");
            this.Property(t => t.panelsID).HasColumnName("panelsID");
            this.Property(t => t.submissionID).HasColumnName("submissionID");
            this.Property(t => t.panelistNames).HasColumnName("panelistNames");
            this.Property(t => t.plan).HasColumnName("plan");
            this.Property(t => t.guideQuestion).HasColumnName("guideQuestion");
            this.Property(t => t.formatDescription).HasColumnName("formatDescription");
            this.Property(t => t.necessaryEquipment).HasColumnName("necessaryEquipment");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.submission)
                .WithMany(t => t.panels)
                .HasForeignKey(d => d.submissionID);

        }
    }
}
