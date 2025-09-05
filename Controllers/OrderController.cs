using Microsoft.AspNetCore.Mvc;
using BookstoreWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookstoreWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly BookstoreContext _context;
        public OrderController(BookstoreContext context)
        {
            _context = context;
        }

        // XỬ LÝ ĐƠN HÀNG
        //xem trạng thái đơn
        public IActionResult ViewOrderStatus()
        {
            ViewBag.Categories = _context.Categories.ToList();
            var userId = GetCurrentUserId();
            var orders = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserID == userId && o.Status != "New")
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View("OrderStatus", orders);
        }

        // Hủy đơn hàng
        [HttpPost]
        public IActionResult CancelOrder(int orderId)
        {
            var userId = GetCurrentUserId();
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderID == orderId && o.UserID == userId);

            if (order == null || order.Status== "Cancelled" || order.Status == "Completed")
            {
                return RedirectToAction("ViewOrderStatus");
            }

            order.Status = "Cancelled";
            _context.SaveChanges();

            return RedirectToAction("ViewOrderStatus");

        }

        //confirm order( nhập thông tin khi checkout)
        [HttpPost]
        public IActionResult ConfirmOrder(string FullName, string Email, string Phone, string Address, string? Note, string PaymentMethod)
        {
            var userId = GetCurrentUserId();
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude (od => od.Product)
                .FirstOrDefault(o => o.UserID == userId && o.Status == "Checked Out");

            if (order == null)
            {
                return RedirectToAction("ViewCart","Cart");
            }

            order.FullName = FullName;
            order.Email = Email;
            order.Phone = Phone;
            order.Address = Address;
            order.Note = string.IsNullOrEmpty(Note) ? "No note provided" : Note;
            order.PaymentMethod = PaymentMethod;
            order.Status = "Confirmed";
            order.OrderDate = DateTime.Now;

            _context.SaveChanges();

            return View("OrderSuccess", order);


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
    }
}
