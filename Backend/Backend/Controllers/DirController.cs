using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    //[Authorize(Roles = "ADMIN, USER")]
    public class DirController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DirController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dir
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
                ViewData["id"] = "non";
            else ViewData["id"] = id;
            var applicationDbContext = _context.Catalogs.Include(c => c.ParentCatalog).Include(c => c.Messages).Include(c => c.ChildCatalogs);
            return View(await applicationDbContext.ToListAsync());
            //var applicationDbContext = _context.Catalogs.Include(c => c.ParentCatalog)
            //    .Where(p=>p.Id==id)
            //    .Include(c => c.Messages).Include(c=>c.ChildCatalogs);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Dir/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .Include(c => c.ParentCatalog)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // GET: Dir/Create
        public IActionResult Create()
        {
            ViewData["ParentCatalogId"] = new SelectList(_context.Catalogs, "Id", "Id");
            return View();
        }

        // POST: Dir/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ParentCatalogId,Id")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCatalogId"] = new SelectList(_context.Catalogs, "Id", "Id", catalog.ParentCatalogId);
            return View(catalog);
        }

        // GET: Dir/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs.SingleOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["ParentCatalogId"] = new SelectList(_context.Catalogs, "Id", "Id", catalog.ParentCatalogId);
            return View(catalog);
        }

        // POST: Dir/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Title,ParentCatalogId,Id")] Catalog catalog)
        {
            if (id != catalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogExists(catalog.Id))
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
            ViewData["ParentCatalogId"] = new SelectList(_context.Catalogs, "Id", "Id", catalog.ParentCatalogId);
            return View(catalog);
        }

        // GET: Dir/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .Include(c => c.ParentCatalog)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // POST: Dir/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var catalog = await _context.Catalogs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Catalogs.Remove(catalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogExists(string id)
        {
            return _context.Catalogs.Any(e => e.Id == id);
        }
    }
}
