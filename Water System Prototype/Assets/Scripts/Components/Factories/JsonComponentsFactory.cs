using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class JsonComponentsFactory : MonoBehaviour
{
    public GameObject junction, pipe, pump, reservoir, tank, valve;

    public static JsonComponentsFactory Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject CreateComponentFromJson(string id, int typeId, JSONNode node)
    {
        switch (typeId)
        {
            case 0:
                return CreateJunction(id, typeId, node);
            case 1:
                return CreatePipe(id, typeId, node);
            case 2:
                return CreatePump(id, typeId, node);
            case 3:
                return CreateReservoir(id, typeId, node);
            case 4:
                return CreateTank(id, typeId, node);
            case 5:
                return CreateValve(id, typeId, node);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT CLASS!");
                return null;
        }
    }

    private GameObject CreateJunction(string id, int typeId, JSONNode node)
    {
        float longitude = node["geometry"]["coordinates"][1];
        float latitude = node["geometry"]["coordinates"][0];
        float height = node["geometry"]["coordinates"][2];

        float base_demand = node["properties"]["basedemand"];
        float pressure = 0.0f;
        float elevation = 0.0f;

        Mapbox.Unity.Map.AbstractMap absMap = GameObject.FindWithTag("AbstractMap").GetComponent<Mapbox.Unity.Map.AbstractMap>();
        var geoPos = new Mapbox.Utils.Vector2d(longitude, latitude);
        var convertedGeoPos = Mapbox.Unity.Utilities.Conversions.GeoToWorldPosition(geoPos,
                                                                 absMap.CenterMercator,
                                                                 absMap.WorldRelativeScale);

        Vector3 pos = new Vector3((float)convertedGeoPos.x, 0.0f, (float)convertedGeoPos.y);

        /**/
        var obj = Instantiate(junction, pos, Quaternion.Euler(90, 0, 0));
        var junctionScript = new Junction(id, typeId, base_demand, elevation, pressure);
        obj.GetComponent<Junction>().Init(junctionScript);

        return obj;
    }

    private GameObject CreatePipe(string id, int typeId, JSONNode node)
    {
        string start_node_id = node["properties"]["startnode"];
        string end_node_id = node["properties"]["endnode"];

        float diameter = node["properties"]["diameter"];
        float length = node["properties"]["length"];
        float flow = 0.0f;
        float flow_velocity = 0.0f;
        string status = node["properties"]["status"];

        float start_node_longitude = node["geometry"]["coordinates"][0][1];
        float start_node_latitude = node["geometry"]["coordinates"][0][0];
        float end_node_longitude = node["geometry"]["coordinates"][1][1];
        float end_node_latitude = node["geometry"]["coordinates"][1][0];

        Mapbox.Unity.Map.AbstractMap absMap = GameObject.FindWithTag("AbstractMap").GetComponent<Mapbox.Unity.Map.AbstractMap>();
        var geoPos = new Mapbox.Utils.Vector2d(start_node_longitude, start_node_latitude);
        var convertedGeoPos = Mapbox.Unity.Utilities.Conversions.GeoToWorldPosition(geoPos,
                                                                 absMap.CenterMercator,
                                                                 absMap.WorldRelativeScale);

        Vector3 startNodePos = new Vector3((float)convertedGeoPos.x, 0.0f, (float)convertedGeoPos.y);

        geoPos = new Mapbox.Utils.Vector2d(end_node_longitude, end_node_latitude);
        convertedGeoPos = Mapbox.Unity.Utilities.Conversions.GeoToWorldPosition(geoPos,
                                                                 absMap.CenterMercator,
                                                                 absMap.WorldRelativeScale);
        Vector3 endNodePos = new Vector3((float)convertedGeoPos.x, 0.0f, (float)convertedGeoPos.y);


        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetPathCreationState();

        var obj = LineGenerator.Instance.CreateAndReturnLineComponent(startNodePos, endNodePos, typeId, diameter);
        var pipeScript = new Pipe(id, typeId, start_node_id, end_node_id, length, diameter, flow, flow_velocity, 0);

        if (obj == null)
            Debug.LogError("OBJ IS NULL!");
        else if (obj.GetComponent<Pipe>() == null)
            Debug.LogError("OBJ pipe IS NULL!");
        obj.GetComponent<Pipe>().Init(pipeScript);

        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();

        return obj;
    }

    private GameObject CreatePump(string id, int typeId, JSONNode node)
    {
        string start_node_id = node["properties"]["startnode"];
        string end_node_id = node["properties"]["endnode"];

        float flow = 0.0f;
        float flow_velocity = 0.0f;
        string pump_curve = node["properties"]["status"];

        float start_node_longitude = node["geometry"]["coordinates"][0][1];
        float start_node_latitude = node["geometry"]["coordinates"][0][0];
        float end_node_longitude = node["geometry"]["coordinates"][1][1];
        float end_node_latitude = node["geometry"]["coordinates"][1][0];

        Mapbox.Unity.Map.AbstractMap absMap = GameObject.FindWithTag("AbstractMap").GetComponent<Mapbox.Unity.Map.AbstractMap>();
        var geoPos = new Mapbox.Utils.Vector2d(start_node_longitude, start_node_latitude);
        var convertedGeoPos = Mapbox.Unity.Utilities.Conversions.GeoToWorldPosition(geoPos,
                                                                 absMap.CenterMercator,
                                                                 absMap.WorldRelativeScale);

        Vector3 startNodePos = new Vector3((float)convertedGeoPos.x, 0.0f, (float)convertedGeoPos.y);

        geoPos = new Mapbox.Utils.Vector2d(end_node_longitude, end_node_latitude);
        convertedGeoPos = Mapbox.Unity.Utilities.Conversions.GeoToWorldPosition(geoPos,
                                                                 absMap.CenterMercator,
                                                                 absMap.WorldRelativeScale);
        Vector3 endNodePos = new Vector3((float)convertedGeoPos.x, 0.0f, (float)convertedGeoPos.y);


        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetPathCreationState();

        var obj = LineGenerator.Instance.CreateAndReturnLineComponent(startNodePos, endNodePos, typeId, 250);
        var pumpScript = new Pump(id, typeId, start_node_id, end_node_id, flow, flow_velocity, 0);
        obj.GetComponent<Pump>().Init(pumpScript);

        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();

        return obj;
    }

    private GameObject CreateReservoir(string id, int typeId, JSONNode node)
    {
        float longitude = node["geometry"]["coordinates"][1];
        float latitude = node["geometry"]["coordinates"][0];
        float height = node["geometry"]["coordinates"][2];

        //not right but can it be wronger?
        float total_head = node["properties"]["basedemand"];

        Mapbox.Unity.Map.AbstractMap absMap = GameObject.FindWithTag("AbstractMap").GetComponent<Mapbox.Unity.Map.AbstractMap>();
        var geoPos = new Mapbox.Utils.Vector2d(longitude, latitude);
        var convertedGeoPos = Mapbox.Unity.Utilities.Conversions.GeoToWorldPosition(geoPos,
                                                                 absMap.CenterMercator,
                                                                 absMap.WorldRelativeScale);

        Vector3 pos = new Vector3((float)convertedGeoPos.x, 0.0f, (float)convertedGeoPos.y);
        Debug.Log("Geo pos is: x = " + geoPos.x + " y = " + geoPos.y);
        Debug.Log("World pos is: x = " + pos.x + " y = " + pos.y + " z = " + pos.z);

        /**/
        var obj = Instantiate(reservoir, pos, Quaternion.Euler(90, 0, 0));
        var reservoirScript = new Reservoir(id, typeId, total_head);
        obj.GetComponent<Reservoir>().Init(reservoirScript);

        return obj;
    }

    private GameObject CreateTank(string id, int typeId, JSONNode node)
    {
        float longitude = node["geometry"]["coordinates"][1];
        float latitude = node["geometry"]["coordinates"][0];
        float height = node["geometry"]["coordinates"][2];

        //not right but can it be wronger?
        float volume = node["properties"]["basedemand"];
        float elevation = 0.0f;

        Mapbox.Unity.Map.AbstractMap absMap = GameObject.FindWithTag("AbstractMap").GetComponent<Mapbox.Unity.Map.AbstractMap>();
        var geoPos = new Mapbox.Utils.Vector2d(longitude, latitude);
        var convertedGeoPos = Mapbox.Unity.Utilities.Conversions.GeoToWorldPosition(geoPos,
                                                                 absMap.CenterMercator,
                                                                 absMap.WorldRelativeScale);

        Vector3 pos = new Vector3((float)convertedGeoPos.x, 0.0f, (float)convertedGeoPos.y);

        var obj = Instantiate(tank, pos, Quaternion.Euler(90, 0, 0));
        var tankScript = new Tank(id, typeId, volume, elevation);
        obj.GetComponent<Tank>().Init(tankScript);

        return obj;
    }

    private GameObject CreateValve(string id, int typeId, JSONNode node)
    {
        string start_node_id = node["properties"]["startnode"];
        string end_node_id = node["properties"]["endnode"];

        float diameter = node["properties"]["diameter"];
        float flow = 0.0f;
        float flow_velocity = 0.0f;
        string pump_curve = node["properties"]["status"];

        float start_node_longitude = node["geometry"]["coordinates"][0][1];
        float start_node_latitude = node["geometry"]["coordinates"][0][0];
        float end_node_longitude = node["geometry"]["coordinates"][1][1];
        float end_node_latitude = node["geometry"]["coordinates"][1][0];

        Mapbox.Unity.Map.AbstractMap absMap = GameObject.FindWithTag("AbstractMap").GetComponent<Mapbox.Unity.Map.AbstractMap>();
        var geoPos = new Mapbox.Utils.Vector2d(start_node_longitude, start_node_latitude);
        var convertedGeoPos = Mapbox.Unity.Utilities.Conversions.GeoToWorldPosition(geoPos,
                                                                 absMap.CenterMercator,
                                                                 absMap.WorldRelativeScale);

        Vector3 startNodePos = new Vector3((float)convertedGeoPos.x, 0.0f, (float)convertedGeoPos.y);

        geoPos = new Mapbox.Utils.Vector2d(end_node_longitude, end_node_latitude);
        convertedGeoPos = Mapbox.Unity.Utilities.Conversions.GeoToWorldPosition(geoPos,
                                                                 absMap.CenterMercator,
                                                                 absMap.WorldRelativeScale);
        Vector3 endNodePos = new Vector3((float)convertedGeoPos.x, 0.0f, (float)convertedGeoPos.y);


        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetPathCreationState();

        var obj = LineGenerator.Instance.CreateAndReturnLineComponent(startNodePos, endNodePos, typeId, diameter);
        var valveScript = new Valve(id, typeId, start_node_id, end_node_id, diameter, flow, flow_velocity, 0, 0);
        obj.GetComponent<Valve>().Init(valveScript);

        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();

        return obj;
    }
}
