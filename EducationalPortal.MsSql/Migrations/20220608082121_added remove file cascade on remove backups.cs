using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPortal.MsSql.Migrations
{
    public partial class addedremovefilecascadeonremovebackups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Backups_BackupId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Backups_BackupId",
                table: "Files",
                column: "BackupId",
                principalTable: "Backups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Backups_BackupId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Backups_BackupId",
                table: "Files",
                column: "BackupId",
                principalTable: "Backups",
                principalColumn: "Id");
        }
    }
}
