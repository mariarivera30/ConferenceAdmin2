using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class interfacedocumentMap : EntityTypeConfiguration<interfacedocument>
    {
        public interfacedocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.interfaceDocumentID);

            // Properties
            this.Property(t => t.attibuteName)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.content)
                .HasMaxLength(1073741823);

            // Table & Column Mappings
            this.ToTable("interfacedocument", "conferenceadmin");
            this.Property(t => t.interfaceDocumentID).HasColumnName("interfaceDocumentID");
            this.Property(t => t.attibuteName).HasColumnName("attibuteName");
            this.Property(t => t.content).HasColumnName("content");
        }
    }
}
