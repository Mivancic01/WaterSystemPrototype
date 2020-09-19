using Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsFactory : MonoBehaviour
{
    public static ElementsFactory Instance { get; private set; }

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

    public BaseElement CreateElement(int id, int typeId, Vector3 pos, string restOfLine)
    {
        switch (typeId)
        {
            case 0:
                return CreateJunction(id, typeId, pos, restOfLine);
            case 1:
                return CreatePipe(id, typeId, pos, restOfLine);
            case 2:
                return CreatePump(id, typeId, pos, restOfLine);
            case 3:
                return CreateReservoir(id, typeId, pos, restOfLine);
            case 4:
                return CreateTank(id, typeId, pos, restOfLine);
            case 5:
                return CreateValve(id, typeId, pos, restOfLine);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT CLASS!");
                return null;
        }
    }

    private BaseElement CreateJunction(int id, int typeId, Vector3 pos, string line)
    {
        float baseDemand = FileReader.GetNextNumber(line);
        line = line.Remove(0, FileReader.FindNextNumberIndex(line));

        float elevation = FileReader.GetNextNumber(line);
        line = line.Remove(0, FileReader.FindNextNumberIndex(line));

        float pressure = FileReader.GetNextNumber(line);

        return new Junction(id, typeId, pos, baseDemand, elevation, pressure);
    }

    private BaseElement CreatePipe(int id, int typeId, Vector3 pos, string line)
    {
        float length = FileReader.GetNextNumber(line);
        line = line.Remove(0, FileReader.FindNextNumberIndex(line));

        float diameter = FileReader.GetNextNumber(line);
        line = line.Remove(0, FileReader.FindNextNumberIndex(line));

        float flow = FileReader.GetNextNumber(line);
        line = line.Remove(0, FileReader.FindNextNumberIndex(line));

        float flowVelocity = FileReader.GetNextNumber(line);

        return new Pipe(id, typeId, pos, length, diameter, flow, flowVelocity);
    }

    private BaseElement CreatePump(int id, int typeId, Vector3 pos, string line)
    {
        float flow = FileReader.GetNextNumber(line);
        line = line.Remove(0, FileReader.FindNextNumberIndex(line));

        float flowVelocity = FileReader.GetNextNumber(line);

        return new Pump(id, typeId, pos, flow, flowVelocity);
    }

    private BaseElement CreateReservoir(int id, int typeId, Vector3 pos, string line)
    {
        float totalHead = FileReader.GetNextNumber(line);

        return new Reservoir(id, typeId, pos, totalHead);
    }

    private BaseElement CreateTank(int id, int typeId, Vector3 pos, string line)
    {
        float volume = FileReader.GetNextNumber(line);
        line = line.Remove(0, FileReader.FindNextNumberIndex(line));

        float elevation = FileReader.GetNextNumber(line);

        return new Tank(id, typeId, pos, volume, elevation);
    }

    private BaseElement CreateValve(int id, int typeId, Vector3 pos, string line)
    {
        float diameter = FileReader.GetNextNumber(line);
        line = line.Remove(0, FileReader.FindNextNumberIndex(line));

        float flow = FileReader.GetNextNumber(line);
        line = line.Remove(0, FileReader.FindNextNumberIndex(line));

        float flowVelocity = FileReader.GetNextNumber(line);

        return new Valve(id, typeId, pos, diameter, flow, flowVelocity);
    }
}
