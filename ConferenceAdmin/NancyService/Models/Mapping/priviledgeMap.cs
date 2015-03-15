using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class priviledgeMap : EntityTypeConfiguration<priviledge>
    {
        public priviledgeMap()
        {
            // Primary Key
            this.HasKey(t => t.priviledgesID);

            // Properties
            this.Property(t => t.priviledgestType)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("priviledges", "conferenceadmin");
            this.Property(t => t.priviledgesID).HasColumnName("priviledgesID");
            this.Property(t => t.priviledgestType).HasColumnName("priviledgestType");
        }
    }
}
