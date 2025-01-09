namespace DIERENTUIN13.Models
{
    using System.ComponentModel.DataAnnotations;

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

        [MaxLength(500)]
        public string Prey { get; set; }

        // Relatie met Enclosure (nullable)
        public int? EnclosureId { get; set; }
        public Enclosure? Enclosure { get; set; }

        [Required]
        public double SpaceRequirement { get; set; }

        [Required]
        public SecurityLevelEnum SecurityRequirement { get; set; }

        public string ActieSunrise()
        {
            return ActivityPattern switch
            {
                ActivityPatternEnum.Diurnal => "Wakes up",
                ActivityPatternEnum.Nocturnal => "Goes to sleep",
                ActivityPatternEnum.Cathemeral => "Always active",
                _ => "Unknown"
            };
        }

        public string ActieSunset()
        {
            return ActivityPattern switch
            {
                ActivityPatternEnum.Diurnal => "Goes to sleep",
                ActivityPatternEnum.Nocturnal => "Wakes up",
                ActivityPatternEnum.Cathemeral => "Always active",
                _ => "Unknown"
            };
        }

        public string ActieFeedingTime()
        {
            return $"Eats: {Prey}";
        }
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
