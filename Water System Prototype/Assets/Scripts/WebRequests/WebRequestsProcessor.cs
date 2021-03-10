using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class WebRequestsProcessor : MonoBehaviour
{
    public void TestGetRequests()
    {

        StartCoroutine(StartGetRequest());
    }

    public void TestPostRequests()
    {

        StartCoroutine(StartPostRequest());
    }

    IEnumerator StartGetRequest()
    {
        // Get Pokemon Info

        string myUrl = "localhost:90/api/elevation?x=15.441983&y=47.0696462";
        // Example URL: https://pokeapi.co/api/v2/pokemon/151

        UnityWebRequest catchedGetRequest = UnityWebRequest.Get(myUrl);

        yield return catchedGetRequest.SendWebRequest();

        if (catchedGetRequest.isNetworkError || catchedGetRequest.isHttpError)
        {
            Debug.Log("Submitting error " + Time.time);
            Debug.LogError(catchedGetRequest.error);
            yield break;
        }

        Debug.Log("Went Through the error check " + Time.time);
        Debug.Log("Request text is: " + catchedGetRequest.downloadHandler.text);

        JSONNode jsonText = JSON.Parse(catchedGetRequest.downloadHandler.text);
        Debug.Log("Json handled text is : " + jsonText["elevation"]);
    }

    IEnumerator StartPostRequest()
    {
        // Get Pokemon Info

        string myUrl = "127.0.0.1/api/transform?path=models/Graz_reduced_14000.inp&coordinate-system=epsg:31256";
        string postMsg = "hello world :D";
        // Example URL: https://pokeapi.co/api/v2/pokemon/151

        UnityWebRequest catchedPostRequest = UnityWebRequest.Post(myUrl, postMsg);

        yield return catchedPostRequest.SendWebRequest();

        if (catchedPostRequest.isNetworkError || catchedPostRequest.isHttpError)
        {
            Debug.Log("Submitting error " + Time.time);
            Debug.LogError(catchedPostRequest.error);
            yield break;
        }

        //Debug.Log("Went Through the error check " + Time.time);
        //Debug.Log("Request text is: " + catchedPostRequest.downloadHandler.text);

        //JSONNode jsonText = JSON.Parse(catchedPostRequest.downloadHandler.text);
        //Debug.Log("Json handled text is : " + jsonText["elevation"]);
    }
}
