# bookstore-web-aspnet-core

## 1. Introduction
This project is a **web application simulating an online bookstore management and sales system**, developed using **ASP.NET Core MVC (C#)** and **SQL Server**.

The application supports both **customer-facing features** (browsing books, shopping cart, ordering) and **administration features** (managing books, users, orders, and reports), aiming to practice web application development following the MVC pattern.

---

## 2. Main Features

### User Management
- CRUD operations for users (view, create, update, delete)
- Role-based authorization (Admin / User)

### Book Management
- CRUD operations for books
- Search books by content and author

### Shopping Cart
- Add books to cart
- Update quantities
- Remove items from cart

### Order Management
- Create orders
- Store order information in the database

### Admin Panel
- Manage categories, books, users, and orders
- View overall system statistics and reports

---

## 3. Technologies Used
- **Programming Languages:** C#, HTML, CSS, JavaScript
- **Framework:** ASP.NET Core MVC
- **Database:** SQL Server  
  (Database script included: `dulieuBookstoreDb.sql`)
- **Tools & Libraries:** Visual Studio, Entity Framework Core, Bootstrap

---

## 4. Project Structure
- **Controllers**
  - Handle user requests and application logic

- **Models**
  - Define data entities such as Book, User, Order, etc.

- **Views**
  - User interface built with Razor Pages

- **Data & Migrations**
  - Manage database context and Entity Framework migrations

- **wwwroot**
  - Static files (CSS, JavaScript, images)

---

## 5. Installation & Run Guide
1. Clone the project from GitHub
2. Open the project using **Visual Studio**
3. Import `dulieuBookstoreDb.sql` into **SQL Server** to initialize the database
4. Update the database connection string in `appsettings.json`
5. Run the project using:
   ```bash
   dotnet run
 6. Access the application at http://localhost:5001

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

