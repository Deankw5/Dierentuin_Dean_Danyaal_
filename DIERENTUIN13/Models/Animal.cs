namespace DIERENTUIN13.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Species { get; set; }

        // Relatie met Category (nullable)
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        public SizeEnum Size { get; set; }

        [Required]
        public DietaryClassEnum DietaryClass { get; set; }

        [Required]
        public ActivityPatternEnum ActivityPattern { get; set; }

        public List<Animal>? Prey { get; set; } = new List<Animal>();

        // Relatie met Enclosure (nullable)
        public int? EnclosureId { get; set; }
        public Enclosure? Enclosure { get; set; }

        [Required]
        public double SpaceRequirement { get; set; } // in m² per dier

        [Required]
        public SecurityLevelEnum SecurityRequirement { get; set; }
    }

    public enum SizeEnum
    {
        Microscopic,
        VerySmall,
        Small,
        Medium,
        Large,
        VeryLarge
    }

    public enum DietaryClassEnum
    {
        Carnivore,
        Herbivore,
        Omnivore,
        Insectivore,
        Piscivore
    }

    public enum ActivityPatternEnum
    {
        Diurnal,  // Dagactief
        Nocturnal, // Nachtactief
        Cathemeral // Afwisselend actief
    }

    public enum SecurityLevelEnum
    {
        Low,
        Medium,
        High
    }

}
