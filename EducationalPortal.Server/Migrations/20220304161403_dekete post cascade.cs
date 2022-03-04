using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPortal.Server.Migrations
{
    public partial class deketepostcascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectPost_Subjects_SubjectId",
                table: "SubjectPost");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectPost_Subjects_SubjectId",
                table: "SubjectPost",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectPost_Subjects_SubjectId",
                table: "SubjectPost");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectPost_Subjects_SubjectId",
                table: "SubjectPost",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
