using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class themluongthang3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nam",
                table: "luongThangs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Thang",
                table: "luongThangs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nam",
                table: "luongThangs");

            migrationBuilder.DropColumn(
                name: "Thang",
                table: "luongThangs");
        }
    }
}
