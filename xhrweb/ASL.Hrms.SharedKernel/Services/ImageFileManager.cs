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
    public class ImageFileManager : IImageFileManager
    {
        public string UploadImageFile(IFormFile imageFile, string filePath, ImageFileSettings imageFileSettings = null)
        {
            var result = string.Empty;
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (imageFile.Length > 0)
                {
                    ValidateFile(imageFile, imageFileSettings);

                    var fileName = $"ASL_{Utility.GenerateUniqueKey(10)}{Path.GetExtension(imageFile.FileName).ToLower()}";
                    var fullPath = Path.Combine(filePath, $"{fileName}");

                    using var image = Image.Load(imageFile.OpenReadStream());

                    if (imageFileSettings != null && imageFileSettings.ApplyResize)
                    {
                        image.Mutate(x => x.Resize(imageFileSettings.Width, imageFileSettings.Height));
                    }
                    image.Save(fullPath);

                    result = $"{fileName}";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private void ValidateFile(IFormFile file, ImageFileSettings imageFileSettings)
        {
            if (imageFileSettings == null) return;

            var extension = Path.GetExtension(file.FileName).ToLower();

            var validExtensions = imageFileSettings?.ValidExtensions.ToList().Select(s => s.Trim()).ToList();


            if (validExtensions.Count() > 0 && validExtensions.Contains(extension) == false)
            {
                throw new Exception($"File extension {extension} is not valid. Please upload a file in {string.Join(", ", imageFileSettings.ValidExtensions)} format.");
            }

            var fileSize = file.Length;

            if(fileSize == 0)
            {
                throw new Exception("Empty file is not supported");
            }

            if (imageFileSettings.MaxFileSize > 0 && fileSize > imageFileSettings.MaxFileSize * 1024 * 1024)
            {
                throw new Exception($"Maximum file size limit exceeded. Please upload file within {imageFileSettings.MaxFileSize} MB.");
            }
        }
    }
}
