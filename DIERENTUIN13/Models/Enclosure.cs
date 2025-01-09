namespace DIERENTUIN13.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Enclosure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Relatie met Animals
        public List<Animal> Animals { get; set; } = new List<Animal>();

        [Required]
        public ClimateEnum Climate { get; set; }

        [Required]
        public HabitatTypeEnum HabitatType { get; set; } // Flags Enum

        [Required]
        public SecurityLevelEnum SecurityLevel { get; set; }

        [Required]
        public double Size { get; set; } // in m²

        // Relatie met Zoo (nullable)
        public int? ZooId { get; set; }
        public Zoo? Zoo { get; set; }
    }

    public enum ClimateEnum
    {
        Tropical,
        Temperate,
        Arctic
    }

    [Flags]
    public enum HabitatTypeEnum
    {
        Forest = 1,
        Aquatic = 2,
        Desert = 4,
        Grassland = 8
    }
}
