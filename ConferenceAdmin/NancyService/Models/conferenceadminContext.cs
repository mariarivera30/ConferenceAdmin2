using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using NancyService.Models.Mapping;
using NancyService.Modules.Models.Mapping;

namespace NancyService.Models
{
    public partial class conferenceadminContext : DbContext
    {
        static conferenceadminContext()
        {
            Database.SetInitializer<conferenceadminContext>(null);
        }

        public conferenceadminContext()
            : base("Name=conferenceadminContext")
        {
        }

        public DbSet<address> addresses { get; set; }
        public DbSet<authorizationsubmitted> authorizationsubmitteds { get; set; }
        public DbSet<authorizationtemplate> authorizationtemplates { get; set; }
        public DbSet<claim> claims { get; set; }
        public DbSet<committeeinterface> committeeinterfaces { get; set; }
        public DbSet<companion> companions { get; set; }
        public DbSet<companionminor> companionminors { get; set; }
        public DbSet<complementarykey> complementarykeys { get; set; }
        public DbSet<documentssubmitted> documentssubmitteds { get; set; }
        public DbSet<evaluationsubmitted> evaluationsubmitteds { get; set; }
        public DbSet<evaluatiorsubmission> evaluatiorsubmissions { get; set; }
        public DbSet<evaluator> evaluators { get; set; }
        public DbSet<interfacedocument> interfacedocuments { get; set; }
        public DbSet<interfaceinformation> interfaceinformations { get; set; }
        public DbSet<membership> memberships { get; set; }
        public DbSet<minor> minors { get; set; }
        public DbSet<panel> panels { get; set; }
        public DbSet<payment> payments { get; set; }
        public DbSet<paymentbill> paymentbills { get; set; }
        public DbSet<paymentcomplementary> paymentcomplementaries { get; set; }
        public DbSet<paymenttype> paymenttypes { get; set; }
        public DbSet<privilege> privileges { get; set; }
        public DbSet<registration> registrations { get; set; }
        public DbSet<sponsortype> sponsortypes { get; set; }
        public DbSet<submission> submissions { get; set; }
        public DbSet<submissiontype> submissiontypes { get; set; }
        public DbSet<template> templates { get; set; }
        public DbSet<templatesubmission> templatesubmissions { get; set; }
        public DbSet<topiccategory> topiccategories { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<usersubmission> usersubmission { get; set; }
        public DbSet<usertype> usertypes { get; set; }
        public DbSet<workshop> workshops { get; set; }
        public DbSet<sponsor2> sponsor2 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new addressMap());
            modelBuilder.Configurations.Add(new authorizationsubmittedMap());
            modelBuilder.Configurations.Add(new authorizationtemplateMap());
            modelBuilder.Configurations.Add(new claimMap());
            modelBuilder.Configurations.Add(new committeeinterfaceMap());
            modelBuilder.Configurations.Add(new companionMap());
            modelBuilder.Configurations.Add(new companionminorMap());
            modelBuilder.Configurations.Add(new complementarykeyMap());
            modelBuilder.Configurations.Add(new documentssubmittedMap());
            modelBuilder.Configurations.Add(new evaluationsubmittedMap());
            modelBuilder.Configurations.Add(new evaluatiorsubmissionMap());
            modelBuilder.Configurations.Add(new evaluatorMap());
            modelBuilder.Configurations.Add(new interfacedocumentMap());
            modelBuilder.Configurations.Add(new interfaceinformationMap());
            modelBuilder.Configurations.Add(new membershipMap());
            modelBuilder.Configurations.Add(new minorMap());
            modelBuilder.Configurations.Add(new panelMap());
            modelBuilder.Configurations.Add(new paymentMap());
            modelBuilder.Configurations.Add(new paymentbillMap());
            modelBuilder.Configurations.Add(new paymentcomplementaryMap());
            modelBuilder.Configurations.Add(new paymenttypeMap());
            modelBuilder.Configurations.Add(new privilegeMap());
            modelBuilder.Configurations.Add(new registrationMap());
            modelBuilder.Configurations.Add(new sponsortypeMap());
            modelBuilder.Configurations.Add(new submissionMap());
            modelBuilder.Configurations.Add(new submissiontypeMap());
            modelBuilder.Configurations.Add(new templateMap());
            modelBuilder.Configurations.Add(new templatesubmissionMap());
            modelBuilder.Configurations.Add(new topiccategoryMap());
            modelBuilder.Configurations.Add(new userMap());
            modelBuilder.Configurations.Add(new usersubmissionMap());
            modelBuilder.Configurations.Add(new usertypeMap());
            modelBuilder.Configurations.Add(new workshopMap());
            modelBuilder.Configurations.Add(new sponsor2Map());
        }
    }
}
