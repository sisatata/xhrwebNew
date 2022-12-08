using ASL.Utility.FileManager.Interfaces;
using ASL.Utility.FileManager.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Linq;

namespace ASL.Utility.FileManager.Services
{
    public class AttachedFileManager : IAttachedFileManager
    {
        public string UploadAttachedFile(IFormFile attachedFile, string filePath, AttachedFileSettings attachedFileSettings = null)
        {
            var result = string.Empty;
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (attachedFile.Length > 0)
                {
                    ValidateFile(attachedFile, attachedFileSettings);

                    var fileName = $"ASL_{Utility.GenerateUniqueKey(10)}{Path.GetExtension(attachedFile.FileName).ToLower()}";
                    var fullPath = Path.Combine(filePath, $"{fileName}");

                    //using var image = Image.Load(imageFile.OpenReadStream());

                    //if (attachedFileSettings != null && attachedFileSettings.ApplyResize)
                    //{
                    //    image.Mutate(x => x.Resize(imageFileSettings.Width, imageFileSettings.Height));
                    //}
                    //image.Save(fullPath);

                    attachedFile.CopyTo(new FileStream(fullPath, FileMode.Create));

                    result = $"{fileName}";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private void ValidateFile(IFormFile file, AttachedFileSettings attachedFileSettings)
        {
            if (attachedFileSettings == null) return;

            var extension = Path.GetExtension(file.FileName).ToLower();

            var validExtensions = attachedFileSettings?.ValidExtensions.ToList().Select(s => s.Trim()).ToList();


            if (validExtensions.Count() > 0 && validExtensions.Contains(extension) == false)
            {
                throw new Exception($"File extension {extension} is not valid. Please upload a file in {string.Join(", ", attachedFileSettings.ValidExtensions)} format.");
            }

            var fileSize = file.Length;

            if (attachedFileSettings.MaxFileSize > 0 && fileSize > attachedFileSettings.MaxFileSize * 1024 * 1024)
            {
                throw new Exception($"Maximum file size limit exceeded. Please upload file within {attachedFileSettings.MaxFileSize} MB.");
            }
        }
    }
}
