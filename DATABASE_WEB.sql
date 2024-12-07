use master
create database SimmyWEB
use SimmyWEB

CREATE TABLE TaiKhoan
(
  MaTaiKhoan INT IDENTITY (1, 1)  NOT NULL,
  Email VARCHAR(50) NOT NULL,
  MatKhau VARCHAR(15) NOT NULL,
  TrangThai bit not null default 0,
  PRIMARY KEY (MaTaiKhoan)
);
CREATE TABLE ChucVu(
MaChucVu int identity(1,1) not null primary key,
TenChucVu Nvarchar(30)
);
CREATE TABLE NguoiDung
(
  MaNguoiDung INT IDENTITY (1, 1) NOT NULL,
  TenNguoiDung NVARCHAR(50) NOT NULL,
  DiaChi NVARCHAR(MAX) NOT NULL,
  SDT char(11) NOT NULL,
  AnhDaiDien VARCHAR(MAX) NOT NULL,
  MaChucVu int not null,
  MaTaiKhoan INT NOT NULL,
  PRIMARY KEY (MaNguoiDung),
  FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan),
   FOREIGN KEY (MaChucVu) REFERENCES ChucVu(MaChucVu)
);
CREATE TABLE Thue
(
    MaThue INT IDENTITY(1,1) PRIMARY KEY,  
    TenThue NVARCHAR(50) NOT NULL,         
    GiaTri FLOAT NOT NULL,          
    MoTa NVARCHAR(MAX),                    
    TrangThai BIT DEFAULT 1                
);

CREATE TABLE DanhMuc
(
  MaDanhMuc INT IDENTITY (1, 1) NOT NULL,
  TenDanhMuc NVARCHAR(50) NOT NULL,
  MoTa NVARCHAR(MAX)
  PRIMARY KEY (MaDanhMuc)
);
CREATE TABLE NhaCungCap (
    MaNhaCungCap INT PRIMARY KEY IDENTITY(1,1),   
    TenNhaCungCap NVARCHAR(100) NOT NULL,        
    SDT Char(11) not null,                             
    DiaChi NVARCHAR(255),                         
	Email VARCHAR(50)
);
CREATE TABLE VOUCHER(
MaVoucher INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	TenVoucher nvarchar(max) not null,
GiaTri int not null,
ThoiGianBatDau datetime not null default getdate(),
ThoiGianKetThuc datetime not null default getdate(),
TrangThai bit not null default 0,
NgayTao datetime not null default getdate(),
DieuKienApDung int default 0,
SoLuong int not null default 1
);
CREATE TABLE SanPham
(
  MaSanPham INT IDENTITY (1, 1) NOT NULL,
  TenSanPham NVARCHAR(50) NOT NULL,
  GiaTienMoi INT NOT NULL default 0,
  GiaTienCu INT NOT NULL default 0,
  MoTa NVARCHAR(max) NOT NULL,
  AnhSP VARCHAR(MAX) NOT NULL,
  MaDanhMuc INT NOT NULL,
  NgayTao date not null default GetDate(),
  MaNhaCungCap int not null,
  PRIMARY KEY (MaSanPham),
  FOREIGN KEY (MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc),
  FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap)
);
CREATE TABLE DonHang
(
  MaDonHang Varchar(20) NOT NULL,
  NgayDatHang DATE NOT NULL,
  TrangThai NVARCHAR(50) NOT NULL,
  PhiVanChuyen FLOAT NOT NULL,
  TongTien FLOAT NOT NULL,
  MaNguoiGui int NOT NULL,
  SDTNguoiNhan char(11) NOT NULL,
  DiaChiNguoiNhan NVARCHAR(100) NOT NULL,
  TenNguoiNhan Nvarchar(30) not null,
  TongSL int not null default 0,
  TongSoTien int not null default 0,
  TienPhaiTra int not null default 0,
  MaVoucher int ,
  MaThue INT NOT NULL DEFAULT 0,
  HinhThucNhanHang NVARCHAR(50) not null DEFAULT ' Chờ xử lý ',
    FOREIGN KEY (MaVoucher) REFERENCES Voucher(MaVoucher),
  FOREIGN KEY (MaNguoiGui) REFERENCES NguoiDung(MaNguoiDung),
  FOREIGN KEY (MaThue) REFERENCES Thue(MaThue),
  PRIMARY KEY (MaDonHang)
);

CREATE TABLE ChiTietDonHang
(
MaChiTietDonHang int identity(1,1) not null primary key,
  MaDonHang Varchar(20)   NOT NULL,
  Soluong int not null default 0,
  DonGia int not null default 0,
  TongTien int not null default 0,
  FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
);

