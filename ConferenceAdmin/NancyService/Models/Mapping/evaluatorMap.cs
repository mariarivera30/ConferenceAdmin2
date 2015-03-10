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

            // Relationships
            this.HasRequired(t => t.user)
                .WithMany(t => t.evaluators)
                .HasForeignKey(d => d.userID);

        }
    }
}
