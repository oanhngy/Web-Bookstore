using System.ComponentModel.Design;
using BookstoreWeb.Data;
using BookstoreWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWeb.Controllers
{
    [Authorize(Roles="Admin")]
    public class CategoryController:Controller
    {
        private readonly BookstoreContext _context;
        public CategoryController(BookstoreContext context)
        {
            _context=context;
        }

        //lấy all category+hiển thị
        public IActionResult Index()
        {
            var categories=_context.Categories.ToList();
            return View(categories);
        }

        //tạo form
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //sửa
        public IActionResult EditCategory(int id)
        {
            var category=_context.Categories.Find(id);
            if(category==null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //xóa
        [HttpPost] 
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}