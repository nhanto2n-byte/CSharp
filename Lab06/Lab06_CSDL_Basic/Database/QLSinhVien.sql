/* Script minh họa tạo CSDL và dữ liệu mẫu. Chạy trong SSMS.
   Nếu đã có CSDL QLSinhVien chứa dữ liệu, hãy sao lưu trước khi dùng script này. */
IF DB_ID(N'QLSinhVien') IS NULL
    CREATE DATABASE QLSinhVien;
GO
USE QLSinhVien;
GO

IF OBJECT_ID('dbo.Diem','U') IS NOT NULL DROP TABLE dbo.Diem;
IF OBJECT_ID('dbo.SinhVien','U') IS NOT NULL DROP TABLE dbo.SinhVien;
IF OBJECT_ID('dbo.MonHoc','U') IS NOT NULL DROP TABLE dbo.MonHoc;
IF OBJECT_ID('dbo.Lop','U') IS NOT NULL DROP TABLE dbo.Lop;
IF OBJECT_ID('dbo.Khoa','U') IS NOT NULL DROP TABLE dbo.Khoa;
GO

CREATE TABLE dbo.Khoa(
    MaKhoa VARCHAR(20) NOT NULL PRIMARY KEY,
    TenKhoa NVARCHAR(100) NOT NULL
);
CREATE TABLE dbo.Lop(
    MaLop VARCHAR(20) NOT NULL PRIMARY KEY,
    TenLop NVARCHAR(100) NOT NULL,
    MaKhoa VARCHAR(20) NOT NULL,
    CONSTRAINT FK_Lop_Khoa FOREIGN KEY(MaKhoa) REFERENCES dbo.Khoa(MaKhoa)
);
CREATE TABLE dbo.MonHoc(
    MaMonHoc VARCHAR(20) NOT NULL PRIMARY KEY,
    TenMonHoc NVARCHAR(100) NOT NULL
);
CREATE TABLE dbo.SinhVien(
    MaSinhVien VARCHAR(20) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    MaLop VARCHAR(20) NOT NULL,
    CONSTRAINT FK_SinhVien_Lop FOREIGN KEY(MaLop) REFERENCES dbo.Lop(MaLop)
);
CREATE TABLE dbo.Diem(
    MaSinhVien VARCHAR(20) NOT NULL,
    MaMonHoc VARCHAR(20) NOT NULL,
    Diem DECIMAL(4,2) NOT NULL CHECK(Diem BETWEEN 0 AND 10),
    CONSTRAINT PK_Diem PRIMARY KEY(MaSinhVien, MaMonHoc),
    CONSTRAINT FK_Diem_SinhVien FOREIGN KEY(MaSinhVien) REFERENCES dbo.SinhVien(MaSinhVien),
    CONSTRAINT FK_Diem_MonHoc FOREIGN KEY(MaMonHoc) REFERENCES dbo.MonHoc(MaMonHoc)
);
GO

INSERT dbo.Khoa VALUES
('CNTT',N'Công nghệ thông tin'),('KT',N'Kế toán'),('QTKD',N'Quản trị kinh doanh');
INSERT dbo.Lop VALUES
('CNTT01',N'Công nghệ phần mềm 01','CNTT'),
('CNTT02',N'Hệ thống thông tin 02','CNTT'),
('KT01',N'Kế toán 01','KT');
INSERT dbo.MonHoc VALUES
('MH01',N'Cơ sở dữ liệu'),('MH02',N'Lập trình C#'),('MH03',N'Cấu trúc dữ liệu'),
('MH04',N'Mạng máy tính'),('MH05',N'Phân tích thiết kế hệ thống');
INSERT dbo.SinhVien VALUES
('SV001',N'Nguyễn An', '2003-05-12','CNTT01'),
('SV002',N'Trần Bình', '2002-11-03','CNTT01'),
('SV003',N'Lê Chi', '2004-01-21','CNTT02'),
('SV004',N'Phạm Dũng', '2001-08-15','KT01'),
('SV005',N'Võ Hà', '2003-12-30','CNTT02');
INSERT dbo.Diem VALUES
('SV001','MH01',8.50),('SV001','MH02',7.00),('SV002','MH01',6.50),
('SV003','MH03',9.00),('SV004','MH04',7.50);
GO
