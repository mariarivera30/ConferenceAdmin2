using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class submissionMap : EntityTypeConfiguration<submission>
    {
        public submissionMap()
        {
            // Primary Key
            this.HasKey(t => t.submissionID);

            // Properties
            this.Property(t => t.submissionAbstract)
                .HasMaxLength(8000);

            this.Property(t => t.title)
                .HasMaxLength(45);

            this.Property(t => t.status)
                .HasMaxLength(45);


            // Relationships
            this.HasRequired(t => t.submissiontype)
                .WithMany(t => t.submissions)
                .HasForeignKey(d => d.submissionTypeID);
            this.HasRequired(t => t.topiccategory)
                .WithMany(t => t.submissions)
                .HasForeignKey(d => d.topicID);
            this.HasRequired(t => t.user)
                .WithMany(t => t.submissions)
                .HasForeignKey(d => d.userID);

            // Table & Column Mappings
            this.ToTable("submissions", "conferenceadmin");
            this.Property(t => t.submissionID).HasColumnName("submissionID");
            this.Property(t => t.userID).HasColumnName("userID");
            this.Property(t => t.topicID).HasColumnName("topicID");
            this.Property(t => t.submissionTypeID).HasColumnName("submissionTypeID");
            this.Property(t => t.submissionAbstract).HasColumnName("submissionAbstract");
            this.Property(t => t.title).HasColumnName("title");
            this.Property(t => t.status).HasColumnName("status");
            this.Property(t => t.creationDate).HasColumnName("creationDate");
            this.Property(t => t.byAdmin).HasColumnName("byAdmin");
            this.Property(t => t.deleted).HasColumnName("deleted");

        }
    }
}
