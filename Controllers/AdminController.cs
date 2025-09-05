using BookstoreWeb.Data;
using BookstoreWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Reflection;


[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly BookstoreContext _context;

    public AdminController(BookstoreContext context)
    {
        _context = context;
    }

    //admin dashboard
    public IActionResult Dashboard()
    {
        ViewBag.TotalProducts = _context.Products.Count();

        ViewBag.NewOrders = _context.Orders
                            .Count(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == DateTime.Now.Date);

        ViewBag.TodayRevenue = _context.Orders
                            .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == DateTime.Now.Date && o.Status == "Completed")
                            .Sum(o => o.TotalAmount ?? 0);

        ViewBag.RecentOrders = _context.Orders
                            .OrderByDescending(o => o.OrderDate)
                            .Take(5)
                            .Select(o => new
                            {
                                o.OrderID,
                                o.TotalAmount,
                                OrderDate = o.OrderDate.HasValue ? o.OrderDate.Value.ToString("dd/MM/yyyy") : "N/A"
                            })
                            .ToList();

        return View();
    }


    //1. quản lý sản phẩm( xem, thêm, xóa, sửa) CRUD
    //xem
    public IActionResult ProductList()
    {
        var products= _context.Products.Include(p => p.Category).ToList();
        return View(products);
    }

    // Thêm
    [HttpGet]
    public IActionResult AddProduct()
    {
        var categories = _context.Categories
            .Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.Name
            })
            .ToList();

        if (categories == null || !categories.Any())
        {
            categories = new List<SelectListItem>();
        }

        ViewBag.Categories = categories;
        return View();
    }

    [HttpPost]
    public IActionResult AddProduct([Bind("ProductID, Name, Description, CategoryID, Price, Author")] Product product, List<IFormFile> ImageFiles)
    {
        ModelState.Remove("Category");

        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            if (ImageFiles != null && ImageFiles.Count > 0)
            {
                foreach (var imageFile in ImageFiles)
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(imageFile.FileName);
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        }

                        var relativePath = fileName;

                        var productImage = new ProductImage
                        {
                            ProductID = product.ProductID,
                            ImagePath = relativePath,
                            IsPrimary = true,
                            ImageType = "main"
                        };

                        _context.ProductImages.Add(productImage);
                    }
                }
                _context.SaveChanges();
            }
            TempData["SuccessMessage"] = "Sản phẩm được thêm thành công.";
            return RedirectToAction("ProductList");
        }

        var categories = _context.Categories
            .Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.Name
            }).ToList();

        ViewBag.Categories = categories;
        return View(product);
    }


    //sửa
    [HttpGet]
    public IActionResult EditProduct(int id)
    {
        var product = _context.Products.Include(p => p.ProductImages)
            .FirstOrDefault(p => p.ProductID == id);

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories = _context.Categories
            .Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.Name
            }).ToList();

        return View(product);
    }

    [HttpPost]
    public IActionResult EditProduct([Bind("ProductID, Name, Description, CategoryID, Price, Author")] Product product, List<IFormFile> ImageFiles)
    {
        ModelState.Remove("Category");

        if (ModelState.IsValid)
        {
            _context.Products.Update(product);
            _context.SaveChanges();

            if (ImageFiles != null && ImageFiles.Count > 0)
            {
                foreach (var imageFile in ImageFiles)
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(imageFile.FileName);
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        }

                        var relativePath = fileName;

                        var productImage = new ProductImage
                        {
                            ProductID = product.ProductID,
                            ImagePath = relativePath,
                            IsPrimary = true,
                            ImageType = "main"
                        };

                        _context.ProductImages.Add(productImage);
                    }
                }
                _context.SaveChanges();
            }

            TempData["SuccessMessage"] = "Sản phẩm được cập nhật thành công.";
            return RedirectToAction("ProductList");
        }

        ViewBag.Categories = _context.Categories
            .Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.Name
            }).ToList();

        return View(product);
    }


    //xóa
    [HttpPost]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductID == id);

        if (product != null)
        {
            var productImages = _context.ProductImages.Where(pi => pi.ProductID == id).ToList();
            if (productImages.Any())
            {
                _context.ProductImages.RemoveRange(productImages);
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Sản phẩm được xóa thành công";
        }

        return RedirectToAction("ProductList");
    }


    //2. quản lý đơn hàng( xem, update trạng thái đơn)
    //Xem danh sách đơn hàng
    public IActionResult Index()
    {
        var orders= _context.Orders
            .Include( o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        return View(orders);
    }

    //udate trặng thái đơn
    [HttpPost]
    public IActionResult UpdateOrderStatus(int orderId, string status)
    {
        var order = _context.Orders.FirstOrDefault(o => o.OrderID== orderId);
        if(order == null)
        {
            return NotFound();
        }
        order.Status = status;
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    //view detail đơn
    public IActionResult Details(int orderId)
    {
        var order = _context.Orders
            .Include ( o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefault(o => o.OrderID==orderId);

        if(order==null)
        {
            return NotFound();
        }
        return View(order);
    }

    //3. xem doanh thu theo ngày, tuần, tháng, năm= biểu đồ
    //API trả về json
    [HttpGet]
    public IActionResult GetRevenueData(string timeFrame="day")
    {
        DateTime now= DateTime.Now;
        IQueryable<Order> orders = _context.Orders.Where(o => o.Status == "Completed");

        var revenueData=new List<object>();

        switch (timeFrame.ToLower())
        {
            case "day":
                revenueData = orders
                    .AsEnumerable()
                    .GroupBy(o => o.OrderDate.Value.Date)
                    .Select(g => new
                    {
                        label = g.Key.ToString("dd/MM/yyyy"),
                        totalRevenue = g.Sum(o => o.TotalAmount ?? 0)
                    })
                    .OrderBy(r => DateTime.ParseExact(r.label, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                    .ToList<object>();
                break;

            case "week":
                revenueData = orders
                    .AsEnumerable()
                    .GroupBy(o => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear
                    (o.OrderDate.Value, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                    .Select(g => new
                    {
                        label = $"Week {g.Key}",
                        totalRevenue = g.Sum(o => o.TotalAmount ?? 0)
                    })
                    .OrderBy(r => r.label)
                    .ToList<object>();
                break;

            case "month":
                revenueData = orders
                    .AsEnumerable()
                    .GroupBy(o => o.OrderDate.Value.Month)
                    .Select(g => new
                    {
                        label = g.Key,
                        totalRevenue = g.Sum(o => o.TotalAmount ?? 0)
                    })
                    .OrderBy(r => r.label)
                    .ToList<object>();
                break;

            case "year":
                revenueData = orders
                    .AsEnumerable()
                    .GroupBy(o => o.OrderDate.Value.Year)
                    .Select(g => new
                    {
                        label = g.Key.ToString(),
                        totalRevenue = g.Sum(o => o.TotalAmount ?? 0)
                    })
                    .OrderBy(r => r.label)
                    .ToList<object>();
                break;

            default:
                return BadRequest("Invalid time frame");
        }
        return Json(revenueData);    
    }

    //trả về View
    [HttpGet]
    public IActionResult ViewRevenue()
    {
        return View();
    }
}
