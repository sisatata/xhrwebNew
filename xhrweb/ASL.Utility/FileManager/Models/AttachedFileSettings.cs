using System.Collections.Generic;

namespace ASL.Utility.FileManager.Models
{
    public class AttachedFileSettings
    {
        public List<string> ValidExtensions { get; set; }
        //public bool ApplyResize { get; set; }
        //public int Width { get; set; }
        //public int Height { get; set; }
        public bool IsDeleteDuplicate { get; set; }
        public double MaxFileSize { get; set; }
    }
}
