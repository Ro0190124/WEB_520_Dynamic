﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEB_520_Dynamic.DataAccess.Data;

#nullable disable

namespace WEB_520_Dynamic.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231212134418_AddTableToDb")]
    partial class AddTableToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WEB_520_Dynamic.Model.BIEN_LAI", b =>
                {
                    b.Property<int>("MaBienLai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaBienLai"));

                    b.Property<string>("GhiChu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LoaiBienLai")
                        .HasColumnType("bit");

                    b.Property<int>("MaNguoiDung")
                        .HasColumnType("int");

                    b.Property<int?>("MaNhaCungCap")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayGiao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayLap")
                        .HasColumnType("datetime2");

                    b.Property<string>("ThongTinGiaoHang")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<byte>("TrangThai")
                        .HasColumnType("tinyint");

                    b.HasKey("MaBienLai");

                    b.HasIndex("MaNguoiDung");

                    b.HasIndex("MaNhaCungCap");

                    b.ToTable("BIEN_LAIs");
                });

            modelBuilder.Entity("WEB_520_Dynamic.Model.BIEN_LAI_CHI_TIET", b =>
                {
                    b.Property<int>("MaBienLaiChiTiet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaBienLaiChiTiet"));

                    b.Property<int>("MaBienLai")
                        .HasColumnType("int");

                    b.Property<int>("MaLo")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("MaBienLaiChiTiet");

                    b.HasIndex("MaBienLai");

                    b.HasIndex("MaLo");

                    b.ToTable("BIEN_LAI_CHI_TIETs");
                });

            modelBuilder.Entity("WEB_520_Dynamic.Model.LO", b =>
                {
                    b.Property<int>("MaLo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLo"));

                    b.Property<int?>("BIEN_LAI_CHI_TIETMaBienLaiChiTiet")
                        .HasColumnType("int");

                    b.Property<DateTime>("HanSuDung")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaSanPham")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenLo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaLo");

                    b.HasIndex("BIEN_LAI_CHI_TIETMaBienLaiChiTiet");

                    b.HasIndex("MaSanPham");

                    b.ToTable("LOs");
                });

            modelBuilder.Entity("WEB_520_Dynamic.Model.NGUOI_DUNG", b =>
                {
                    b.Property<int>("MaNguoiDung")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNguoiDung"));

                    b.Property<bool?>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TenNguoiDung")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TenTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaNguoiDung");

                    b.ToTable("NGUOI_DUNGs");
                });

            modelBuilder.Entity("WEB_520_Dynamic.Model.NHA_CUNG_CAP", b =>
                {
                    b.Property<int>("MaNhaCungCap")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNhaCungCap"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("MaSoThue")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("NguoiDaiDien")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("SoTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("TenNhaCungCap")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaNhaCungCap");

                    b.ToTable("NHA_CUNG_CAPs");
                });

            modelBuilder.Entity("WEB_520_Dynamic.Model.SAN_PHAM", b =>
                {
                    b.Property<int>("MaSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaSanPham"));

                    b.Property<double>("DonGia")
                        .HasColumnType("float");

                    b.Property<string>("DonVi")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("QuyCach")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaSanPham");

                    b.ToTable("SAN_PHAMs");
                });

            modelBuilder.Entity("WEB_520_Dynamic.Model.BIEN_LAI", b =>
                {
                    b.HasOne("WEB_520_Dynamic.Model.NGUOI_DUNG", "NGUOI_DUNG")
                        .WithMany()
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB_520_Dynamic.Model.NHA_CUNG_CAP", "NHA_CUNG_CAP")
                        .WithMany()
                        .HasForeignKey("MaNhaCungCap");

                    b.Navigation("NGUOI_DUNG");

                    b.Navigation("NHA_CUNG_CAP");
                });

            modelBuilder.Entity("WEB_520_Dynamic.Model.BIEN_LAI_CHI_TIET", b =>
                {
                    b.HasOne("WEB_520_Dynamic.Model.BIEN_LAI", "BIEN_LAI")
                        .WithMany()
                        .HasForeignKey("MaBienLai")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEB_520_Dynamic.Model.LO", "LO")
                        .WithMany()
                        .HasForeignKey("MaLo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BIEN_LAI");

                    b.Navigation("LO");
                });

            modelBuilder.Entity("WEB_520_Dynamic.Model.LO", b =>
                {
                    b.HasOne("WEB_520_Dynamic.Model.BIEN_LAI_CHI_TIET", null)
                        .WithMany("LOs")
                        .HasForeignKey("BIEN_LAI_CHI_TIETMaBienLaiChiTiet");

                    b.HasOne("WEB_520_Dynamic.Model.SAN_PHAM", "SAN_PHAM")
                        .WithMany()
                        .HasForeignKey("MaSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SAN_PHAM");
                });

            modelBuilder.Entity("WEB_520_Dynamic.Model.BIEN_LAI_CHI_TIET", b =>
                {
                    b.Navigation("LOs");
                });
#pragma warning restore 612, 618
        }
    }
}
