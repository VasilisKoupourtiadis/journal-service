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
    [Migration("20231225125356_InitialCreate")]
    partial class InitialCreate
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

                    b.Property<Guid?>("JournalId")
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
                            Id = new Guid("e67c30c6-3d97-4ea0-ac20-562266fde339"),
                            Email = "john.doe@gmail.com",
                            FirstName = "John",
                            LastName = "Stewart",
                            PhoneNumber = 829445668,
                            SocialSecurityNumber = "432-71-6221"
                        },
                        new
                        {
                            Id = new Guid("de0a4ce8-a1b1-40f9-ac05-428f32b2f76c"),
                            Email = "christopher.river@yahoo.com",
                            FirstName = "Christopher",
                            LastName = "River",
                            PhoneNumber = 1804310031,
                            SocialSecurityNumber = "611-22-9012"
                        },
                        new
                        {
                            Id = new Guid("e9389c44-ee85-42bd-9b18-b701b1013f78"),
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
                    b.HasOne("journal_service.Domain.Journal", null)
                        .WithMany("Entries")
                        .HasForeignKey("JournalId");
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