create database giaothong

create table USERS
(
	ID int primary key identity(1,1),
	Role_ID int,
	UserName char(30),
	Password char(20),
	Name nvarchar(30),
	Phone char(15),
	Email char(25),
	Address nvarchar(60),
	BirthDay Date,
)

Create table USERS_ROLE
(
	ID int primary key identity(1,1),
	name nvarchar(30)
)

alter table users add constraint fk_user_role foreign key(Role_ID) references USERS_ROLE(id) 

Create table GIAOVIEN
(
	MaGV char(20) primary key,
	HoDem nvarchar(25),
	TenGV nvarchar(15),
	NgaySinh Date,
	SoCCCD Char(12) null,
	NoiCT nvarchar(30),
	GioiTinh int,
	Phone Char(15),
	TuyenDung nvarchar(10),
	TrinhDo_VH nvarchar(10),
	Nganh_CM nvarchar(20),
	TrinhDo_SP nvarchar(10),
	GV_LT Bit,
	GV_TH Bit,
	SoGCN char(15),
	MaSoGTVT char(5),
	MaCSDT char(5),
	TrangThai Bit,
	AnhCD Text,
	NguoiCapNhat nvarchar(20),
	NgayCapNhat Date,
	NguoiDuyet nvarchar(20),
	NgayDuyet Date
)


ALTER TABLE GIAOVIEN
ADD TrinhDo_CM char(10);

create table GIAOVIEN_GCN
(
	SoGCN char(15) primary key,
	QDCap char(15),
	DonViCap char(5),
	NgayCap date,
	HangXe char(5),
	MaDonViTapHuan char(5),
	NgayKiemTra date,
	AnhGCN text,
	NguoiCapNhat nvarchar(15),
	NgayCapNhat date
)

alter table GIAOVIEN add constraint fk_giaovien_gcn foreign key(SoGCN) references GIAOVIEN_GCN(SoGCN) on delete cascade
ALTER TABLE GIAOVIEN_GCN ADD MaDVKiemTra nvarchar(5);

create table XETAPLAI	
(
	SoDangKy char(10) primary key,
	TenChuSoHuu char(30),
	SoGPTL char(15),
	NhanHieu char(15),
	KieuLoai char(20),
	HangXe char(5),
	MauXe nvarchar(20),
	SoDongCo char(20),
	SoKhung char(20),
	NamSX char(10),
	LoaiXe nvarchar(15),
	XeSatHach bit,
	TrangThaiHD bit,
	NgayCapNhat date,
	NguoiCapNhat nvarchar(15)
)

create table XETAPLAI_GP
(
	SoGPXTL char(15) primary key,
	SoDangKy char(10),
	MaDonViCap char(5),
	MaCSDT char(5),
	XeHopDong bit,
	NgayCap date,
	NgayHH date,
	AnhGP text,
	AnhDKiem text,
	AnhDky text,
	NgayCapNhat date,
	NguoiCapNhat nvarchar(15),
	DuongTapLai char(200)
)
alter table XETAPLAI add constraint fk_xetl_gp foreign key(SoGPTL) references XETAPLAI_GP(SoGPXTL) on delete cascade

CREATE TABLE province_city (
  matp varchar(5) primary key,
  name varchar(100)NOT NULL,
  type varchar(30) NOT NULL,
  slug varchar(30) DEFAULT NULL
)
ALTER TABLE province_city
ALTER column name nvarchar(100);

CREATE TABLE HangXe
(
	mahx int identity(1,1),
	ten varchar(5)
)

INSERT INTO HangXe(ten) VALUES
('A1'),
('A2'),
('A3'),
('A4'),
('B1'),
('B2'),
('C'),
('D'),
('E'),
('FB2'),
('FC'),
('FD'),
('FE');

