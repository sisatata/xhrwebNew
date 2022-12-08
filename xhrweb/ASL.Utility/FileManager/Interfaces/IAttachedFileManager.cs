using ASL.Utility.FileManager.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Utility.FileManager.Interfaces
{
    public interface IAttachedFileManager
    {
        string UploadAttachedFile(IFormFile imageFile, string filePath, AttachedFileSettings attachedFileSettings = null);
    }
}
