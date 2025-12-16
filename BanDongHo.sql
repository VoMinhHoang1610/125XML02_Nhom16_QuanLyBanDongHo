USE master;
GO

-- Xoa database cu neu ton tai
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'BanDongHo')
BEGIN
    ALTER DATABASE BanDongHo SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BanDongHo;
END
GO

-- Tao database moi
CREATE DATABASE BanDongHo
GO 

USE BanDongHo
GO

-- =====================================================
-- 1. BANG NHAN VIEN
-- Luu thong tin nhan vien va tai khoan dang nhap
-- =====================================================
CREATE TABLE NhanVien(
	maNV varchar(7) PRIMARY KEY,           -- Ma nhan vien (VD: NV00001)
	tenNV nvarchar(50) NOT NULL,           -- Ten day du nhan vien
	tenDN varchar(25) UNIQUE NOT NULL,     -- Ten dang nhap (phai duy nhat)
	matKhau varchar(25) NOT NULL           -- Mat khau dang nhap
)

-- Them du lieu nhan vien mau
INSERT INTO NhanVien VALUES
('NV00001', N'Võ Minh Hoàng', 'hoang1', '12345'),
('NV00002', N'Trương Thị Kiều Nhi', 'nhi2', '12345'),
('NV00003', N'Lê Phương Linh', 'linh3', '12345'),
('NV00004', N'Đăng Khánh Trịnh', 'khanh4', '12345'),
('NV00005', N'Thanh Loan Vương', 'ThanhLoan70', '12345'),
('NV00006', N'Trường Phát Vương', 'phattruong02', '12345')

-- =====================================================
-- 2. BANG KHACH HANG
-- Luu thong tin khach hang va lich su mua hang
-- =====================================================
CREATE TABLE KhachHang(
	maKH varchar(7) PRIMARY KEY,           -- Ma khach hang (VD: KH00001)
	tenKH nvarchar(50) NOT NULL,           -- Ten khach hang
	SoTienDaDung int DEFAULT 0,            -- Tong tien da mua (tu dong cap nhat)
	namSinh int,                           -- Nam sinh
	diaChi nvarchar(50)                    -- Dia chi
)

-- Them du lieu khach hang mau
INSERT INTO KhachHang VALUES
('KH00000', N'Khách vãn lai', 0, 0, ''),                    -- Khach van lai mac dinh
('KH00001', N'Quốc Tuấn', 0, 2001, N'Thừa Thiên Huế'),
('KH00002', N'Văn Hoàng', 0, 1999, N'Đà Nẵng'),
('KH00003', N'Thu Hằng', 0, 1989, N'Kom Tum'),
('KH00004', N'Thu Hiền', 0, 2002, N'Đắk Lắk'),
('KH00005', N'Ngọc Diễm', 0, 2005, N'Đà Nẵng'),
('KH00006', N'Thế Hoàng', 0, 1992, N'Đà Nẵng'),
('KH00007', N'Huỳnh Trí', 0, 1970, N'Hà Tĩnh'),
('KH00008', N'Văn Duy', 0, 1990, N'Hải Phòng')

-- =====================================================
-- 3. BANG DANH MUC SAN PHAM
-- Phan loai cac dong ho theo danh muc
-- =====================================================
CREATE TABLE DanhMucSanPham(
	maDM varchar(7) PRIMARY KEY,           -- Ma danh muc (VD: DM00001)
	tenDM nvarchar(50) NOT NULL            -- Ten danh muc
)

-- Them cac danh muc dong ho
INSERT INTO DanhMucSanPham VALUES
('DM00001', N'Đồng hồ điện tử'),         -- Dong ho dien tu (Casio, G-Shock...)
('DM00002', N'Đồng hồ kim'),             -- Dong ho kim truyen thong (Seiko, Rolex...)
('DM00003', N'Đồng hồ thông minh')       -- Dong ho thong minh (Apple Watch, Huawei...)

-- =====================================================
-- 4. BANG CHI TIET SAN PHAM
-- Luu thong tin chi tiet tung san pham dong ho
-- =====================================================
CREATE TABLE ChiTietSanPham(
	maSP varchar(7) PRIMARY KEY,           -- Ma san pham (VD: SP00001)
	tenSP nvarchar(50) NOT NULL,           -- Ten san pham
	soLuong int NOT NULL DEFAULT 0,        -- So luong ton kho
	chiTiet nvarchar(50),                  -- Mo ta chi tiet
	maDMSP varchar(7) NOT NULL,            -- Ma danh muc (lien ket voi DanhMucSanPham)
	giaNhap int NOT NULL,                  -- Gia nhap vao
	gia int NOT NULL,                      -- Gia ban ra
	
	-- Rang buoc du lieu
	CONSTRAINT CK_SoLuong CHECK (soLuong >= 0),           -- So luong khong am
	CONSTRAINT CK_GiaNhap CHECK (giaNhap > 0),            -- Gia nhap duong
	CONSTRAINT CK_GiaBan CHECK (gia > giaNhap),           -- Gia ban > gia nhap
	CONSTRAINT FK_DanhMuc FOREIGN KEY (maDMSP) REFERENCES DanhMucSanPham(maDM)
)

-- Them du lieu san pham mau
INSERT INTO ChiTietSanPham VALUES
-- Dong ho dien tu
('SP00001', 'Casio E300', 23, N'Kiểu dáng đẹp', 'DM00001', 250000, 300000),
('SP00002', 'Casio N1001', 23, N'Thiết kế mới, sang trọng', 'DM00001', 450000, 550000),
('SP00003', 'Casio N900', 23, N'Thiết kế mới, sang trọng', 'DM00001', 380000, 480000),
('SP00004', 'G-Shock 107', 23, N'Đảm bảo chính hãng', 'DM00001', 950000, 1122000),

