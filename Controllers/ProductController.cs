using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookstoreWeb.Data;
using BookstoreWeb.Models;
using X.PagedList;
using System.Linq;
using X.PagedList.Extensions;

namespace BookstoreWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly BookstoreContext _context;

        public ProductController(BookstoreContext context)
        {
            _context = context;
        }

        // Xem danh sách sản phẩm
        public IActionResult Index(string sortOrder, string searchString, string category, int page = 1)
        {
            ViewBag.Categories = _context.Categories.ToList();
            var products = from p in _context.Products
                           .Include(p => p.Category)
                           .Include(p => p.ProductImages.Where(pi => pi.IsPrimary==true && pi.ImageType=="main"))
                           select p;

            // Tìm kiếm sản phẩm theo tên
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            // Lọc theo thể loại sản phẩm
            if (!string.IsNullOrEmpty(category) && int.TryParse(category, out int categoryId))
            {
                products = products.Where(p => p.CategoryID==categoryId);
            }

            // Sắp xếp sản phẩm theo giá
            switch (sortOrder)
            {
                case "price_asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            // Phân trang
            int pageSize = 12;
            var paginatedProducts = products.ToPagedList(page, pageSize);

            // Gửi dữ liệu sang view
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SearchString = searchString;
            ViewBag.Category = category;
            ViewBag.Categories = _context.Categories.ToList();

            return View(paginatedProducts);
        }

        // Xem chi tiết sản phẩm
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            var product = await _context.Products
                        .Include(p => p.Category)
                        .Include(p => p.ProductImages)
                        .FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
