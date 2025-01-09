namespace DIERENTUIN13.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        
        public string Name { get; set; }

        // Relatie met Animals
        public List<Animal> Animals { get; set; } = new List<Animal>();
    }




}



