using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace journal_service.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JournalEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntryBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Entry = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    JournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntry_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "SocialSecurityNumber" },
                values: new object[,]
                {
                    { new Guid("de0a4ce8-a1b1-40f9-ac05-428f32b2f76c"), "christopher.river@yahoo.com", "Christopher", "River", 1804310031, "611-22-9012" },
                    { new Guid("e67c30c6-3d97-4ea0-ac20-562266fde339"), "john.doe@gmail.com", "John", "Stewart", 829445668, "432-71-6221" },
                    { new Guid("e9389c44-ee85-42bd-9b18-b701b1013f78"), "joaquin.matthews@outlook.com", "Joaquin", "Matthews", 955812204, "322-18-8711" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_JournalId",
                table: "JournalEntry",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_PatientId",
                table: "Journals",
                column: "PatientId",
                unique: true,
                filter: "[PatientId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalEntry");

            migrationBuilder.DropTable(
                name: "Journals");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
