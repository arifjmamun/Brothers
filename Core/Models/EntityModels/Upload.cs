using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.EntityModels
{
    public class Upload
    {
        public int UploadId { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(5)]
        public string Drrive { get; set; }
        
        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(150, MinimumLength = 2)]
        public string Title { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "Sub-Category")]
        public int? SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }

        public string UploadPath { get; set; }

        public List<FileInfo> FileInfos { get; set; }

        public byte? Thumbnail { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime LastUpdate { get; set; }

    }
}
