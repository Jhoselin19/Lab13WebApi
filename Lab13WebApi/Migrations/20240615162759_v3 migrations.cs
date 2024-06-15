using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab13WebApi.Migrations
{
    /// <inheritdoc />
    public partial class v3migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Customers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Customers");
        }
    }
}
