using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Masters.Models;

namespace Masters.Controllers
{
    public class CategoryStoresController : Controller
    {
        private readonly FurnitureContext _context;

        public CategoryStoresController(FurnitureContext context)
        {
            _context = context;
        }

        // GET: CategoryStores
        public async Task<IActionResult> Index()
        {
            var furnitureContext = _context.CategoryStores.Include(c => c.Cat).Include(c => c.Store);
            return View(await furnitureContext.ToListAsync());
        }

        // GET: CategoryStores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryStores == null)
            {
                return NotFound();
            }

            var categoryStore = await _context.CategoryStores
                .Include(c => c.Cat)
                .Include(c => c.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryStore == null)
            {
                return NotFound();
            }

            return View(categoryStore);
        }

        // GET: CategoryStores/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName");
            return View();
        }

        // POST: CategoryStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CatId,StoreId")] CategoryStore categoryStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "CategoryName", categoryStore.CatId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName", categoryStore.StoreId);
            return View(categoryStore);
        }

        // GET: CategoryStores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryStores == null)
            {
                return NotFound();
            }

            var categoryStore = await _context.CategoryStores.FindAsync(id);
            if (categoryStore == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "CategoryName", categoryStore.CatId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName", categoryStore.StoreId);
            return View(categoryStore);
        }

        // POST: CategoryStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CatId,StoreId")] CategoryStore categoryStore)
        {
            if (id != categoryStore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryStoreExists(categoryStore.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "Id", "CategoryName", categoryStore.CatId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName", categoryStore.StoreId);
            return View(categoryStore);
        }

        // GET: CategoryStores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryStores == null)
            {
                return NotFound();
            }

            var categoryStore = await _context.CategoryStores
                .Include(c => c.Cat)
                .Include(c => c.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryStore == null)
            {
                return NotFound();
            }

            return View(categoryStore);
        }

        // POST: CategoryStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryStores == null)
            {
                return Problem("Entity set 'FurnitureContext.CategoryStores'  is null.");
            }
            var categoryStore = await _context.CategoryStores.FindAsync(id);
            if (categoryStore != null)
            {
                _context.CategoryStores.Remove(categoryStore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryStoreExists(int id)
        {
          return (_context.CategoryStores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
