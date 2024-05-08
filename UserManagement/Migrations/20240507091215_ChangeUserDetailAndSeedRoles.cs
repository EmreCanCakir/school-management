using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserDetailAndSeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_user_details_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "user_details");

            migrationBuilder.DropColumn(
                name: "Faculty",
                table: "user_details");

            migrationBuilder.RenameColumn(
                name: "TeacherStatus",
                table: "user_details",
                newName: "LecturerStatus");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "user_details",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "user_details",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "user_details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "user_details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "user_details",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1829cde2-583f-4ecc-9751-8ace702cf901", "3", "Admin", "ADMIN" },
                    { "1e705930-0c62-4d96-9c76-5b0ec9ec1827", "1", "Student", "STUDENT" },
                    { "a781ac63-4428-4efb-ba7a-bbe6e2e1075c", "2", "Lecturer", "LECTURER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_details_UserId",
                table: "user_details",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_user_details_AspNetUsers_UserId",
                table: "user_details",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_details_AspNetUsers_UserId",
                table: "user_details");

            migrationBuilder.DropIndex(
                name: "IX_user_details_UserId",
                table: "user_details");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1829cde2-583f-4ecc-9751-8ace702cf901");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e705930-0c62-4d96-9c76-5b0ec9ec1827");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a781ac63-4428-4efb-ba7a-bbe6e2e1075c");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "user_details");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "user_details");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "user_details");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "user_details");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "user_details");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "user_details");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "user_details");

            migrationBuilder.RenameColumn(
                name: "LecturerStatus",
                table: "user_details",
                newName: "TeacherStatus");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "user_details",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Faculty",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_user_details_Id",
                table: "AspNetUsers",
                column: "Id",
                principalTable: "user_details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
