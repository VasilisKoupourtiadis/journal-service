using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace journal_service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJournalPatientPointerToNotBeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Patients_PatientId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_PatientId",
                table: "Journals");

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
                name: "PatientId",
                table: "Journals",
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
                    { new Guid("120144f7-91b3-4d00-be49-b63e0e5fe849"), "christopher.river@yahoo.com", "Christopher", "River", 1804310031, "611-22-9012" },
                    { new Guid("4e055ff0-8d8d-448c-857c-a055eb651470"), "john.doe@gmail.com", "John", "Stewart", 829445668, "432-71-6221" },
                    { new Guid("91b604f9-5019-4598-bf72-b66261222ec9"), "joaquin.matthews@outlook.com", "Joaquin", "Matthews", 955812204, "322-18-8711" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journals_PatientId",
                table: "Journals",
                column: "PatientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Patients_PatientId",
                table: "Journals",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Patients_PatientId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_PatientId",
                table: "Journals");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("120144f7-91b3-4d00-be49-b63e0e5fe849"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("4e055ff0-8d8d-448c-857c-a055eb651470"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("91b604f9-5019-4598-bf72-b66261222ec9"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PatientId",
                table: "Journals",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber", "SocialSecurityNumber" },
                values: new object[,]
                {
                    { new Guid("55dd6e19-f2f4-4fe0-9451-a1c0ea043086"), "joaquin.matthews@outlook.com", "Joaquin", "Matthews", 955812204, "322-18-8711" },
                    { new Guid("97238e06-cece-46c5-af38-05bd16fb7882"), "john.doe@gmail.com", "John", "Stewart", 829445668, "432-71-6221" },
                    { new Guid("ad05fd12-c40f-4eb2-9d77-91a2165df7fc"), "christopher.river@yahoo.com", "Christopher", "River", 1804310031, "611-22-9012" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journals_PatientId",
                table: "Journals",
                column: "PatientId",
                unique: true,
                filter: "[PatientId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Patients_PatientId",
                table: "Journals",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
