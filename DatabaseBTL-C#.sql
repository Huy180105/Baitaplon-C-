USE [Baitaplon-C#]
GO

-- 1. Bảng Tài Khoản
CREATE TABLE TaiKhoan 
(
    Tendangnhap nvarchar(50) primary key,
    Matkhau nvarchar(50) not null,
    Hoten nvarchar(50) not null
);

-- 2. Bảng Loại Phòng
CREATE TABLE LoaiPhong
(
    Maloaiphong INT PRIMARY KEY IDENTITY(1,1),
    Tenloai NVARCHAR(50) NOT NULL,
    Dongia DECIMAL(18,0) NOT NULL,
    CONSTRAINT CK_LoaiPhong_Dongia CHECK (Dongia >= 0)
);

-- 3. Bảng Dịch Vụ
CREATE TABLE Dichvu
(
    MaDV nvarchar(50) NOT NULL PRIMARY KEY, -- Đã thêm PRIMARY KEY
    TenDV nvarchar(50) not null,
    Donvitinh nvarchar(50),
    Dongia decimal(18,0) not null check (Dongia >= 0)
);

-- 4. Bảng Khách Thuê
CREATE TABLE KhachThue
(
    Makhach int primary key identity(1,1),
    Hoten nvarchar(100) not null,
    CCCD varchar(20) unique,
    SDT varchar(15),
    Gioitinh nvarchar(10), -- Đã thêm Cột Giới Tính
    Ngaysinh date,
    Quequan nvarchar(100)
);

-- 5. Bảng Phòng Trọ (Tạo bảng trước, thêm khóa ngoại sau để tránh lỗi vòng tròn)
CREATE TABLE Phongtro
(
    Maphong varchar(10) primary key,
    Tenphong nvarchar(50) not null,
    Dientich int,
    Trangthai nvarchar(20) not null default N'Trống',
    Maloaiphong int,
    Makhach int, -- Đã thêm cột Mã Khách
    Mats int,    -- Đã thêm cột Mã Tài Sản
    foreign key (Maloaiphong) references LoaiPhong(Maloaiphong),
    foreign key (Makhach) references KhachThue(Makhach)
);

-- 6. Bảng Tài Sản
CREATE TABLE TaiSan
(
    Mats int primary key identity(1,1),
    Tents nvarchar(100) not null,
    Soluong int not null check (Soluong >= 0),
    Tinhtrang nvarchar(50),
    Maphong varchar(10),
    foreign key (Maphong) references Phongtro(Maphong) on delete set null
);

-- 7. Bảng Hợp Đồng
CREATE TABLE HopDong
(
    MaHD int primary key identity(1,1),
    Maphong varchar(10) not null,
    Makhach int not null,
    Ngaylap date not null,
    Ngaybatdau date not null,
    Ngayketthuc date,
    Tiencoc decimal(18,0) not null check (Tiencoc >= 0),
    Giathuethan decimal(18,0) not null,
    Trangthai nvarchar(20) not null default N'Hiệu lực',
    foreign key(Maphong) references PhongTro(Maphong),
    foreign key(Makhach) references KhachThue(Makhach)
);

-- 8. Bảng Chỉ Số Điện Nước
CREATE TABLE ChiSoDienNuoc
(
    ID int primary key identity(1,1),
    Maphong varchar(10) not null,
    Thang int not null check (Thang between 1 and 12),
    Nam int not null,
    Chisodiencu int not null check (Chisodiencu >= 0),
    Chisodienmoi int not null,
    Chisonuoccu int not null check(Chisonuoccu >= 0),
    Chisonuocmoi int not null,
    unique(Maphong, Thang, Nam),
    foreign key(Maphong) references PhongTro(Maphong),
    CONSTRAINT CK_ChiSoDien_HopLe CHECK (Chisodienmoi >= Chisodiencu),
    CONSTRAINT CK_ChiSoNuoc_HopLe CHECK (Chisonuocmoi >= Chisonuoccu)
);

-- 9. Bảng Hóa Đơn
CREATE TABLE HoaDon
(
    MaHD int primary key identity(1,1),
    MaHopdong int,
    Thang int not null check (Thang between 1 and 12),
    Nam int not null,
    Ngaylap date default getdate(),
    Tongtien decimal(18,0) not null check (Tongtien >= 0 ),
    Trangthai nvarchar(20) not null default N'Chưa trả',
    foreign key (MaHopdong) references HopDong(MaHD)
);

-- 10. Bảng Chi Tiết Hóa Đơn
CREATE TABLE ChiTietHoaDon
(
    MaCT int primary key identity(1,1),
    MaHoaDon int not null,
    MaDV nvarchar(50), -- Đã thêm cột Mã Dịch Vụ
    Tenkhoanmuc nvarchar(100) not null,
    Donvitinh nvarchar(20),
    Soluong decimal(10,2) not null,
    Dongia decimal(18,0) not null,  
    Thanhtien AS (CAST(Soluong * Dongia AS decimal(18,0))), 
    foreign key (MaHoaDon) references HoaDon(MaHD),
    foreign key (MaDV) references Dichvu(MaDV) -- Đã thêm Khóa Ngoại Dịch Vụ
);

-- 11. CẬP NHẬT KHÓA NGOẠI CHO BẢNG PHÒNG TRỌ (Mats)
-- Phải làm bước này cuối cùng vì khi tạo bảng Phongtro, bảng TaiSan chưa tồn tại
ALTER TABLE Phongtro
ADD CONSTRAINT FK_Phongtro_TaiSan
FOREIGN KEY (Mats) REFERENCES TaiSan(Mats);