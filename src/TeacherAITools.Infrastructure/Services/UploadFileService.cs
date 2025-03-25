﻿using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TeacherAITools.Application.Common.Interfaces.Services;

namespace TeacherAITools.Infrastructure.Services
{
    public class UploadFileService(
        IConfiguration configuration) : IUploadFileService
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<string> UploadImage(IFormFile file)
        {
            var _apiKey = _configuration["Firebase:ApiKey"];
            var _storage = _configuration["Firebase:Storage"];

            var storage = new FirebaseStorage(_storage);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            string folderName;
            var fileExtension = Path.GetExtension(file.FileName);

            switch (fileExtension)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                    folderName = "images";
                    break;
                case ".docx":
                    folderName = "docx";
                    break;
                case ".ppt":
                case ".pptx":
                    folderName = "ppt";
                    break;
                case ".mp4":
                case ".mov":
                    folderName = "videos";
                    break;
                default:
                    folderName = "other";
                    break;
            }

            using var stream = file.OpenReadStream();
            var storageReference = storage.Child(folderName).Child(fileName);
            await storageReference.PutAsync(stream);
            return await storageReference.GetDownloadUrlAsync();
        }
    }
}
