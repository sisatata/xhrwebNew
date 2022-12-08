using ASL.Hrms.SharedKernel.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IFileManager
    {
        string UploadFile(IFormFile file, string filePath, FileSettings fileSettings = null);
    }
}
