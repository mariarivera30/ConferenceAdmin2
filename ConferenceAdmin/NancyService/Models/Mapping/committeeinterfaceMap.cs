using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class committeeinterfaceMap : EntityTypeConfiguration<committeeinterface>
    {
        public committeeinterfaceMap()
        {
            // Primary Key
            this.HasKey(t => t.committeID);

            // Properties
            this.Property(t => t.firstNme)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.lastname)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.affiliation)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.description)
                .IsRequired()
                .HasMaxLength(500);

        }
    }
}
