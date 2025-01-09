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

        // Relationship with Animals
        public List<Animal> Animals { get; set; } = new List<Animal>();

        [Required]
        public ClimateEnum Climate { get; set; }

        [Required]
        public HabitatTypeEnum HabitatType { get; set; }

        [Required]
        public SecurityLevelEnum SecurityLevel { get; set; }

        [Required]
        public double Size { get; set; }

        // Relationship with Zoo (nullable)
        public int? ZooId { get; set; }
        public Zoo? Zoo { get; set; }
    }

    // Enum for Climate
    public enum ClimateEnum
    {
        Tropical,
        Temperate,
        Arctic
    }

    // Enum for Habitat Type
    public enum HabitatTypeEnum
    {
        Forest,
        Desert,
        Grassland,
        Wetland,
        Marine
    }

 
}


