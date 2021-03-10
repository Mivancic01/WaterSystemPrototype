using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using UnityEngine;

namespace UnityMinioExample
{
    public class S3SnycStore : MonoBehaviour
    {
        private readonly IAmazonS3 _s3Client;

        public S3SnycStore(string serviceUrl, string accessKey, string secretKey)
        {
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                ServiceURL = serviceUrl,
                ForcePathStyle = true
            };
            _s3Client = new AmazonS3Client(accessKey, secretKey, config);
        }

        public void CreateBucket(string name)
        {
            var putBucketRequest = new PutBucketRequest()
            {
                BucketName = name

            };
            _s3Client.PutBucketAsync(putBucketRequest, null);
        }


        public void Upload(Stream bodyStream, string bucket, string path)
        {
            var putObjectRequest = new PutObjectRequest
            {
                Key = path,
                BucketName = bucket,
                InputStream = bodyStream,
                Headers = { ContentLength = bodyStream.Length }
            };

            _s3Client.PutObjectAsync(putObjectRequest, null);
        }
        public void Download(string bucket, string path, System.IO.FileStream fileStream)
        {
            var getObjectRequest = new GetObjectRequest()
            {
                Key = path,
                BucketName = bucket,
            };

            _s3Client.GetObjectAsync(getObjectRequest, (responseObj) =>
            {
                var response = responseObj.Response;

                Debug.Log("Checkpoint download");

                if (responseObj.Exception != null)
                    Debug.LogError("ResponseObj is wacked: " + responseObj.Exception.ToString());

                Debug.Log(responseObj.state);

                Debug.Log("Checkpoint download 2");

                if (response.ResponseStream != null)
                {
                    var stream = response.ResponseStream;
                    stream.CopyToAsync(fileStream);
                }
                else
                    Debug.Log("ResponseStream is null");

                Debug.Log("Checkpoint end download");
            });
        }

        private Amazon.Runtime.AmazonServiceCallback<PutBucketRequest, PutBucketResponse> putBucketCallback()
        {
            Debug.LogError("CALLED PUT BUCKET CALLBACK!");
            return null;
        }
    }

}