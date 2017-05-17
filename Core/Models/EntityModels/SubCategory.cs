using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.EntityModels
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }

        [Required]
        [Display(Name = "Sub-Category")]
        [Column(TypeName = "varchar")]
        [StringLength(32, MinimumLength = 2)]
        public string SubCategoryName { get; set; }
    }
}
