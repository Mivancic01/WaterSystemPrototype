using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsFactory : MonoBehaviour
{
    public GameObject junction, pipe, pump, reservoir, tank, valve;
    public GameObject junctionWindow, pipePropWindow, pumpPropWindow, reservoirPropWindow, tankPropWindow, valvePropWindow;

    public static ComponentsFactory Instance { get; private set; }

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
    public GameObject CreateComponentFromFile(int id, int typeId, string restOfLine)
    {
        switch (typeId)
        {
            case 0:
                return CreateJunction(id, typeId, restOfLine);
            case 1:
                return CreatePipe(id, typeId, restOfLine);
            case 2:
                return CreatePump(id, typeId, restOfLine);
            case 3:
                return CreateReservoir(id, typeId, restOfLine);
            case 4:
                return CreateTank(id, typeId, restOfLine);
            case 5:
                return CreateValve(id, typeId, restOfLine);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT CLASS!");
                return null;
        }
    }

    public GameObject CreateComponent(int type, Vector3 position)
    {
        switch (type)
        {
            case 0:
                return Instantiate(junction, position, Quaternion.identity);
            case 1:
                return Instantiate(pipe, position, Quaternion.identity);
            case 2:
                return Instantiate(pump, position, Quaternion.identity);
            case 3:
                return Instantiate(reservoir, position, Quaternion.identity);
            case 4:
                return Instantiate(tank, position, Quaternion.identity);
            case 5:
                return Instantiate(valve, position, Quaternion.identity);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT!");
                return null;
        }
    }

    public GameObject CreatePropertiesWindow(int type)
    {
        switch (type)
        {
            case 0:
                return Instantiate(junctionWindow, GameObject.FindWithTag("Canvas").transform);
            case 1:
                return Instantiate(pipePropWindow, GameObject.FindWithTag("Canvas").transform);
            case 2:
                return Instantiate(pumpPropWindow, GameObject.FindWithTag("Canvas").transform);
            case 3:
                return Instantiate(reservoirPropWindow, GameObject.FindWithTag("Canvas").transform);
            case 4:
                return Instantiate(tankPropWindow, GameObject.FindWithTag("Canvas").transform);
            case 5:
                return Instantiate(valvePropWindow, GameObject.FindWithTag("Canvas").transform);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT!");
                return null;
        }
    }

    private GameObject CreateJunction(int id, int typeId, string line)
    {
        Vector3 pos = FileReaderHelper.GetPosition(line);

        float baseDemand = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float elevation = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float pressure = FileReaderHelper.GetNextNumber(line);

        var obj = Instantiate(junction, pos, Quaternion.identity);
        var junctionScript = new Junction(id, typeId, baseDemand, elevation, pressure);
        obj.GetComponent<Junction>().Init(junctionScript);

        return obj;
    }

    private GameObject CreatePipe(int id, int typeId, string line)
    {
        int startNodeID = FileReaderHelper.GetNextIntNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        int endNodeID = FileReaderHelper.GetNextIntNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float length = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float diameter = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float flow = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float flowVelocity = FileReaderHelper.GetNextNumber(line);

        var startNodePos = MainSimulationManager.ComponentsHelper.Instance.GetComponentPosition(startNodeID);
        var endNodePos = MainSimulationManager.ComponentsHelper.Instance.GetComponentPosition(endNodeID);

        var obj = LineGenerator.Instance.CreateAndReturnLineComponent(startNodePos, endNodePos, typeId);
        var pipeScript = new Pipe(id, typeId, startNodeID, endNodeID, length, diameter, flow, flowVelocity);
        obj.GetComponent<Pipe>().Init(pipeScript);

        return obj;
    }

    private GameObject CreatePump(int id, int typeId, string line)
    {
        int startNodeID = FileReaderHelper.GetNextIntNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        int endNodeID = FileReaderHelper.GetNextIntNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float flow = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float flowVelocity = FileReaderHelper.GetNextNumber(line);

        var startNodePos = MainSimulationManager.ComponentsHelper.Instance.GetComponentPosition(startNodeID);
        var endNodePos = MainSimulationManager.ComponentsHelper.Instance.GetComponentPosition(endNodeID);

        var obj = LineGenerator.Instance.CreateAndReturnLineComponent(startNodePos, endNodePos, typeId);
        var pumpScript = new Pump(id, typeId, startNodeID, endNodeID, flow, flowVelocity);
        obj.GetComponent<Pump>().Init(pumpScript);

        return obj;
    }

    private GameObject CreateReservoir(int id, int typeId, string line)
    {
        Vector3 pos = FileReaderHelper.GetPosition(line);

        float totalHead = FileReaderHelper.GetNextNumber(line);

        var obj = Instantiate(reservoir, pos, Quaternion.identity);
        var reservoirScript = new Reservoir(id, typeId, totalHead);
        obj.GetComponent<Reservoir>().Init(reservoirScript);

        return obj;
    }

    private GameObject CreateTank(int id, int typeId, string line)
    {
        Vector3 pos = FileReaderHelper.GetPosition(line);

        float volume = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float elevation = FileReaderHelper.GetNextNumber(line);

        var obj = Instantiate(tank, pos, Quaternion.identity);
        var tankScript = new Tank(id, typeId, volume, elevation);
        obj.GetComponent<Tank>().Init(tankScript);

        return obj;
    }

    private GameObject CreateValve(int id, int typeId, string line)
    {
        int startNodeID = FileReaderHelper.GetNextIntNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        int endNodeID = FileReaderHelper.GetNextIntNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float diameter = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float flow = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float flowVelocity = FileReaderHelper.GetNextNumber(line);

        var startNodePos = MainSimulationManager.ComponentsHelper.Instance.GetComponentPosition(startNodeID);
        var endNodePos = MainSimulationManager.ComponentsHelper.Instance.GetComponentPosition(endNodeID);

        var obj = LineGenerator.Instance.CreateAndReturnLineComponent(startNodePos, endNodePos, typeId);
        var valveScript = new Valve(id, typeId, startNodeID, endNodeID, diameter, flow, flowVelocity);
        obj.GetComponent<Valve>().Init(valveScript);

        return obj;
    }
}
