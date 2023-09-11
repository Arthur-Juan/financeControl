using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class SlugInDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Departments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Departments",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_Id",
                table: "Departments",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_Id",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Departments");
        }
    }
}
