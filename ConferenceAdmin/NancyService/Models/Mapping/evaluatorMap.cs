using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class evaluatorMap : EntityTypeConfiguration<evaluator>
    {
        public evaluatorMap()
        {
            // Primary Key
            this.HasKey(t => t.evaluatorsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("evaluators", "conferenceadmin");
            this.Property(t => t.evaluatorsID).HasColumnName("evaluatorsID");
            this.Property(t => t.userID).HasColumnName("userID");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasRequired(t => t.user)
                .WithMany(t => t.evaluators)
                .HasForeignKey(d => d.userID);

        }
    }
}
