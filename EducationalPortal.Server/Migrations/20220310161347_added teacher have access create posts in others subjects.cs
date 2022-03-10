using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPortal.Server.Migrations
{
    public partial class addedteacherhaveaccesscreatepostsinotherssubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GradeModelSubjectModel",
                columns: table => new
                {
                    GradesHaveAccessReadId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectsHaveAccessReadId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeModelSubjectModel", x => new { x.GradesHaveAccessReadId, x.SubjectsHaveAccessReadId });
                    table.ForeignKey(
                        name: "FK_GradeModelSubjectModel_Grades_GradesHaveAccessReadId",
                        column: x => x.GradesHaveAccessReadId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeModelSubjectModel_Subjects_SubjectsHaveAccessReadId",
                        column: x => x.SubjectsHaveAccessReadId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubjectModelUserModel",
                columns: table => new
                {
                    SubjectHaveAccessCreatePostsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeachersHaveAccessCreatePostsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectModelUserModel", x => new { x.SubjectHaveAccessCreatePostsId, x.TeachersHaveAccessCreatePostsId });
                    table.ForeignKey(
                        name: "FK_SubjectModelUserModel_Subjects_SubjectHaveAccessCreatePostsId",
                        column: x => x.SubjectHaveAccessCreatePostsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectModelUserModel_Users_TeachersHaveAccessCreatePostsId",
                        column: x => x.TeachersHaveAccessCreatePostsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GradeModelSubjectModel_SubjectsHaveAccessReadId",
                table: "GradeModelSubjectModel",
                column: "SubjectsHaveAccessReadId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectModelUserModel_TeachersHaveAccessCreatePostsId",
                table: "SubjectModelUserModel",
                column: "TeachersHaveAccessCreatePostsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeModelSubjectModel");

            migrationBuilder.DropTable(
                name: "SubjectModelUserModel");
        }
    }
}
