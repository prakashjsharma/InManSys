using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InManSys.Models;

namespace InManSys.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context) => _context = context;

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .OrderBy(p => p.ProductName)
                .ToListAsync();
            return View(products);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierId", "SupplierName");
            return View();
        }

        // POST: Product/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,UnitPrice,Quantity,ReorderLevel,CategoryId, SupplierId,ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }

                    product.ProductImage = "/uploads/" + uniqueFileName;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierId", "SupplierName", product.SupplierId);
            return View(product);
        }

        // ✅ GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierId", "SupplierName", product.SupplierId);
            return View(product);
        }

        // ✅ POST: Product/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,UnitPrice,Quantity,ReorderLevel,CategoryId, SupplierId,ImageFile,ProductImage")] Product product)
        {
            if (id != product.ProductId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageFile != null)
                    {
                        string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");
                        if (!Directory.Exists(uploadDir))
                            Directory.CreateDirectory(uploadDir);

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                        string filePath = Path.Combine(uploadDir, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(fileStream);
                        }

                        product.ProductImage = "/uploads/" + uniqueFileName;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.ProductId == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(await _context.Categories.ToListAsync(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierId", "SupplierName", product.SupplierId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _context.Products.FindAsync(id);
            if (p != null) _context.Products.Remove(p);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
