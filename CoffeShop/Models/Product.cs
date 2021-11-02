using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeShop.Models
{
    public class Product
    {
        public Product()
        {
            Quantity = 1;
        }


        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public double Price { get; set; }

        public string Image { get; set; }

        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "Product Type")]
        public int ProductTypesId { get; set; }

        [ForeignKey("ProductTypesId")]
        public virtual ProductTypes ProductTypes { get; set; }

        [NotMapped]
        [Range(1,1000)]
        public int Quantity { get; set; }

    }
}
