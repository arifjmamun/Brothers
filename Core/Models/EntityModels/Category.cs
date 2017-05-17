using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.EntityModels
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category")]
        [Column(TypeName = "varchar")]
        [StringLength(32, MinimumLength = 2)]
        public string CategoryName { get; set; }
    }
}
