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
    public class ProductsController : Controller
    {
        private readonly FurnitureContext _context;

        public ProductsController(FurnitureContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var furnitureContext = _context.Products.Include(p => p.CategoryStore);
            return View(await furnitureContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CategoryStore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryStoreId1"] = new SelectList(_context.CategoryStores.Include(obj => obj.Store), "Id", "Store.StoreName");

            ViewData["CategoryStoreId"] = new SelectList(_context.CategoryStores.Include(obj=>obj.Cat), "Id", "Cat.CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Quantity,Price,ImagePath,CategoryStoreId")] Product product, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                var fileName = Path.GetFileName(image.FileName);
                product.ImagePath = image.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryStoreId"] = new SelectList(_context.CategoryStores, "Id", "Id",product.CategoryStoreId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryStoreId"] = new SelectList(_context.CategoryStores, "Id", "Id", product.CategoryStoreId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Quantity,Price,ImagePath,CategoryStoreId")] Product product, IFormFile image)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
           
            Product c =_context.Products.Find(id);
            c.Name= product.Name;
            c.Price= product.Price;
            c.Description= product.Description;
            c.Quantity= product.Quantity;
            c.CategoryStoreId= product.CategoryStoreId;
            c.ImagePath=product.ImagePath;

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
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryStoreId"] = new SelectList(_context.CategoryStores, "Id", "Id", product.CategoryStoreId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CategoryStore)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'FurnitureContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
