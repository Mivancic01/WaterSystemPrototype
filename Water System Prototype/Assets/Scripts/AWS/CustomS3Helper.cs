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
                var response = responseObj.Response;

                Debug.Log("Checkpoint download");

                if (responseObj.Exception != null)
                    Debug.LogError("ResponseObj is wacked: " + responseObj.Exception.ToString());

                Debug.Log(responseObj.state);

                Debug.Log("Checkpoint download 2");

                if (response.ResponseStream != null)
                {
                    var stream = response.ResponseStream;

                    StreamReader reader = new StreamReader(stream);
                    string text = reader.ReadToEnd();

                    Debug.Log("The stream is: ");
                    Debug.Log(text);
                    //stream.CopyToAsync(fileStream);
                }
                else
                    Debug.Log("ResponseStream is null");

                Debug.Log("Checkpoint end download");
            });
        }

        public void DownloadJSON(string bucket, string path, System.IO.FileStream fileStream)
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

                    StreamReader reader = new StreamReader(stream);
                    string text = reader.ReadToEnd();

                    Debug.Log("The stream is: ");
                    Debug.Log(text);
                    LocalJSONReader.Instance.LoadGame(text);
                }
                else
                    Debug.Log("ResponseStream is null");

                Debug.Log("Checkpoint end download");
            });
        }

        public void ListObjects(string bucket)
        {
            var listObjectRequest = new ListObjectsRequest()
            {
                BucketName = bucket,
            };

            _s3Client.ListObjectsAsync(listObjectRequest, (responseObject) =>
            {
                string result_text = "";
                if (responseObject.Exception == null)
                {
                    result_text += "Got Response \nPrinting now \n";
                    responseObject.Response.S3Objects.ForEach((o) =>
                    {
                        result_text += string.Format("{0}\n", o.Key);
                    });

                    Debug.Log(result_text);
                }
                else
                {
                    result_text += "Got Exception \n";

                    Debug.LogError(result_text);
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