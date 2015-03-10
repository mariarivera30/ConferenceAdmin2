using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class interfaceinformationMap : EntityTypeConfiguration<interfaceinformation>
    {
        public interfaceinformationMap()
        {
            // Primary Key
            this.HasKey(t => t.interfaceID);

            // Properties
            this.Property(t => t.attribute)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.content)
                .IsRequired()
                .HasMaxLength(1000);

        }
    }
}
