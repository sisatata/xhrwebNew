using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Services
{
    public class FileManager : IFileManager
    {
        public string UploadFile(IFormFile file, string filePath, FileSettings fileSettings = null)
        {
            

            var result = string.Empty;
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (file.Length > 0)
                {
                    ValidateFile(file, fileSettings);

                    //var fileName = $"ASL_{Utility.GenerateUniqueKey(10)}{Path.GetExtension(file.FileName).ToLower()}";
                    var fullPath = Path.Combine(filePath, $"{file.FileName}");
                    if (File.Exists(fullPath) && !fileSettings.IsDeleteDuplicate)
                    {
                        result = string.Format("This file already uploaded. Please choose another file and try again.");
                        return result;
                    }
                    if (File.Exists(fullPath) && fileSettings.IsDeleteDuplicate)
                    {
                        File.Delete(fullPath);
                    }

                    using (Stream fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                    }

                    result = $"{file.FileName}";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private void ValidateFile(IFormFile file, FileSettings fileSettings)
        {
            if (fileSettings == null) return;

            var extension = Path.GetExtension(file.FileName).ToLower();

            var validExtensions = fileSettings?.ValidExtensions.ToList().Select(s => s.Trim()).ToList();


            if (validExtensions.Count() > 0 && validExtensions.Contains(extension) == false)
            {
                throw new Exception($"File extension {extension} is not valid. Please upload a file in {string.Join(", ", fileSettings.ValidExtensions)} format.");
            }

            var fileSize = file.Length;

            if (fileSize == 0)
            {
                throw new Exception("Empty file is not supported");
            }

            if (fileSettings.MaxFileSize > 0 && fileSize > fileSettings.MaxFileSize * 1024 * 1024)
            {
                throw new Exception($"Maximum file size limit exceeded. Please upload file within {fileSettings.MaxFileSize} MB.");
            }
        }
    }
}
