using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPortal.MsSql.Migrations
{
    public partial class addedbackups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BackupId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Backups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_BackupId",
                table: "Files",
                column: "BackupId",
                unique: true,
                filter: "[BackupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Backups_BackupId",
                table: "Files",
                column: "BackupId",
                principalTable: "Backups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Backups_BackupId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "Backups");

            migrationBuilder.DropIndex(
                name: "IX_Files_BackupId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "BackupId",
                table: "Files");
        }
    }
}
