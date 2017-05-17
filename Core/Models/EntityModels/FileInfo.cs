using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.EntityModels
{
    public class FileInfo
    {
        public int FileInfoId { get; set; }

        [Required]
        [Display(Name = "File Name")]
        [Column(TypeName = "varchar")]
        [StringLength(255, MinimumLength = 2)]
        public string FileName { get; set; }

        [ForeignKey("Upload")]
        public int UploadId { get; set; }
        public virtual Upload Upload { get; set; }
    }
}
