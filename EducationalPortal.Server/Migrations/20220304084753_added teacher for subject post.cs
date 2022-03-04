using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPortal.Server.Migrations
{
    public partial class addedteacherforsubjectpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "SubjectPost",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPost_TeacherId",
                table: "SubjectPost",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectPost_Users_TeacherId",
                table: "SubjectPost",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectPost_Users_TeacherId",
                table: "SubjectPost");

            migrationBuilder.DropIndex(
                name: "IX_SubjectPost_TeacherId",
                table: "SubjectPost");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SubjectPost");
        }
    }
}
