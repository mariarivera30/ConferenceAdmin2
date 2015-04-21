using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NancyService.Models.Mapping
{
    public class templateMap : EntityTypeConfiguration<template>
    {
        public templateMap()
        {
            // Primary Key
            this.HasKey(t => t.templateID);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.document)
                .IsRequired()
                .HasMaxLength(1073741823);

            this.Property(t => t.topic)
                .HasMaxLength(255);

            this.Property(t => t.description)
                .HasMaxLength(16777215);

            // Table & Column Mappings
            this.ToTable("templates", "conferenceadmin");
            this.Property(t => t.templateID).HasColumnName("templateID");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.document).HasColumnName("document");
            this.Property(t => t.deleted).HasColumnName("deleted");
            this.Property(t => t.topic).HasColumnName("topic");
            this.Property(t => t.description).HasColumnName("description");
        }
    }
}
