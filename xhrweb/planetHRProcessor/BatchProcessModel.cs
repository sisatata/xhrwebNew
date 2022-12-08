using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planetHRProcessor
{
    public class BatchProcessModel
    {
        //public string MyProperty { get; set; }
        public int BatchProcessId { get; set; }
        public string ProcessName { get; set; }
        public string ProcessCode { get; set; }
        public string FileTypeCode { get; set; }
        public string FileName { get; set; }
        public string StatusCode { get; set; }
        public string Active { get; set; }
        public string Frequency { get; set; }
        public int Runevery { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? LastRun { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }

        public string HandlerAssembly { get; set; }
        public string HandlerClass { get; set; }
    }
}
