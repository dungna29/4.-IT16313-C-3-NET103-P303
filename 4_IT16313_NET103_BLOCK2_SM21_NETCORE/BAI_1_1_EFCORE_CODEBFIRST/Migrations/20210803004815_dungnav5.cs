using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BAI_1_1_EFCORE_CODEBFIRST.Migrations
{
    public partial class dungnav5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdRole",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_IdRole",
                table: "Accounts",
                column: "IdRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Roles_IdRole",
                table: "Accounts",
                column: "IdRole",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Roles_IdRole",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_IdRole",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IdRole",
                table: "Accounts");
        }
    }
}
