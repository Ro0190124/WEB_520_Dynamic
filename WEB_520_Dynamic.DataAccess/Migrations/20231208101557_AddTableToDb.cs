using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_520_Dynamic.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NGUOI_DUNGs",
                columns: table => new
                {
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNguoiDung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenTaiKhoan = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NGUOI_DUNGs", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "NHA_CUNG_CAPs",
                columns: table => new
                {
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MaSoThue = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SoTaiKhoan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NguoiDaiDien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NHA_CUNG_CAPs", x => x.MaNhaCungCap);
                });

            migrationBuilder.CreateTable(
                name: "SAN_PHAMs",
                columns: table => new
                {
                    MaSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    DonVi = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    QuyCach = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAN_PHAMs", x => x.MaSanPham);
                });

            migrationBuilder.CreateTable(
                name: "BIEN_LAIs",
                columns: table => new
                {
                    MaBienLai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiBienLai = table.Column<bool>(type: "bit", nullable: false),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: true),
                    ThongTinGiaoHang = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIEN_LAIs", x => x.MaBienLai);
                    table.ForeignKey(
                        name: "FK_BIEN_LAIs_NGUOI_DUNGs_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NGUOI_DUNGs",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BIEN_LAIs_NHA_CUNG_CAPs_MaNhaCungCap",
                        column: x => x.MaNhaCungCap,
                        principalTable: "NHA_CUNG_CAPs",
                        principalColumn: "MaNhaCungCap");
                });

            migrationBuilder.CreateTable(
                name: "BIEN_LAI_CHI_TIETs",
                columns: table => new
                {
                    MaBienLai = table.Column<int>(type: "int", nullable: false),
                    MaLo = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIEN_LAI_CHI_TIETs", x => x.MaBienLai);
                    table.ForeignKey(
                        name: "FK_BIEN_LAI_CHI_TIETs_BIEN_LAIs_MaBienLai",
                        column: x => x.MaBienLai,
                        principalTable: "BIEN_LAIs",
                        principalColumn: "MaBienLai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LOs",
                columns: table => new
                {
                    MaLo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    HanSuDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BIEN_LAI_CHI_TIETMaBienLai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOs", x => x.MaLo);
                    table.ForeignKey(
                        name: "FK_LOs_BIEN_LAI_CHI_TIETs_BIEN_LAI_CHI_TIETMaBienLai",
                        column: x => x.BIEN_LAI_CHI_TIETMaBienLai,
                        principalTable: "BIEN_LAI_CHI_TIETs",
                        principalColumn: "MaBienLai");
                    table.ForeignKey(
                        name: "FK_LOs_SAN_PHAMs_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SAN_PHAMs",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BIEN_LAI_CHI_TIETs_MaLo",
                table: "BIEN_LAI_CHI_TIETs",
                column: "MaLo");

            migrationBuilder.CreateIndex(
                name: "IX_BIEN_LAIs_MaNguoiDung",
                table: "BIEN_LAIs",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_BIEN_LAIs_MaNhaCungCap",
                table: "BIEN_LAIs",
                column: "MaNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_LOs_BIEN_LAI_CHI_TIETMaBienLai",
                table: "LOs",
                column: "BIEN_LAI_CHI_TIETMaBienLai");

            migrationBuilder.CreateIndex(
                name: "IX_LOs_MaSanPham",
                table: "LOs",
                column: "MaSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_BIEN_LAI_CHI_TIETs_LOs_MaLo",
                table: "BIEN_LAI_CHI_TIETs",
                column: "MaLo",
                principalTable: "LOs",
                principalColumn: "MaLo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIEN_LAI_CHI_TIETs_BIEN_LAIs_MaBienLai",
                table: "BIEN_LAI_CHI_TIETs");

            migrationBuilder.DropForeignKey(
                name: "FK_BIEN_LAI_CHI_TIETs_LOs_MaLo",
                table: "BIEN_LAI_CHI_TIETs");

            migrationBuilder.DropTable(
                name: "BIEN_LAIs");

            migrationBuilder.DropTable(
                name: "NGUOI_DUNGs");

            migrationBuilder.DropTable(
                name: "NHA_CUNG_CAPs");

            migrationBuilder.DropTable(
                name: "LOs");

            migrationBuilder.DropTable(
                name: "BIEN_LAI_CHI_TIETs");

            migrationBuilder.DropTable(
                name: "SAN_PHAMs");
        }
    }
}
