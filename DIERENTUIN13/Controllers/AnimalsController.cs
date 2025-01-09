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
    public class AnimalsController : Controller
    {
        private readonly DIERENTUIN13Context _context;

        public AnimalsController(DIERENTUIN13Context context)
        {
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var dIERENTUIN13Context = _context.Animal.Include(a => a.Category).Include(a => a.Enclosure);
            return View(await dIERENTUIN13Context.ToListAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.Category)
                .Include(a => a.Enclosure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }


        public async Task<IActionResult> Filter(string searchString)
        {
            var animals = from a in _context.Animal.Include(a => a.Category).Include(a => a.Enclosure)
                          select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(s => s.Name.Contains(searchString) ||
                                             s.Species.Contains(searchString) ||
                                             s.Category.Name.Contains(searchString) ||
                                             s.Enclosure.Name.Contains(searchString) ||
                                             s.Size.ToString().Contains(searchString) ||
                                             s.DietaryClass.ToString().Contains(searchString) ||
                                             s.ActivityPattern.ToString().Contains(searchString) ||
                                             s.SpaceRequirement.ToString().Contains(searchString) ||
                                             s.SecurityRequirement.ToString().Contains(searchString));
            }

            return View("Index", await animals.ToListAsync());
        }










        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            ViewData["EnclosureId"] = new SelectList(_context.Set<Enclosure>(), "Id", "Name");

            ViewData["Sizes"] = Enum.GetValues(typeof(SizeEnum)).Cast<SizeEnum>();
            ViewData["DietaryClasses"] = Enum.GetValues(typeof(DietaryClassEnum)).Cast<DietaryClassEnum>();
            ViewData["ActivityPatterns"] = Enum.GetValues(typeof(ActivityPatternEnum)).Cast<ActivityPatternEnum>();
            ViewData["SecurityRequirements"] = Enum.GetValues(typeof(SecurityLevelEnum)).Cast<SecurityLevelEnum>();

            ViewData["Prey"] = new SelectList(_context.Animal, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Species,CategoryId,Size,DietaryClass,ActivityPattern,Prey,EnclosureId,SpaceRequirement,SecurityRequirement")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", animal.CategoryId);
            ViewData["EnclosureId"] = new SelectList(_context.Set<Enclosure>(), "Id", "Name", animal.EnclosureId);
            return View(animal);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", animal.CategoryId);
            ViewData["EnclosureId"] = new SelectList(_context.Set<Enclosure>(), "Id", "Name", animal.EnclosureId);

            ViewData["Sizes"] = Enum.GetValues(typeof(SizeEnum)).Cast<SizeEnum>();
            ViewData["DietaryClasses"] = Enum.GetValues(typeof(DietaryClassEnum)).Cast<DietaryClassEnum>();
            ViewData["ActivityPatterns"] = Enum.GetValues(typeof(ActivityPatternEnum)).Cast<ActivityPatternEnum>();
            ViewData["SecurityRequirements"] = Enum.GetValues(typeof(SecurityLevelEnum)).Cast<SecurityLevelEnum>();

            return View(animal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Species,CategoryId,Size,DietaryClass,ActivityPattern,Prey,EnclosureId,SpaceRequirement,SecurityRequirement")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "Name", animal.CategoryId);
            ViewData["EnclosureId"] = new SelectList(_context.Set<Enclosure>(), "Id", "Name", animal.EnclosureId);

            ViewData["Sizes"] = Enum.GetValues(typeof(SizeEnum)).Cast<SizeEnum>();
            ViewData["DietaryClasses"] = Enum.GetValues(typeof(DietaryClassEnum)).Cast<DietaryClassEnum>();
            ViewData["ActivityPatterns"] = Enum.GetValues(typeof(ActivityPatternEnum)).Cast<ActivityPatternEnum>();
            ViewData["SecurityRequirements"] = Enum.GetValues(typeof(SecurityLevelEnum)).Cast<SecurityLevelEnum>();

            return View(animal);
        }





        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.Category)
                .Include(a => a.Enclosure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            if (animal != null)
            {
                _context.Animal.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }



    }
}
