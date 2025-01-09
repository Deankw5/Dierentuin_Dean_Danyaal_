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
            if (_context.Animal.Any() || _context.Category.Any() || _context.Enclosure.Any())
            {
                return; // DB has been seeded
            }

            // Seed Categories
            var categories = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                .Generate(5);

            _context.Category.AddRange(categories);
            _context.SaveChanges();

            // Seed Enclosures
            var enclosures = new Faker<Enclosure>()
                .RuleFor(e => e.Name, f => f.Address.City())
                .RuleFor(e => e.Climate, f => f.PickRandom<ClimateEnum>())
                .RuleFor(e => e.HabitatType, f => f.PickRandom<HabitatTypeEnum>())
                .RuleFor(e => e.SecurityLevel, f => f.PickRandom<SecurityLevelEnum>())
                .RuleFor(e => e.Size, f => f.Random.Double(500, 2000))
                .Generate(5);

            _context.Enclosure.AddRange(enclosures);
            _context.SaveChanges();

            // Seed Animals
            var animals = new Faker<Animal>()
                .RuleFor(a => a.Name, f => f.Name.FirstName())
                .RuleFor(a => a.Species, f => f.Name.FirstName())
                .RuleFor(a => a.CategoryId, f => f.PickRandom(categories).Id)
                .RuleFor(a => a.Size, f => f.PickRandom<SizeEnum>())
                .RuleFor(a => a.DietaryClass, f => f.PickRandom<DietaryClassEnum>())
                .RuleFor(a => a.ActivityPattern, f => f.PickRandom<ActivityPatternEnum>())
                .RuleFor(a => a.Prey, f => string.Join(", ", f.Lorem.Words(3)))
                .RuleFor(a => a.EnclosureId, f => f.PickRandom(enclosures).Id)
                .RuleFor(a => a.SpaceRequirement, f => f.Random.Double(10, 100))
                .RuleFor(a => a.SecurityRequirement, f => f.PickRandom<SecurityLevelEnum>())
                .Generate(20);

            _context.Animal.AddRange(animals);
            _context.SaveChanges();
        }
    }
}
