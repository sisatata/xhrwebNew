using ASL.Hrms.SharedKernel.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IImageFileManager
    {
        string UploadImageFile(IFormFile imageFile, string filePath, ImageFileSettings imageFileSettings = null);
    }
}
