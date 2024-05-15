using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganisationManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Departments_DepartmentId",
                table: "Classrooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Departments_DepartmentId",
                table: "Classrooms",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Departments_DepartmentId",
                table: "Classrooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Departments_DepartmentId",
                table: "Classrooms",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
