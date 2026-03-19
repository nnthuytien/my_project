using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class suakhoangoai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chiTietPhongBans_phongBans_MaNhanVien",
                table: "chiTietPhongBans");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietPhongBans_PBId",
                table: "chiTietPhongBans",
                column: "PBId");

            migrationBuilder.AddForeignKey(
                name: "FK_chiTietPhongBans_phongBans_PBId",
                table: "chiTietPhongBans",
                column: "PBId",
                principalTable: "phongBans",
                principalColumn: "PBId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chiTietPhongBans_phongBans_PBId",
                table: "chiTietPhongBans");

            migrationBuilder.DropIndex(
                name: "IX_chiTietPhongBans_PBId",
                table: "chiTietPhongBans");

            migrationBuilder.AddForeignKey(
                name: "FK_chiTietPhongBans_phongBans_MaNhanVien",
                table: "chiTietPhongBans",
                column: "MaNhanVien",
                principalTable: "phongBans",
                principalColumn: "PBId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
