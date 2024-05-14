using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganisationManagement.Migrations
{
    /// <inheritdoc />
    public partial class DiscardClassroomWithFacultyDependency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Faculties_FacultyId",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_FacultyId",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Classrooms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FacultyId",
                table: "Classrooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_FacultyId",
                table: "Classrooms",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Faculties_FacultyId",
                table: "Classrooms",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
