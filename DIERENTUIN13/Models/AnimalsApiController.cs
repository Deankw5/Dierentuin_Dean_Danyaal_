using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DIERENTUIN13.Data;
using DIERENTUIN13.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIERENTUIN13.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsApiController : ControllerBase
    {
        private readonly DIERENTUIN13Context _context;

        public AnimalsApiController(DIERENTUIN13Context context)
        {
            _context = context;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return await _context.Animal.Include(a => a.Category).Include(a => a.Enclosure).ToListAsync();
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animal.Include(a => a.Category).Include(a => a.Enclosure).FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        // PUT: api/Animals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Animals
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animal.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Animals/Sunrise
        [HttpGet("Sunrise")]
        public ActionResult<IEnumerable<string>> GetSunriseActions()
        {
            var actions = _context.Animal.Select(a => new { a.Name, Action = a.ActieSunrise() }).ToList();
            return Ok(actions);
        }

        // GET: api/Animals/Sunset
        [HttpGet("Sunset")]
        public ActionResult<IEnumerable<string>> GetSunsetActions()
        {
            var actions = _context.Animal.Select(a => new { a.Name, Action = a.ActieSunset() }).ToList();
            return Ok(actions);
        }

        // GET: api/Animals/FeedingTime
        [HttpGet("FeedingTime")]
        public ActionResult<IEnumerable<string>> GetFeedingTimeActions()
        {
            var actions = _context.Animal.Select(a => new { a.Name, Action = a.ActieFeedingTime() }).ToList();
            return Ok(actions);
        }

        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }
    }
}
