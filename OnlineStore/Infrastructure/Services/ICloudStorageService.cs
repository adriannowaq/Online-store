using Microsoft.AspNetCore.Http;
using OnlineStore.Infrastructure.Services.Models;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Services
{
    public interface ICloudStorageService
    {
        Task<UploadFileModel> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string fileName);
    }
}
