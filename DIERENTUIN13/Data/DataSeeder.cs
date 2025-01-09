using Bogus;
using DIERENTUIN13.Data;
using DIERENTUIN13.Models;
using System.Linq;

namespace DIERENTUIN13.Data
{
    public class DataSeeder
    {
        private readonly DIERENTUIN13Context _context;

        public DataSeeder(DIERENTUIN13Context context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Clear existing data
            _context.Animal.RemoveRange(_context.Animal);
            _context.Category.RemoveRange(_context.Category);
            _context.Enclosure.RemoveRange(_context.Enclosure);
            _context.SaveChanges();

            // Seed Categories
            var categories = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.PickRandom(new[] { "Mammals", "Birds", "Reptiles", "Amphibians", "Fish" }))
                .Generate(5);

            _context.Category.AddRange(categories);
            _context.SaveChanges();

            // Seed Enclosures
            var enclosures = new Faker<Enclosure>()
                .RuleFor(e => e.Name, f => f.PickRandom(new[] { "Savannah", "Rainforest", "Desert", "Aquarium", "Aviary" }))
                .RuleFor(e => e.Climate, f => f.PickRandom<ClimateEnum>())
                .RuleFor(e => e.HabitatType, f => f.PickRandom<HabitatTypeEnum>())
                .RuleFor(e => e.SecurityLevel, f => f.PickRandom<SecurityLevelEnum>())
                .RuleFor(e => e.Size, f => f.Random.Double(500, 2000))
                .Generate(5);

            _context.Enclosure.AddRange(enclosures);
            _context.SaveChanges();

            // Seed Animals
            var animals = new Faker<Animal>()
                .RuleFor(a => a.Name, f => f.PickRandom(new[] { "Lion", "Elephant", "Crocodile", "Frog", "Shark" }))
                .RuleFor(a => a.Species, f => f.PickRandom(new[] { "Panthera leo", "Loxodonta africana", "Crocodylus niloticus", "Rana temporaria", "Carcharodon carcharias" }))
                .RuleFor(a => a.CategoryId, f => f.PickRandom(categories).Id)
                .RuleFor(a => a.Size, f => f.PickRandom<SizeEnum>())
                .RuleFor(a => a.DietaryClass, f => f.PickRandom<DietaryClassEnum>())
                .RuleFor(a => a.ActivityPattern, f => f.PickRandom<ActivityPatternEnum>())
                .RuleFor(a => a.Prey, f => string.Join(", ", f.PickRandom(new[] { "Zebra", "Buffalo", "Fish", "Insects", "Small mammals" }, 3)))
                .RuleFor(a => a.EnclosureId, f => f.PickRandom(enclosures).Id)
                .RuleFor(a => a.SpaceRequirement, f => f.Random.Double(10, 100))
                .RuleFor(a => a.SecurityRequirement, f => f.PickRandom<SecurityLevelEnum>())
                .Generate(20);

            _context.Animal.AddRange(animals);
            _context.SaveChanges();
        }
    }
}
