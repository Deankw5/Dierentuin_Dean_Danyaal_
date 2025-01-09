namespace DIERENTUIN13.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Zoo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Relatie met Enclosures
        public List<Enclosure> Enclosures { get; set; } = new List<Enclosure>();
    }
}
