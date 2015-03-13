using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class topiccategoryMap : EntityTypeConfiguration<topiccategory>
    {
        public topiccategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.topiccategoryID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(45);

        }
    }
}
