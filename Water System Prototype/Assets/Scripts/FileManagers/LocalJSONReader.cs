using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

using Amazon.S3;
using System.IO;
using UnityMinioExample;
using Amazon;


using componentsManager = MainSimulationManager.ComponentsManager;
using modelsManager = MainSimulationManager.ModelsManager;

public class LocalJSONReader : MonoBehaviour
{

    public static LocalJSONReader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // destroy the duplicate
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        string file_text = System.IO.File.ReadAllText("Graz_reduced_4000.json");
        JSONNode jsonText = JSON.Parse(file_text);

        JSONNode lv2_node = jsonText["features"]["Level_2"];

        int components_count = lv2_node.Count;

        for (int i = 0; i < components_count; i++)
        {
            string ID = lv2_node[i]["properties"]["id"];
            int typeID = GetTypeID(lv2_node[i]["properties"]["type"]);

            GameObject component = JsonComponentsFactory.Instance.CreateComponentFromJson(ID, typeID, lv2_node[i]);

            component.GetComponent<ComponentObject>().elementID = ID;

            if (typeID == 0 || typeID == 3 || typeID == 4)
                componentsManager.AddNodeComponent(component, typeID, ID);
            else
            {
                string startNodeID = "", endNodeID = "";
                (startNodeID, endNodeID) = MainSimulationManager.ComponentsHelper.GetNodeIDsFromLineObject(component, typeID);
                if(startNodeID.Equals(""))
                    print(lv2_node[i]);


                componentsManager.AddLineComponent(component, typeID, startNodeID, endNodeID, ID);
            }

        }
        */

        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        IFileStore store = new S3Store("http://localhost:9000", "access", "secret123");
        var projectDir =
            Path.GetFullPath(Path.Combine(Application.dataPath, @"Buckets"));
        var path = "Graz_reduced_4000.json";

        var fileStream = File.Create(Path.Combine(projectDir, "../../../../Minio_Files/" + path));

        store.DownloadJSON("json-bucket", path, fileStream);
    }

    public void LoadGame(string text)
    {
        JSONNode jsonText = JSON.Parse(text);

        JSONNode lv2_node = jsonText["features"]["Level_2"];

        int components_count = lv2_node.Count;

        for (int i = 0; i < components_count; i++)
        {
            string ID = lv2_node[i]["properties"]["id"];
            int typeID = GetTypeID(lv2_node[i]["properties"]["type"]);

            GameObject component = JsonComponentsFactory.Instance.CreateComponentFromJson(ID, typeID, lv2_node[i]);

            component.GetComponent<ComponentObject>().elementID = ID;

            if (typeID == 0 || typeID == 3 || typeID == 4)
                componentsManager.AddNodeComponent(component, typeID, ID);
            else
            {
                string startNodeID = "", endNodeID = "";
                (startNodeID, endNodeID) = MainSimulationManager.ComponentsHelper.GetNodeIDsFromLineObject(component, typeID);
                if (startNodeID.Equals(""))
                    print(lv2_node[i]);


                componentsManager.AddLineComponent(component, typeID, startNodeID, endNodeID, ID);
            }

        }
    }

        void StartNewGame()
    {
        MainSimulationManager.Instance.InitializeScene();
    }

    // Update is called once per frame
    int GetTypeID(string type)
    {
        switch(type)
        {
            case "Junction":
                return 0;
            case "Pipe":
                return 1;
            case "Pump":
                return 2;
            case "Reservoir":
                return 3;
            case "Tank":
                return 4;
            case "PRV":
            case "PSV":
            case "PBV":
            case "FCV":
            case "TCV":
            case "GPV":
                return 5;
            default:
                return -1;
        }
    }
}
