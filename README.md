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
6. Truy cập http://localhost:5001 để sử dụng

## 6. HÌNH MINH HỌA
# Phần Khách hàng
Trang chủ
<img width="1918" height="928" alt="image" src="https://github.com/user-attachments/assets/49be3e57-22b8-4060-b718-2fc5659a55fc" />

Chi tiết sản phẩm (sách)
<img width="1906" height="562" alt="image" src="https://github.com/user-attachments/assets/17f9286c-82dc-4778-bdba-dda67e3c14c5" />

Giỏ hàng
<img width="1919" height="425" alt="image" src="https://github.com/user-attachments/assets/34502d05-29de-43c1-9c6f-084095699bf4" />

# Phần chung
Đăng nhập
<img width="1919" height="271" alt="image" src="https://github.com/user-attachments/assets/820f9e5a-68ac-4667-8898-6a7b42f24ff6" />

Đăng ký
<img width="1919" height="311" alt="image" src="https://github.com/user-attachments/assets/fc7ba86c-ff4f-4242-8dff-74889fa71deb" />

# Phần Admin
Dashboard ( báo cáo tổng)
<img width="1919" height="420" alt="image" src="https://github.com/user-attachments/assets/e57f5d12-6963-4c95-b6b9-86725167cfb0" />

Quản lý sản phẩm
<img width="1919" height="700" alt="image" src="https://github.com/user-attachments/assets/3d602230-1cda-409d-8ab6-d617fb8f0953" />

Quản lý đơn hàng
<img width="1919" height="768" alt="image" src="https://github.com/user-attachments/assets/17c5e0bd-84c1-4a81-9afb-5872e9742ba2" />

Xem báo cáo doanh thu
<img width="1919" height="708" alt="image" src="https://github.com/user-attachments/assets/9152d49b-c5c4-493c-b82d-29e0d7cb2c2f" />

