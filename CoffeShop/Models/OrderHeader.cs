using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeShop.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy\\MM\\dd HH:mm}",ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required]
        public string FullName { get; set; }

    }
}
