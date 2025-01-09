using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DIERENTUIN13.Data;
using DIERENTUIN13.Models;

namespace DIERENTUIN13.Controllers
{
    public class ZoosController : Controller
    {
        private readonly DIERENTUIN13Context _context;

        public ZoosController(DIERENTUIN13Context context)
        {
            _context = context;
        }

        // GET: Zoos
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var zoos = from z in _context.Zoo
                       select z;

            if (!String.IsNullOrEmpty(searchString))
            {
                zoos = zoos.Where(s => s.Name.Contains(searchString));
            }

            return View(await zoos.ToListAsync());
        }



        // GET: Zoos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoo = await _context.Zoo
                .Include(z => z.Enclosures)
                    .ThenInclude(e => e.Animals)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zoo == null)
            {
                return NotFound();
            }

            return View(zoo);
        }

        // GET: Zoos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zoos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Zoo zoo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zoo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zoo);
        }

        // GET: Zoos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoo = await _context.Zoo.FindAsync(id);
            if (zoo == null)
            {
                return NotFound();
            }
            return View(zoo);
        }

        // POST: Zoos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Zoo zoo)
        {
            if (id != zoo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zoo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZooExists(zoo.Id))
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
            return View(zoo);
        }

        // GET: Zoos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoo = await _context.Zoo
                .Include(z => z.Enclosures)
                    .ThenInclude(e => e.Animals)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zoo == null)
            {
                return NotFound();
            }

            return View(zoo);
        }

        // POST: Zoos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zoo = await _context.Zoo.FindAsync(id);
            if (zoo != null)
            {
                _context.Zoo.Remove(zoo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZooExists(int id)
        {
            return _context.Zoo.Any(e => e.Id == id);
        }
    }
}
