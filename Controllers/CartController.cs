using BookstoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using BookstoreWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Globalization;

namespace BookstoreWeb.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly BookstoreContext _context;

        public CartController(BookstoreContext context)
        {
            _context = context;
        }

        //QUẢN LÝ GIỎ HÀNG
        //add to cart
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = GetCurrentUserId();

            // Tìm Order hiện tại với trạng thái "New" hoặc tạo Order mới nếu không tồn tại
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.UserID == userId && o.Status == "New");

            if (order == null)
            {
                order = new Order
                {
                    UserID = userId,
                    OrderDate = DateTime.Now,
                    Status = "New",
                    TotalAmount = 0
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }

            // Kiểm tra xem sản phẩm đã tồn tại trong OrderDetails của Order "New" hiện tại chưa
            var orderDetail = order.OrderDetails.FirstOrDefault(od => od.ProductID == productId);

            if (orderDetail == null)
            {
                // Nếu chưa tồn tại trong Order "New", thêm sản phẩm mới vào
                var product = _context.Products.Find(productId);
                if (product == null) throw new Exception("Product not found.");

                orderDetail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductID = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                };
                _context.OrderDetails.Add(orderDetail);
            }
            else
            {
                // Nếu đã tồn tại trong Order "New", cập nhật số lượng
                orderDetail.Quantity += quantity;
            }

            // Cập nhật TotalAmount cho Order
            order.TotalAmount = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice);

            _context.SaveChanges();

            // Điều hướng về trang ViewCart
            return RedirectToAction("ViewCart");
        }


        //xem Cart
        public IActionResult ViewCart()
        {
            ViewBag.Categories = _context.Categories.ToList();
            var userId = GetCurrentUserId();
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.UserID == userId && o.Status == "New");

            if (order == null)
            {
                return View("EmptyCart");
            }
            return View(order);
        }


        //xóa cart
        public IActionResult RemoveFromCart(int orderDetailId)
        {
            var orderDetail = _context.OrderDetails.Find(orderDetailId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);

                var order = _context.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefault(o => o.OrderID == orderDetail.OrderID);

                if (order != null)
                {
                    order.TotalAmount = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice);
                    var formattedTotal = string.Format(new CultureInfo("vi-VN"), "{0:C0}", order.TotalAmount);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("ViewCart");
        }

        //sửa số lượng sp
        public IActionResult UpdateQuantity(int orderDetailId, int quantity)
        {
            var orderDetail = _context.OrderDetails.Find(orderDetailId);
            if (orderDetail != null)
            {
                orderDetail.Quantity = quantity;

                var order = _context.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefault(o => o.OrderID == orderDetail.OrderID);

                if (order != null)
                {
                    order.TotalAmount = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice);
                    var formattedTotal = string.Format(new CultureInfo("vi-VN"), "{0:C0}", order.TotalAmount);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("ViewCart");
        }

        //checkout/ mua hàng
        [HttpPost]
        public IActionResult Checkout()
        {
            var userId=GetCurrentUserId();
            var order= _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.UserID == userId && o.Status == "New");

            if (order == null || !order.OrderDetails.Any())
            {
                return RedirectToAction("ViewCart");
            }

            order.Status = "Checked Out";
            order.OrderDate= DateTime.Now;

            _context.SaveChanges();
            return View("Checkout", order);
        }



        private string GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userIdClaim))
            {
                return userIdClaim;
            }
            throw new Exception("User ID is not valid.");
        }


        //xem Claims
        public IActionResult ClaimsTest()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Json(claims);
        }
    }
}
