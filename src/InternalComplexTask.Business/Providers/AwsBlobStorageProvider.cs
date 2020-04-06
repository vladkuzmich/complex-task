using System.IO;
using System.Threading;
using System.Threading.Tasks;
using InternalComplexTask.Business.Contracts;
using InternalComplexTask.Business.Extensions;
using Microsoft.Extensions.Logging;
using Minio;

namespace InternalComplexTask.Business.Providers
{
    public class AwsBlobStorageProvider : IBlobStorageProvider
    {
        private const string BaseUrl = "https://{0}.s3.us-east-2.amazonaws.com/{1}";

        private readonly ILogger<AwsBlobStorageProvider> _logger;
        private readonly MinioClient _minioClient;

        public AwsBlobStorageProvider(ILogger<AwsBlobStorageProvider> logger, MinioClient minioClient)
        {
            _logger = logger;
            _minioClient = minioClient;
        }

        public async Task<string> UploadFileAsync(string bucketName, string objectName, byte[] data, CancellationToken cancellationToken = default)
        {
            var isBucketExist = await _minioClient.BucketExistsAsync(bucketName, cancellationToken);

            if (!isBucketExist)
            {
                _logger.LogWarning($"Blob storage with name: {bucketName} doesn't exist");

                return null;
            }

            using var memoryStream = new MemoryStream(data);

            await _minioClient.PutObjectAsync(
                bucketName: bucketName,
                objectName: objectName.ToJpeg(),
                data: memoryStream,
                size: memoryStream.Length,
                contentType: "application/octet-stream",
                cancellationToken: cancellationToken);

            return string.Format(BaseUrl, bucketName, objectName.ToJpeg());
        }

        public async Task<bool> TryRemoveFileAsync(string bucketName, string objectName, CancellationToken cancellationToken = default)
        {
            var isBucketExist = await _minioClient.BucketExistsAsync(bucketName, cancellationToken);

            if (!isBucketExist)
            {
                _logger.LogWarning($"Blob storage with name: {bucketName} doesn't exist");

                return false;
            }

            await _minioClient.RemoveObjectAsync(bucketName, objectName.ToJpeg(), cancellationToken);

            return true;
        }
    }
}
