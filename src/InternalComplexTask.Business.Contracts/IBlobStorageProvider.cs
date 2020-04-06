using System.Threading;
using System.Threading.Tasks;

namespace InternalComplexTask.Business.Contracts
{
    public interface IBlobStorageProvider
    {
        Task<string> UploadFileAsync(string bucketName, string objectName, byte[] data, CancellationToken cancellationToken = default);
        Task<bool> TryRemoveFileAsync(string bucketName, string objectName, CancellationToken cancellationToken = default);
    }
}
