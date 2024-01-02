using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace journal_service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOneToManyRelationshipBetweenJournalAndJournalEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntry_Journals_JournalId",
                table: "JournalEntry");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("de0a4ce8-a1b1-40f9-ac05-428f32b2f76c"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("e67c30c6-3d97-4ea0-ac20-562266fde339"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("e9389c44-ee85-42bd-9b18-b701b1013f78"));

            migrationBuilder.AlterColumn<Guid>(
                name: "JournalId",
                table: "JournalEntry",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "SocialSecurityNumber" },
                values: new object[,]
                {
                    { new Guid("55dd6e19-f2f4-4fe0-9451-a1c0ea043086"), "joaquin.matthews@outlook.com", "Joaquin", "Matthews", 955812204, "322-18-8711" },
                    { new Guid("97238e06-cece-46c5-af38-05bd16fb7882"), "john.doe@gmail.com", "John", "Stewart", 829445668, "432-71-6221" },
                    { new Guid("ad05fd12-c40f-4eb2-9d77-91a2165df7fc"), "christopher.river@yahoo.com", "Christopher", "River", 1804310031, "611-22-9012" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntry_Journals_JournalId",
                table: "JournalEntry",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntry_Journals_JournalId",
                table: "JournalEntry");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("55dd6e19-f2f4-4fe0-9451-a1c0ea043086"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("97238e06-cece-46c5-af38-05bd16fb7882"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("ad05fd12-c40f-4eb2-9d77-91a2165df7fc"));

            migrationBuilder.AlterColumn<Guid>(
                name: "JournalId",
                table: "JournalEntry",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "SocialSecurityNumber" },
                values: new object[,]
                {
                    { new Guid("de0a4ce8-a1b1-40f9-ac05-428f32b2f76c"), "christopher.river@yahoo.com", "Christopher", "River", 1804310031, "611-22-9012" },
                    { new Guid("e67c30c6-3d97-4ea0-ac20-562266fde339"), "john.doe@gmail.com", "John", "Stewart", 829445668, "432-71-6221" },
                    { new Guid("e9389c44-ee85-42bd-9b18-b701b1013f78"), "joaquin.matthews@outlook.com", "Joaquin", "Matthews", 955812204, "322-18-8711" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntry_Journals_JournalId",
                table: "JournalEntry",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id");
        }
    }
}