INSERT INTO province_city(
matp, name, type, slug) VALUES
('01', 'Thành phố Hà Nội', 'Thành phố Trung ương', 'HANOI'),
('02', 'Tỉnh Hà Giang', 'Tỉnh', 'HAGIANG'),
('04', 'Tỉnh Cao Bằng', 'Tỉnh', 'CAOBANG'),
('06', 'Tỉnh Bắc Kạn', 'Tỉnh', 'BACKAN'),
('08', 'Tỉnh Tuyên Quang', 'Tỉnh', 'TUYENQUANG'),
('10', 'Tỉnh Lào Cai', 'Tỉnh', 'LAOCAI'),
('11', 'Tỉnh Điện Biên', 'Tỉnh', 'DIENBIEN'),
('12', 'Tỉnh Lai Châu', 'Tỉnh', 'LAICHAU'),
('14', 'Tỉnh Sơn La', 'Tỉnh', 'SONLA'),
('15', 'Tỉnh Yên Bái', 'Tỉnh', 'YENBAI'),
('17', 'Tỉnh Hoà Bình', 'Tỉnh', 'HOABINH'),
('19', 'Tỉnh Thái Nguyên', 'Tỉnh', 'THAINGUYEN'),
('20', 'Tỉnh Lạng Sơn', 'Tỉnh', 'LANGSON'),
('22', 'Tỉnh Quảng Ninh', 'Tỉnh', 'QUANGNINH'),
('24', 'Tỉnh Bắc Giang', 'Tỉnh', 'BACGIANG'),
('25', 'Tỉnh Phú Thọ', 'Tỉnh', 'PHUTHO'),
('26', 'Tỉnh Vĩnh Phúc', 'Tỉnh', 'VINHPHUC'),
('27', 'Tỉnh Bắc Ninh', 'Tỉnh', 'BACNINH'),
('30', 'Tỉnh Hải Dương', 'Tỉnh', 'HAIDUONG'),
('31', 'Thành phố Hải Phòng', 'Thành phố Trung ương', 'HAIPHONG'),
('33', 'Tỉnh Hưng Yên', 'Tỉnh', 'HUNGYEN'),
('34', 'Tỉnh Thái Bình', 'Tỉnh', 'THAIBINH'),
('35', 'Tỉnh Hà Nam', 'Tỉnh', 'HANAM'),
('36', 'Tỉnh Nam Định', 'Tỉnh', 'NAMDINH'),
('37', 'Tỉnh Ninh Bình', 'Tỉnh', 'NINHBINH'),
('38', 'Tỉnh Thanh Hóa', 'Tỉnh', 'THANHHOA'),
('40', 'Tỉnh Nghệ An', 'Tỉnh', 'NGHEAN'),
('42', 'Tỉnh Hà Tĩnh', 'Tỉnh', 'HATINH'),
('44', 'Tỉnh Quảng Bình', 'Tỉnh', 'QUANGBINH'),
('45', 'Tỉnh Quảng Trị', 'Tỉnh', 'QUANGTRI'),
('46', 'Tỉnh Thừa Thiên Huế', 'Tỉnh', 'THUATHIENHUE'),
('48', 'Thành phố Đà Nẵng', 'Thành phố Trung ương', 'DANANG'),
('49', 'Tỉnh Quảng Nam', 'Tỉnh', 'QUANGNAM'),
('51', 'Tỉnh Quảng Ngãi', 'Tỉnh', 'QUANGNGAI'),
('52', 'Tỉnh Bình Định', 'Tỉnh', 'BINHDINH'),
('54', 'Tỉnh Phú Yên', 'Tỉnh', 'PHUYEN'),
('56', 'Tỉnh Khánh Hòa', 'Tỉnh', 'KHANHHOA'),
('58', 'Tỉnh Ninh Thuận', 'Tỉnh', 'NINHTHUAN'),
('60', 'Tỉnh Bình Thuận', 'Tỉnh', 'BINHTHUAN'),
('62', 'Tỉnh Kon Tum', 'Tỉnh', 'KONTUM'),
('64', 'Tỉnh Gia Lai', 'Tỉnh', 'GIALAI'),
('66', 'Tỉnh Đắk Lắk', 'Tỉnh', 'DAKLAK'),
('67', 'Tỉnh Đắk Nông', 'Tỉnh', 'DAKNONG'),
('68', 'Tỉnh Lâm Đồng', 'Tỉnh', 'LAMDONG'),
('70', 'Tỉnh Bình Phước', 'Tỉnh', 'BINHPHUOC'),
('72', 'Tỉnh Tây Ninh', 'Tỉnh', 'TAYNINH'),
('74', 'Tỉnh Bình Dương', 'Tỉnh', 'BINHDUONG'),
('75', 'Tỉnh Đồng Nai', 'Tỉnh', 'DONGNAI'),
('77', 'Tỉnh Bà Rịa - Vũng Tàu', 'Tỉnh', 'BARIAVUNGTAU'),
('79', 'Thành phố Hồ Chí Minh', 'Thành phố Trung ương', 'HOCHIMINH'),
('80', 'Tỉnh Long An', 'Tỉnh', 'LONGAN'),
('82', 'Tỉnh Tiền Giang', 'Tỉnh', 'TIENGIANG'),
('83', 'Tỉnh Bến Tre', 'Tỉnh', 'BENTRE'),
('84', 'Tỉnh Trà Vinh', 'Tỉnh', 'TRAVINH'),
('86', 'Tỉnh Vĩnh Long', 'Tỉnh', 'VINHLONG'),
('87', 'Tỉnh Đồng Tháp', 'Tỉnh', 'DONGTHAP'),
('89', 'Tỉnh An Giang', 'Tỉnh', 'ANGIANG'),
('91', 'Tỉnh Kiên Giang', 'Tỉnh', 'KIENGIANG'),
('92', 'Thành phố Cần Thơ', 'Thành phố Trung ương', 'CANTHO'),
('93', 'Tỉnh Hậu Giang', 'Tỉnh', 'HAUGIANG'),
('94', 'Tỉnh Sóc Trăng', 'Tỉnh', 'SOCTRANG'),
('95', 'Tỉnh Bạc Liêu', 'Tỉnh', 'BACLIEU'),
('96', 'Tỉnh Cà Mau', 'Tỉnh', 'CAMAU');