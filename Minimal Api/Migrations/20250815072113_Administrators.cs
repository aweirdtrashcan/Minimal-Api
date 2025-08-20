using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class Administrators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_administrators",
                table: "administrators");

            migrationBuilder.RenameTable(
                name: "administrators",
                newName: "Administrators");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators");

            migrationBuilder.RenameTable(
                name: "Administrators",
                newName: "administrators");

            migrationBuilder.AddPrimaryKey(
                name: "PK_administrators",
                table: "administrators",
                column: "Id");
        }
    }
}
