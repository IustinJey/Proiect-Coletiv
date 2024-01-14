using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skillz_backend.Services.Interfaces
{
    public interface IGoogleCloudStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }

}