CREATE TABLE BinhLuan
(
  MaBinhLuan INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
  MaSanPham INT NOT NULL,
  MaNguoiDung INT NOT NULL,
  NoiDung NVARCHAR(MAX) NOT NULL,
  NgayBinhLuan DATETIME NOT NULL DEFAULT GETDATE(),
  FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham),
  FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);

CREATE TABLE GioHang
(
    MaGioHang INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(50) DEFAULT 'Chua Thanh Toan',
    TongTien FLOAT DEFAULT 0,
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
CREATE TABLE ChiTietGioHang
(
    MaGioHang INT NOT NULL,
    MaKichCo INT NOT NULL,
    SoLuong INT NOT NULL,
    GiaBan FLOAT NOT NULL,
    PRIMARY KEY (MaGioHang, MaKichCo),
    FOREIGN KEY (MaGioHang) REFERENCES GioHang(MaGioHang),
    
);
CREATE TABLE ThanhToan
(
    MaThanhToan Varchar(250) PRIMARY KEY, 
    MaDonHang Varchar(20) NOT NULL,                      
    PhuongThucThanhToan NVARCHAR(50) NOT NULL,   
	NgayThanhToan DATETIME DEFAULT GETDATE(),    
    TongTien FLOAT NOT NULL,                     
    TrangThaiThanhToan bit not null default 0, 
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang)     
);
CREATE TABLE HoaDon
(
    MaHoaDon varchar(20) PRIMARY KEY,      
    MaDonHang Varchar(20) NOT NULL,                      
    MaNguoiDung INT NOT NULL,                    
    NgayXuatHoaDon DATETIME DEFAULT GETDATE(),   
    TongTien FLOAT NOT NULL,                     
    MaThanhToan Varchar(250) NOT NULL,                   
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),   
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung), 
    FOREIGN KEY (MaThanhToan) REFERENCES ThanhToan(MaThanhToan)   
);

CREATE TABLE DanhGiaSanPham
(
    MaDanhGia INT IDENTITY(1, 1) PRIMARY KEY,  
    MaSanPham INT NOT NULL,                    
    MaNguoiDung INT NOT NULL,                  
    DiemDanhGia INT CHECK (DiemDanhGia >= 1 AND DiemDanhGia <= 5) NOT NULL,                     
    NgayDanhGia DATETIME DEFAULT GETDATE(),    
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham), 
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)  
);
CREATE TABLE SanPhamYeuThich
(
    MaYeuThich INT IDENTITY(1, 1) PRIMARY KEY,  
    MaSanPham INT NOT NULL,                     
    MaNguoiDung INT NOT NULL,                   
    NgayThem DATETIME DEFAULT GETDATE(),        
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham),  
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)  
);
CREATE TABLE LichSuGiaSanPham
(
    MaLichSu INT IDENTITY(1, 1) PRIMARY KEY,  
    MaSanPham INT NOT NULL,                   
    GiaCu INT NOT NULL,                       
    GiaMoi INT NOT NULL,                      
    NgayCapNhat DATETIME DEFAULT GETDATE(),   
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham) 
);
CREATE TABLE LichSuGiamGiaSanPham
(
    MaLichSuGiamGia INT IDENTITY(1, 1) PRIMARY KEY,  
    MaSanPham INT NOT NULL,                   
    GiaGiamCu INT NOT NULL,                       
    GiaGiamMoi INT NOT NULL,                      
    NgayCapNhat DATETIME DEFAULT GETDATE(),   
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham) 
);
CREATE TABLE BaoCaoNguoiDung (
  MaBaoCao INT IDENTITY(1,1) PRIMARY KEY,
  MaNguoiDung INT NOT NULL,
  PhanHoi NVARCHAR(MAX),
  NgayTao DATETIME DEFAULT GETDATE(),
  TrangThai INT DEFAULT 0, -- 0: chưa phản hồi, 1: đã phản hồi
  FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);

-- Feedback Response Table for user and admin feedback tracking
CREATE TABLE PhanHoiBaoCao (
  MaPhanHoi INT IDENTITY(1,1) PRIMARY KEY,
  MaBaoCao INT NOT NULL,
  MaNguoiDung INT NOT NULL,
  NoiDung NVARCHAR(MAX) NOT NULL,
  NgayPhanHoi DATETIME DEFAULT GETDATE(),
  NguoiTraLoi BIT NOT NULL, -- 0: user, 1: admin
  FOREIGN KEY (MaBaoCao) REFERENCES BaoCaoNguoiDung(MaBaoCao),
  FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
