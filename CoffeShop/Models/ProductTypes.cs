using System.ComponentModel.DataAnnotations;

namespace CoffeShop.Models
{
    public class ProductTypes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
