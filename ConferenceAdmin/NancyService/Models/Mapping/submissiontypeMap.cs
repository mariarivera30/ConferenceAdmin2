using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class submissiontypeMap : EntityTypeConfiguration<submissiontype>
    {
        public submissiontypeMap()
        {
            // Primary Key
            this.HasKey(t => t.submissiontypeID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.description)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("submissiontype", "conferenceadmin");
            this.Property(t => t.submissiontypeID).HasColumnName("submissiontypeID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.deadline).HasColumnName("deadline");
        }
    }
}
