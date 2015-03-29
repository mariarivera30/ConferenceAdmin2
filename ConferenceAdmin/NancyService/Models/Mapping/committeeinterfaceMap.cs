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
            this.Property(t => t.firstName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.lastName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.affiliation)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.description)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("committeeinterface", "conferenceadmin");
            this.Property(t => t.committeID).HasColumnName("committeID");
            this.Property(t => t.firstName).HasColumnName("firstName");
            this.Property(t => t.lastName).HasColumnName("lastName");
            this.Property(t => t.affiliation).HasColumnName("affiliation");
            this.Property(t => t.description).HasColumnName("description");
        }
    }
}
