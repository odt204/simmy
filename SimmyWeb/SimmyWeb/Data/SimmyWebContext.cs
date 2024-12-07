using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SimmyWeb.Data;

public partial class SimmyWebContext : DbContext
{
    public SimmyWebContext()
    {
    }

    public SimmyWebContext(DbContextOptions<SimmyWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaoCaoNguoiDung> BaoCaoNguoiDungs { get; set; }

    public virtual DbSet<BinhLuan> BinhLuans { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<DanhGiaSanPham> DanhGiaSanPhams { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<LichSuGiaSanPham> LichSuGiaSanPhams { get; set; }

    public virtual DbSet<LichSuGiamGiaSanPham> LichSuGiamGiaSanPhams { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<PhanHoiBaoCao> PhanHoiBaoCaos { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<SanPhamYeuThich> SanPhamYeuThiches { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public virtual DbSet<Thue> Thues { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=desktop-nfcrpr5;Initial Catalog=SimmyWEB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaoCaoNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaBaoCao).HasName("PK__BaoCaoNg__25A9188C51A4559D");

            entity.ToTable("BaoCaoNguoiDung");

            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasDefaultValue(0);

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.BaoCaoNguoiDungs)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaoCaoNgu__MaNgu__22751F6C");
        });

        modelBuilder.Entity<BinhLuan>(entity =>
        {
            entity.HasKey(e => e.MaBinhLuan).HasName("PK__BinhLuan__87CB66A01A487AD3");

            entity.ToTable("BinhLuan");

            entity.Property(e => e.NgayBinhLuan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BinhLuan__MaNguo__787EE5A0");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BinhLuan__MaSanP__778AC167");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietDonHang).HasName("PK__ChiTietD__4B0B45DD473A784E");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaDonHang)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDo__MaDon__73BA3083");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => new { e.MaGioHang, e.MaKichCo }).HasName("PK__ChiTietG__98E7737B44EE74B6");

            entity.ToTable("ChiTietGioHang");

            entity.HasOne(d => d.MaGioHangNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGioHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietGi__MaGio__01142BA1");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("PK__ChucVu__D46395333B5E343A");

            entity.ToTable("ChucVu");

            entity.Property(e => e.TenChucVu).HasMaxLength(30);
        });

        modelBuilder.Entity<DanhGiaSanPham>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia).HasName("PK__DanhGiaS__AA9515BF0E994BE9");

            entity.ToTable("DanhGiaSanPham");

            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.DanhGiaSanPhams)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGiaSa__MaNgu__114A936A");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.DanhGiaSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGiaSa__MaSan__10566F31");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B3750887D06B9E5E");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.TenDanhMuc).HasMaxLength(50);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DonHang__129584ADD9251DBE");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDonHang)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DiaChiNguoiNhan).HasMaxLength(100);
            entity.Property(e => e.HinhThucNhanHang)
                .HasMaxLength(50)
                .HasDefaultValue(" Ch? x? lý ");
            entity.Property(e => e.SdtnguoiNhan)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDTNguoiNhan");
            entity.Property(e => e.TenNguoiNhan).HasMaxLength(30);
            entity.Property(e => e.TongSl).HasColumnName("TongSL");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaNguoiGuiNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaNguoiGui)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonHang__MaNguoi__6D0D32F4");

            entity.HasOne(d => d.MaThueNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaThue)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonHang__MaThue__6E01572D");

            entity.HasOne(d => d.MaVoucherNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaVoucher)
                .HasConstraintName("FK__DonHang__MaVouch__6C190EBB");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGioHang).HasName("PK__GioHang__F5001DA3D4ACBD0F");

            entity.ToTable("GioHang");

            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasDefaultValue(0.0);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Chua Thanh Toan");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.GioHangs)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GioHang__MaNguoi__7E37BEF6");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13BB16E7EC6");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaDonHang)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaThanhToan)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NgayXuatHoaDon)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaDonHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaDonHan__09A971A2");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaNguoiD__0A9D95DB");

            entity.HasOne(d => d.MaThanhToanNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaThanhToan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaThanhT__0B91BA14");
        });

        modelBuilder.Entity<LichSuGiaSanPham>(entity =>
        {
            entity.HasKey(e => e.MaLichSu).HasName("PK__LichSuGi__C443222A0C5EAC34");

            entity.ToTable("LichSuGiaSanPham");

            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.LichSuGiaSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichSuGia__MaSan__19DFD96B");
        });

        modelBuilder.Entity<LichSuGiamGiaSanPham>(entity =>
        {
            entity.HasKey(e => e.MaLichSuGiamGia).HasName("PK__LichSuGi__35BD950F8377AFFF");

            entity.ToTable("LichSuGiamGiaSanPham");

            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.LichSuGiamGiaSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichSuGia__MaSan__1DB06A4F");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__NguoiDun__C539D76200166609");

            entity.ToTable("NguoiDung");

            entity.Property(e => e.AnhDaiDien).IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.TenNguoiDung).HasMaxLength(50);

            entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.MaChucVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NguoiDung__MaChu__4F7CD00D");

            entity.HasOne(d => d.MaTaiKhoanNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.MaTaiKhoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NguoiDung__MaTai__4E88ABD4");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNhaCungCap).HasName("PK__NhaCungC__53DA920576F4B819");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.TenNhaCungCap).HasMaxLength(100);
        });

        modelBuilder.Entity<PhanHoiBaoCao>(entity =>
        {
            entity.HasKey(e => e.MaPhanHoi).HasName("PK__PhanHoiB__3458D20F9A9DF22E");

            entity.ToTable("PhanHoiBaoCao");

            entity.Property(e => e.NgayPhanHoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaBaoCaoNavigation).WithMany(p => p.PhanHoiBaoCaos)
                .HasForeignKey(d => d.MaBaoCao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhanHoiBa__MaBao__2645B050");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.PhanHoiBaoCaos)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhanHoiBa__MaNgu__2739D489");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__SanPham__FAC7442D98790FCC");

            entity.ToTable("SanPham");

            entity.Property(e => e.AnhSp)
                .IsUnicode(false)
                .HasColumnName("AnhSP");
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TenSanPham).HasMaxLength(50);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SanPham__MaDanhM__6383C8BA");

            entity.HasOne(d => d.MaNhaCungCapNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNhaCungCap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SanPham__MaNhaCu__6477ECF3");
        });

        modelBuilder.Entity<SanPhamYeuThich>(entity =>
        {
            entity.HasKey(e => e.MaYeuThich).HasName("PK__SanPhamY__B9007E4C2A790BA0");

            entity.ToTable("SanPhamYeuThich");

            entity.Property(e => e.NgayThem)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.SanPhamYeuThiches)
                .HasForeignKey(d => d.MaNguoiDung)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SanPhamYe__MaNgu__160F4887");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.SanPhamYeuThiches)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SanPhamYe__MaSan__151B244E");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C65294D5A62DC");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaThanhToan).HasName("PK__ThanhToa__D4B258447A7FE177");

            entity.ToTable("ThanhToan");

            entity.Property(e => e.MaThanhToan)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.MaDonHang)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NgayThanhToan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(50);

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.MaDonHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ThanhToan__MaDon__05D8E0BE");
        });

        modelBuilder.Entity<Thue>(entity =>
        {
            entity.HasKey(e => e.MaThue).HasName("PK__Thue__9CC2FDA3CDAE3F6E");

            entity.ToTable("Thue");

            entity.Property(e => e.TenThue).HasMaxLength(50);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.MaVoucher).HasName("PK__VOUCHER__0AAC5B110FC55A37");

            entity.ToTable("VOUCHER");

            entity.Property(e => e.DieuKienApDung).HasDefaultValue(0);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoLuong).HasDefaultValue(1);
            entity.Property(e => e.ThoiGianBatDau)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThuc)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
