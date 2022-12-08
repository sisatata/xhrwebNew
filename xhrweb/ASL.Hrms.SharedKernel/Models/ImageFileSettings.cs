using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Models
{
    public class ImageFileSettings
    {
        public List<string> ValidExtensions { get; set; }
        public bool ApplyResize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsDeleteDuplicate { get; set; }
        public double MaxFileSize { get; set; }
    }
}
