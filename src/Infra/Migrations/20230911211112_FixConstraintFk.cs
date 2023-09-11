using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class FixConstraintFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Owner",
                table: "Departments");

            migrationBuilder.AddColumn<Guid>(
                name: "FK_Department_Owned",
                table: "Departments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FK_Department_Owned",
                table: "Departments",
                column: "FK_Department_Owned");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_FK_Department_Owned",
                table: "Departments",
                column: "FK_Department_Owned",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_FK_Department_Owned",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_FK_Department_Owned",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "FK_Department_Owned",
                table: "Departments");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Owner",
                table: "Departments",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
