using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnCoSo.Migrations
{
    /// <inheritdoc />
    public partial class themluongthang2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "luongThangs",
                columns: table => new
                {
                    LTId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LId = table.Column<int>(type: "int", nullable: false),
                    NgayTL = table.Column<DateOnly>(type: "date", nullable: false),
                    LuongThoaThuan = table.Column<int>(type: "int", nullable: false),
                    NgayNghiCoLuong = table.Column<int>(type: "int", nullable: true),
                    NgayNghiKhongLuong = table.Column<int>(type: "int", nullable: true),
                    KhenThuong = table.Column<int>(type: "int", nullable: true),
                    KyLuat = table.Column<int>(type: "int", nullable: true),
                    PhuCap = table.Column<int>(type: "int", nullable: true),
                    UngTruoc = table.Column<int>(type: "int", nullable: true),
                    ThucLanh = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_luongThangs", x => x.LTId);
                    table.ForeignKey(
                        name: "FK_luongThangs_bangLuongs_LId",
                        column: x => x.LId,
                        principalTable: "bangLuongs",
                        principalColumn: "LId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_luongThangs_LId",
                table: "luongThangs",
                column: "LId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "luongThangs");
        }
    }
}
