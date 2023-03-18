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
    public class StoresController : Controller
    {
        private readonly FurnitureContext _context;

        public StoresController(FurnitureContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
              return _context.Stores != null ? 
                          View(await _context.Stores.ToListAsync()) :
                          Problem("Entity set 'FurnitureContext.Stores'  is null.");
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StoreName,ImagePath,StatusPublish")] Store store, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                var fileName = Path.GetFileName(image.FileName);
                store.ImagePath = image.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StoreName,ImagePath,StatusPublish")] Store store, IFormFile image)
        {
            if (id != store.Id)
            {
                return NotFound();
            }
            Store c = _context.Stores.Find(id);
            c.StoreName = store.StoreName;
            c.StatusPublish = store.StatusPublish;
            

            if (image != null)
            {
                var fileName = Path.GetFileName(image.FileName);
                c.ImagePath = image.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }
            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(c);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.Id))
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
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stores == null)
            {
                return Problem("Entity set 'FurnitureContext.Stores'  is null.");
            }
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
          return (_context.Stores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
