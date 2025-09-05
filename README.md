## Bookstore Web
## 1. GIỚI THIỆU
Ứng dụng Web mô phỏng hệ thống quản lý và bán sách trực tuyến. Dự án phát triển bằng ASP.NET Core MVC (C#) và cơ sở dữ liệu (CSDL) SQL Server

## 2. MAIN FUNCTION
- Quản lý người dùng: CRUD (xem, thêm, xóa, sửa), phân quyền admin/user
- Quản lý sách: CRUD, tìm kiếm sách theo nội dung, tác giả
- Giỏ hàng: Thêm, xóa, cập nhật số lượng
- Đặt hàng: Tạo đơn và lưu thông tin vào CSDL
- Quản trị (Admin): Quản lý danh mục, sách, người dùng, đơn hàng
- 
## 3. CÔNG NGHỆ
- Ngôn ngữ: C#, HTML, CSS, JavaScript
- Framework: ASP.NET Core MVC
- Cơ sở dữ liệu: SQL Server (file 'dulieuBookstoreDb.sql' đính kèm)
- Công cụ: Visual Studio, Entity Framework Core, Bootstrap
 
## 4. CẤU TRÚC
- Controllers: xử lý logic, request từ user
- Models: các lớp dữ liệu Book, User, Order...
- Views: giao diện (razor pages)
- Data và Migrations: quản lý database context và migrations
- wwwroot: file tĩnh (CSS, JS, hình)

## 5. CÀI ĐẶT+ CHẠY DỰ ÁN
1. Clone project từ GitHub
2. Mở project bằng Visual Studio
3. Import file dulieuBookstoreDb.sql vào SQL Server để khởi tạo database
4. Cập nhật chuội kết nối trong file appsettings.json
5. Chạy dự án bằng dotnet run
6. Truy cập http://localhost:5000 để sử dụng

## 6. HÌNH MINH HỌA
