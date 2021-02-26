using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon.S3;
using System.IO;

using UnityMinioExample;
using Amazon;

public class StorageScript : MonoBehaviour
{
    private void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        Debug.Log("Start obj");
    }

    public async void InitiateTest()
    {
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        IFileStore store = new S3Store("http://localhost:9000", "access", "secret123");
        var projectDir =
            Path.GetFullPath(Path.Combine(Application.dataPath, @"Buckets"));
        Stream fs = File.OpenRead(Path.Combine(projectDir, "examples/Unknown.jpg"));
        var bucket = System.Guid.NewGuid().ToString();
        var path = System.Guid.NewGuid() + ".jpg";
        await store.CreateBucket(bucket);
        await store.Upload(fs, bucket, path);
        var fileStream = File.Create(Path.Combine(projectDir, path));

        await store.Download(bucket, path, fileStream);
    }
}
