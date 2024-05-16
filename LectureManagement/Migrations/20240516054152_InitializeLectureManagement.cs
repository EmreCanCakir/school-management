using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitializeLectureManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Credit = table.Column<float>(type: "real", nullable: false),
                    HoursInWeek = table.Column<int>(type: "int", nullable: false),
                    Quota = table.Column<int>(type: "int", nullable: false),
                    IsGroup = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LectureInstructors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LectureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureInstructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureInstructors_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureInstructors_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LecturePrerequisite",
                columns: table => new
                {
                    LectureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrerequisiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturePrerequisite", x => new { x.LectureId, x.PrerequisiteId });
                    table.ForeignKey(
                        name: "FK_LecturePrerequisite_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LecturePrerequisite_Lectures_PrerequisiteId",
                        column: x => x.PrerequisiteId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LectureSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LectureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassroomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureSchedules_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureSchedules_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LectureStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LectureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureStudents_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureStudents_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYears_EndDate",
                table: "AcademicYears",
                column: "EndDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYears_StartDate",
                table: "AcademicYears",
                column: "StartDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LectureInstructors_AcademicYearId",
                table: "LectureInstructors",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureInstructors_LectureId",
                table: "LectureInstructors",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_LecturePrerequisite_PrerequisiteId",
                table: "LecturePrerequisite",
                column: "PrerequisiteId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_AcademicYearId",
                table: "LectureSchedules",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSchedules_LectureId",
                table: "LectureSchedules",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureStudents_AcademicYearId",
                table: "LectureStudents",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureStudents_LectureId",
                table: "LectureStudents",
                column: "LectureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LectureInstructors");

            migrationBuilder.DropTable(
                name: "LecturePrerequisite");

            migrationBuilder.DropTable(
                name: "LectureSchedules");

            migrationBuilder.DropTable(
                name: "LectureStudents");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "Lectures");
        }
    }
}
