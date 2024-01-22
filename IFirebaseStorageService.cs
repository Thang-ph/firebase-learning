using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Firebase
{
    public interface IFirebaseStorageService
    {
        Task DeleteFile(string name, Uri video);
        public Task<Uri> UploadFile(string name, IFormFile file);

    }
}
