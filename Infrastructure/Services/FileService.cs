using Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly string _path;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _configuration = configuration;
            _environment = environment;

            _path = Path.Combine(environment.WebRootPath, "files");
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
        }

        public async Task<string> AddFileAsync(IFormFile file)
        {
            if (file == null)
                return string.Empty;

            string extension = Path.GetExtension(file.FileName);
            string imageId = Guid.NewGuid().ToString();
            string relativePath = $"/{imageId}{extension}";
            string physicalPath = _path + relativePath;

            using var stream = new FileStream(physicalPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return physicalPath;
        }
    }
}