-- Dong ho kim
('SP00005', 'Seiko A383', 23, N'Chất lượng đi cùng năm tháng', 'DM00002', 1050000, 1230000),
('SP00006', 'Seiko A387', 23, N'Đồng hồ cao cấp', 'DM00002', 1200000, 1400000),
('SP00007', 'Hublot Super', 23, N'Đảm bảo chính hãng', 'DM00002', 3800000, 4500000),
('SP00008', 'Rolex Luxury', 23, N'Đảm bảo chính hãng', 'DM00002', 2500000, 3000000),

-- Dong ho thong minh
('SP00009', 'Apple Watch 4', 23, N'Tích hợp nhiều công nghệ hiện đại', 'DM00003', 3400000, 4000000),
('SP00010', 'Huawei Watch 11', 23, N'Hiện đại, sang trọng, tinh tế', 'DM00003', 1250000, 1500000),
('SP00011', 'Mi band 6', 23, N'Đẹp, sang trọng', 'DM00003', 620000, 746000)

-- =====================================================
-- 5. BANG HOA DON NHAP XUAT
-- Luu thong tin cac hoa don nhap hang va ban hang
-- =====================================================
CREATE TABLE HoaDonNhapXuat(
	maHD varchar(7) PRIMARY KEY,           -- Ma hoa don (VD: HD00001)
	maNV varchar(7) NOT NULL,              -- Ma nhan vien lap hoa don
	maKH varchar(7) NOT NULL,              -- Ma khach hang
	LoaiHD varchar(1) NOT NULL,            -- Loai hoa don: 'N'=Nhap, 'X'=Xuat
	ngayTao datetime DEFAULT GETDATE(),    -- Ngay tao hoa don (tu dong)
	
	-- Rang buoc du lieu
	CONSTRAINT CK_LoaiHD CHECK (LoaiHD IN ('N', 'X')),
	CONSTRAINT FK_NhanVien FOREIGN KEY (maNV) REFERENCES NhanVien(maNV),
	CONSTRAINT FK_KhachHang FOREIGN KEY (maKH) REFERENCES KhachHang(maKH)
)

-- Them du lieu hoa don mau
INSERT INTO HoaDonNhapXuat (maHD, maNV, maKH, LoaiHD) VALUES
('HD00001', 'NV00001', 'KH00001', 'N'),  -- Hoa don nhap hang
('HD00002', 'NV00002', 'KH00000', 'N'),  -- Hoa don nhap hang
('HD00003', 'NV00002', 'KH00001', 'X')   -- Hoa don ban hang

-- =====================================================
-- 6. BANG CHI TIET HOA DON
-- Luu chi tiet san pham trong moi hoa don
-- =====================================================
CREATE TABLE ChiTietHoaDon(
	maHD varchar(7) NOT NULL,              -- Ma hoa don
	maSP varchar(7) NOT NULL,              -- Ma san pham
	soLuong int NOT NULL,                  -- So luong mua/ban
	DonGia int NOT NULL,                   -- Don gia tai thoi diem mua/ban
	
	-- Khoa chinh ket hop
	PRIMARY KEY (maHD, maSP),
	
	-- Rang buoc du lieu
	CONSTRAINT CK_SoLuongHD CHECK (soLuong > 0),
	CONSTRAINT CK_DonGia CHECK (DonGia > 0),
	CONSTRAINT FK_HoaDon FOREIGN KEY (maHD) REFERENCES HoaDonNhapXuat(maHD),
	CONSTRAINT FK_SanPham FOREIGN KEY (maSP) REFERENCES ChiTietSanPham(maSP)
)

-- Them du lieu chi tiet hoa don mau
INSERT INTO ChiTietHoaDon VALUES
('HD00001', 'SP00001', 1, 300000),       -- HD00001: mua 1 Casio E300
('HD00002', 'SP00001', 18, 210000),      -- HD00002: mua 18 Casio E300
('HD00003', 'SP00004', 3, 1122000),      -- HD00003: ban 3 G-Shock 107
('HD00003', 'SP00001', 2, 300000)        -- HD00003: ban 2 Casio E300

-- =====================================================
-- 7. TRUY VAN KIEM TRA DU LIEU
-- Hien thi so luong ban ghi trong moi bang
-- =====================================================
SELECT 'NhanVien' as TenBang, COUNT(*) as SoLuong FROM NhanVien
UNION ALL
SELECT 'KhachHang', COUNT(*) FROM KhachHang
UNION ALL
SELECT 'DanhMucSanPham', COUNT(*) FROM DanhMucSanPham
UNION ALL
SELECT 'ChiTietSanPham', COUNT(*) FROM ChiTietSanPham
UNION ALL
SELECT 'HoaDonNhapXuat', COUNT(*) FROM HoaDonNhapXuat
UNION ALL
SELECT 'ChiTietHoaDon', COUNT(*) FROM ChiTietHoaDon

-- =====================================================
-- 8. TRUY VAN XEM DU LIEU MAU
-- =====================================================
PRINT N'=== DANH SACH NHAN VIEN ==='
SELECT * FROM NhanVien

PRINT N'=== DANH SACH SAN PHAM ==='
SELECT *
FROM ChiTietSanPham 

PRINT N'=== DANH SACH HOA DON ==='
SELECT *
FROM HoaDonNhapXuat