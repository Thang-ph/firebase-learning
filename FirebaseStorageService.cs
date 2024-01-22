using Google.Cloud.Storage.V1;

namespace Firebase
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly StorageClient _storageClient;
        private const string BucketName = "studportal-4fbba.appspot.com";

        public FirebaseStorageService(StorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<Uri> UploadFile(string name, IFormFile file)
        {
            using var stream = new MemoryStream();  
                    var randomGuid = Guid.NewGuid();
            await file.CopyToAsync(stream);

            var blob = await _storageClient.UploadObjectAsync(BucketName,
                $"{name}-{randomGuid}", file.ContentType, stream);
            var videoUri = new Uri(blob.MediaLink);

            return videoUri;
               
        }
        public async Task DeleteFile(string name, Uri videoUri)
        {
            var objectName = videoUri.Segments.Last();

         
            await _storageClient.DeleteObjectAsync(BucketName, objectName);

        }

      

    }
}
