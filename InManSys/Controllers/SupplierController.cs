using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InManSys.Models;

namespace InManSys.Controllers
{
    public class SupplierController:Controller
    {
        public readonly AppDbContext _context;
        public SupplierController(AppDbContext context) => _context = context;

        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            var items = await _context.Suppliers.OrderBy(c => c.SupplierName).ToListAsync();
            return View(items);
        }

        // GET: Supplier/Create
        public IActionResult Create() => View();

        // POST: Supplier/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierName")] Supplier supplier)
        {
            if (!ModelState.IsValid) return View(supplier);
            _context.Add(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var sup = await _context.Suppliers.FindAsync(id);
            if (sup == null) return NotFound();
            return View(sup);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,SupplierName")] Supplier supplier)
        {
            if (id != supplier.SupplierId) return NotFound();
            if (!ModelState.IsValid) return View(supplier);
            try
            {
                _context.Update(supplier);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Suppliers.AnyAsync(e => e.SupplierId == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var sup = await _context.Suppliers.FirstOrDefaultAsync(c => c.SupplierId == id);
            if (sup == null) return NotFound();
            return View(sup);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sup = await _context.Suppliers.FindAsync(id);
            if (sup != null) _context.Suppliers.Remove(sup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
