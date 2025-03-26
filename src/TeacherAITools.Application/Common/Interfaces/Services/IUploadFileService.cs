using Microsoft.AspNetCore.Http;

namespace TeacherAITools.Application.Common.Interfaces.Services
{
    public interface IUploadFileService
    {
        //Task<string> UploadImage(IFormFile file);
        Task<string> CloudinaryStorage(IFormFile file);
    }
}
