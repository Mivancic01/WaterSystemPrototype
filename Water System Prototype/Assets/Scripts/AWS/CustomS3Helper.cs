using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using UnityEngine;

namespace UnityMinioExample
{
    public class S3Store : MonoBehaviour, IFileStore
    {
        private readonly IAmazonS3 _s3Client;

        public S3Store(string serviceUrl, string accessKey, string secretKey)
        {
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                ServiceURL = serviceUrl,
                ForcePathStyle = true
            };
            _s3Client = new AmazonS3Client(accessKey, secretKey, config);
        }

        public async Task CreateBucket(string name)
        {
            var putBucketRequest = new PutBucketRequest()
            {
                BucketName = name
                
            };
            _s3Client.PutBucketAsync(putBucketRequest, null);
        }


        public async Task Upload(Stream bodyStream, string bucket, string path)
        {
            var putObjectRequest = new PutObjectRequest
            {
                Key = path,
                BucketName = bucket,
                InputStream = bodyStream,
                Headers = {ContentLength = bodyStream.Length}
            };

            _s3Client.PutObjectAsync(putObjectRequest, null);
        }
        public async Task Download(string bucket, string path, System.IO.FileStream fileStream)
        {
            var getObjectRequest = new GetObjectRequest()
            {
                Key = path,
                BucketName = bucket,
            };

            _s3Client.GetObjectAsync(getObjectRequest, (responseObj) =>
            {
                string data = null;
                var response = responseObj.Response;

                if(response.ResponseStream != null)
                {
                    var stream = response.ResponseStream;
                    stream.CopyToAsync(fileStream);
                }
            });
        }

        private Amazon.Runtime.AmazonServiceCallback<PutBucketRequest, PutBucketResponse> putBucketCallback()
        {
            Debug.LogError("CALLED PUT BUCKET CALLBACK!");
            return null;
        }
    }

}