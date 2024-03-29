﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace journal_service.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240102120703_UpdateOneToManyRelationshipBetweenJournalAndJournalEntry")]
    partial class UpdateOneToManyRelationshipBetweenJournalAndJournalEntry
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("journal_service.Domain.Journal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PatientId")
                        .IsUnique()
                        .HasFilter("[PatientId] IS NOT NULL");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("journal_service.Domain.JournalEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Entry")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("EntryBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("JournalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("JournalId");

                    b.ToTable("JournalEntry");
                });

            modelBuilder.Entity("journal_service.Domain.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("SocialSecurityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = new Guid("97238e06-cece-46c5-af38-05bd16fb7882"),
                            Email = "john.doe@gmail.com",
                            FirstName = "John",
                            LastName = "Stewart",
                            PhoneNumber = 829445668,
                            SocialSecurityNumber = "432-71-6221"
                        },
                        new
                        {
                            Id = new Guid("ad05fd12-c40f-4eb2-9d77-91a2165df7fc"),
                            Email = "christopher.river@yahoo.com",
                            FirstName = "Christopher",
                            LastName = "River",
                            PhoneNumber = 1804310031,
                            SocialSecurityNumber = "611-22-9012"
                        },
                        new
                        {
                            Id = new Guid("55dd6e19-f2f4-4fe0-9451-a1c0ea043086"),
                            Email = "joaquin.matthews@outlook.com",
                            FirstName = "Joaquin",
                            LastName = "Matthews",
                            PhoneNumber = 955812204,
                            SocialSecurityNumber = "322-18-8711"
                        });
                });

            modelBuilder.Entity("journal_service.Domain.Journal", b =>
                {
                    b.HasOne("journal_service.Domain.Patient", "Patient")
                        .WithOne("Journal")
                        .HasForeignKey("journal_service.Domain.Journal", "PatientId");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("journal_service.Domain.JournalEntry", b =>
                {
                    b.HasOne("journal_service.Domain.Journal", "Journal")
                        .WithMany("Entries")
                        .HasForeignKey("JournalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Journal");
                });

            modelBuilder.Entity("journal_service.Domain.Journal", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("journal_service.Domain.Patient", b =>
                {
                    b.Navigation("Journal");
                });
#pragma warning restore 612, 618
        }
    }
}
