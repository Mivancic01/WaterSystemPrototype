using System.IO;
using System.Threading.Tasks;
using Amazon.S3.Model;

namespace UnityMinioExample
{
    public interface IFileStore
    {
        Task CreateBucket(string name);
        Task Upload(Stream bodyStream, string bucket, string path);
        Task Download(string bucket, string path, System.IO.FileStream fileStream);
        void DownloadJSON(string bucket, string path, System.IO.FileStream fileStream);
        void ListObjects(string bucket);
    }
}