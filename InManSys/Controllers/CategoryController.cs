using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InManSys.Models;

namespace InManSys.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context) => _context = context;

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var items = await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync();
            return View(items);
        }

        // GET: Category/Create
        public IActionResult Create() => View();

        // POST: Category/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName")] Category category)
        {
            if (!ModelState.IsValid) return View(category);
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (id != category.CategoryId) return NotFound();
            if (!ModelState.IsValid) return View(category);
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Categories.AnyAsync(e => e.CategoryId == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var cat = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat != null) _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
