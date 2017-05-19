using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class Image
    {
        public Image(string name, int length)
        {
            Name = name;
            Length = length;
            Type = Path.GetExtension(Name.ToLower());
        }
        public static List<string> AllowedItems = new List<string>
        {
            ".jpg",".png",".gif",".bmp",".jpeg"
        };

        public string Type { get; private set; }
        public int Length { get; private set; }
        public string Name { get; private set; }
    }
}
