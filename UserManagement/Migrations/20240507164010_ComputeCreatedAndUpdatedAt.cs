using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class ComputeCreatedAndUpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Position",
                table: "user_details");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "user_details",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "user_details",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05c5d63e-b174-4a03-8626-1ecadbe73e5e", "2", "Lecturer", "LECTURER" },
                    { "57f92ec8-692c-49f9-9126-5ebb51919944", "3", "Admin", "ADMIN" },
                    { "e2352e09-d8f2-4d87-b860-1e9181381b29", "1", "Student", "STUDENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_details_UserId",
                table: "user_details",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_details_AspNetUsers_UserId",
                table: "user_details",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                keyValue: "05c5d63e-b174-4a03-8626-1ecadbe73e5e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57f92ec8-692c-49f9-9126-5ebb51919944");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2352e09-d8f2-4d87-b860-1e9181381b29");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "user_details");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "user_details",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "user_details",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
