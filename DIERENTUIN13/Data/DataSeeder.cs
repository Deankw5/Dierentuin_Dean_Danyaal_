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
            // Clear existing data from the database
            ClearExistingData();

            // Seed Categories with unique names
            var categories = GenerateUniqueEntities<Category>(new[] { "Mammals", "Birds", "Reptiles", "Amphibians", "Fish" }, (name, index) => new Category { Name = $"{name} {index + 1}" });
            _context.Category.AddRange(categories);
            _context.SaveChanges();

            // Seed Zoos with unique names
            var zoos = GenerateUniqueEntities<Zoo>(new[] { "Zoo A", "Zoo B", "Zoo C" }, (name, index) => new Zoo { Name = $"{name} {index + 1}" });
            _context.Zoo.AddRange(zoos);
            _context.SaveChanges();

            // Seed Enclosures with unique names and assign them to zoos
            var enclosures = GenerateUniqueEntities<Enclosure>(new[] { "Savannah", "Rainforest", "Desert", "Aquarium", "Aviary" }, (name, index) => new Enclosure
            {
                Name = $"{name} {index + 1}",
                Climate = (ClimateEnum)(index % 3), // Assign a climate type based on the index
                HabitatType = (HabitatTypeEnum)(index % 3), // Assign a habitat type based on the index
                SecurityLevel = (SecurityLevelEnum)(index % 3), // Assign a security level based on the index
                Size = 1000 + index * 500, // Assign a size based on the index
                ZooId = zoos[index % zoos.Count].Id // Assign a zoo to the enclosure
            });
            _context.Enclosure.AddRange(enclosures);
            _context.SaveChanges();

            // Seed Animals with unique names and assign them to categories and enclosures
            var animalNames = new[] { "Lion", "Elephant", "Crocodile", "Frog", "Shark" };
            var speciesNames = new[] { "Panthera leo", "Loxodonta africana", "Crocodylus niloticus", "Rana temporaria", "Carcharodon carcharias" };
            var animals = animalNames.Select((name, index) => new Animal
            {
                Name = $"{name} {index + 1}",
                Species = speciesNames[index],
                CategoryId = categories[index % categories.Count].Id, // Assign a category to the animal
                Size = (SizeEnum)(index % 3), // Assign a size based on the index
                DietaryClass = (DietaryClassEnum)(index % 3), // Assign a dietary class based on the index
                ActivityPattern = (ActivityPatternEnum)(index % 3), // Assign an activity pattern based on the index
                Prey = string.Join(", ", new[] { "Zebra", "Buffalo", "Fish", "Insects", "Small mammals" }.Take(3)), // Assign prey items
                EnclosureId = enclosures[index % enclosures.Count].Id, // Assign an enclosure to the animal
                SpaceRequirement = 10 + index * 10, // Assign a space requirement based on the index
                SecurityRequirement = (SecurityLevelEnum)(index % 3) // Assign a security requirement based on the index
            }).ToList();
            _context.Animal.AddRange(animals);
            _context.SaveChanges();
        }

        // Helper method to clear existing data from the database
        private void ClearExistingData()
        {
            _context.Animal.RemoveRange(_context.Animal);
            _context.Category.RemoveRange(_context.Category);
            _context.Enclosure.RemoveRange(_context.Enclosure);
            _context.Zoo.RemoveRange(_context.Zoo);
            _context.SaveChanges();
        }

        // Helper method to generate unique entities
        private List<T> GenerateUniqueEntities<T>(string[] names, Func<string, int, T> createEntity) where T : class
        {
            return names.Select((name, index) => createEntity(name, index)).ToList();
        }
    }
}
