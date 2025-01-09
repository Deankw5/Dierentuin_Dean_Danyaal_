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

        // Relationship with Category (nullable)
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

        // Relationship with Enclosure (nullable)
        public int? EnclosureId { get; set; }
        public Enclosure? Enclosure { get; set; }

        [Required]
        public double SpaceRequirement { get; set; }

        [Required]
        public SecurityLevelEnum SecurityRequirement { get; set; }

        // Method to get the action at sunrise based on the activity pattern
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

        // Method to get the action at sunset based on the activity pattern
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

        // Method to get the feeding time action
        public string ActieFeedingTime()
        {
            return $"Eats: {Prey}";
        }
    }

    // Enum for Size
    public enum SizeEnum
    {
        Microscopic,
        VerySmall,
        Small,
        Medium,
        Large,
        VeryLarge
    }

    // Enum for Dietary Class
    public enum DietaryClassEnum
    {
        Carnivore,
        Herbivore,
        Omnivore,
        Insectivore,
        Piscivore
    }

    // Enum for Activity Pattern
    public enum ActivityPatternEnum
    {
        Diurnal,  // Active during the day
        Nocturnal, // Active during the night
        Cathemeral // Active at intervals throughout the day and night
    }

    // Enum for Security Level
    public enum SecurityLevelEnum
    {
        Low,
        Medium,
        High
    }
}

