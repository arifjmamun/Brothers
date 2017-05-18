using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.EntityModels
{
    public class Upload
    {
        public int UploadId { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(5)]
        public string Drive { get; set; }
        
        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Min and Max value: 2 and 150")]
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

        public virtual byte[] Thumbnail { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime LastUpdate { get; set; }

    }
}
