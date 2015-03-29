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
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("topiccategory", "conferenceadmin");
            this.Property(t => t.topiccategoryID).HasColumnName("topiccategoryID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.deleted).HasColumnName("deleted");
        }
    }
}
