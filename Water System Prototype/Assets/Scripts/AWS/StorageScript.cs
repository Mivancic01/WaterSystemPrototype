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

    public void StartCoroutineTest()
    {
        Debug.Log("StartCoroutineTest()");
        StartCoroutine("CoroutineFunk");
    }

    public async void InitiateTest()
    {
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        IFileStore store = new S3Store("http://localhost:9000", "access", "secret123");
        var projectDir =
            Path.GetFullPath(Path.Combine(Application.dataPath, @"Buckets"));
        Stream fs = File.OpenRead(Path.Combine(projectDir, "examples/Unknown.jpg"));
        var bucket = System.Guid.NewGuid().ToString();
        //var bucket = "Marko-Bucket";
        var path = System.Guid.NewGuid() + ".jpg";
        await store.CreateBucket(bucket);
        Debug.Log("Checkpoint 1");

        await store.Upload(fs, bucket, path);
        Debug.Log("Checkpoint 2");

        var fileStream = File.Create(Path.Combine(projectDir, "../../../../Minio_Files/" + path));

        await store.Download(bucket, path, fileStream);
        Debug.Log("Checkpoint 3");
    }

    public async void TestDownload()
    {
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        IFileStore store = new S3Store("http://localhost:9000", "access", "secret123");
        var projectDir =
            Path.GetFullPath(Path.Combine(Application.dataPath, @"Buckets"));
        var path = "Graz_reduced_4000.json";

        var fileStream = File.Create(Path.Combine(projectDir, "../../../../Minio_Files/" + path));

        await store.Download("json-bucket", path, fileStream);
        Debug.Log("Checkpoint 3");
    }

    public IEnumerator CoroutineFunk()
    {
        Debug.Log("Starting Coroutine");

        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        var store = new S3SnycStore("http://localhost:9000", "access", "secret123");
        var projectDir = Path.GetFullPath(Path.Combine(Application.dataPath, @"Buckets"));
        Stream fs = File.OpenRead(Path.Combine(projectDir, "examples/Unknown.jpg"));
        var bucket = System.Guid.NewGuid().ToString();
        //var bucket = "Marko-Bucket";
        var path = System.Guid.NewGuid() + ".jpg";
        store.CreateBucket(bucket);
        Debug.Log("Checkpoint 1");

        store.Upload(fs, bucket, path);
        Debug.Log("Checkpoint 2");

        var fileStream = File.Create(Path.Combine(projectDir, "../../../../Minio_Files/" + path));

        store.Download(bucket, path, fileStream);
        Debug.Log("Checkpoint 3");

        yield return null;
    }

    public async void TestObjectListing()
    {
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        IFileStore store = new S3Store("http://localhost:9000", "access", "secret123");
        var projectDir =
            Path.GetFullPath(Path.Combine(Application.dataPath, @"Buckets"));
        var path = "Graz_reduced_4000.json";

        var fileStream = File.Create(Path.Combine(projectDir, "../../../../Minio_Files/" + path));

        store.ListObjects("upload");
        Debug.Log("Checkpoint 3");
    }
}
