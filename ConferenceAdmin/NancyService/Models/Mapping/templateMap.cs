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
                .HasMaxLength(45);

            this.Property(t => t.document)
                .IsRequired()
                .HasMaxLength(2000);

        }
    }
}
