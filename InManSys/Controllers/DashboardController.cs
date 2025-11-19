using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InManSys.Models;

namespace InManSys.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Report()
        {
            // Total counts
            ViewBag.TotalProducts = await _context.Products.CountAsync();
            ViewBag.TotalCategories = await _context.Categories.CountAsync();
            ViewBag.TotalSuppliers = await _context.Suppliers.CountAsync();

            // Products by Category
            var prodByCat = await _context.Products
            .GroupBy(p => new { p.CategoryId, p.Category.CategoryName })
            .Select(g => new { Id = g.Key.CategoryId, Category = g.Key.CategoryName, Count = g.Count() })
            .ToListAsync();

            // Products by Supplier
            var prodBySup = await _context.Products
            .GroupBy(p => new { p.SupplierId, p.Supplier.SupplierName })
            .Select(g => new { Id = g.Key.SupplierId, Supplier = g.Key.SupplierName, Count = g.Count() })
            .ToListAsync();

            ViewBag.ProductsByCategory = prodByCat;
            ViewBag.ProductsBySupplier = prodBySup;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int id)
        {
            var list = await _context.Products
                .Where(p => p.CategoryId == id)
                .Select(p => new { p.ProductImage, p.ProductName, p.UnitPrice, p.Quantity })
                .ToListAsync();

            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsBySupplier(int id)
        {
            var list = await _context.Products
                .Where(p => p.SupplierId == id)
                .Select(p => new { p.ProductImage, p.ProductName, p.UnitPrice, p.Quantity })
                .ToListAsync();

            return Json(list);
        }

    }
}
