using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> AddFileAsync(IFormFile file);
    }
}