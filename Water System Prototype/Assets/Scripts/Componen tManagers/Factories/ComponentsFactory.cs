using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elements;

public class ComponentsFactory : MonoBehaviour
{
    public static BaseElement CreateComponentFromFile(int id, int typeId, string restOfLine)
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

    public static BaseElement CreateNodeComponent(int id, int typeId, Vector3 pos)
    {
        switch (typeId)
        {
            case 0:
                return new Junction(id, typeId, pos);
            case 3:
                return new Reservoir(id, typeId, pos);
            case 4:
                return new Tank(id, typeId, pos);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT CLASS!");
                return null;
        }
    }

    public static BaseElement CreateLineComponent(int id, int typeId, int startNodeID, int endNodeID)
    {
        switch (typeId)
        {
            case 1:
                return new Pipe(id, typeId, startNodeID, endNodeID);
            case 2:
                return new Pump(id, typeId, startNodeID, endNodeID);
            case 5:
                return new Valve(id, typeId, startNodeID, endNodeID);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT CLASS!");
                return null;
        }
    }

    private static BaseElement CreateJunction(int id, int typeId, string line)
    {
        Vector3 pos = FileReaderHelper.GetPosition(line);

        float baseDemand = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float elevation = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float pressure = FileReaderHelper.GetNextNumber(line);

        return new Junction(id, typeId, pos, baseDemand, elevation, pressure);
    }

    private static BaseElement CreatePipe(int id, int typeId, string line)
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

        return new Pipe(id, typeId, startNodeID, endNodeID, length, diameter, flow, flowVelocity);
    }

    private static BaseElement CreatePump(int id, int typeId, string line)
    {
        int startNodeID = FileReaderHelper.GetNextIntNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        int endNodeID = FileReaderHelper.GetNextIntNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float flow = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float flowVelocity = FileReaderHelper.GetNextNumber(line);

        return new Pump(id, typeId, startNodeID, endNodeID, flow, flowVelocity);
    }

    private static BaseElement CreateReservoir(int id, int typeId, string line)
    {
        Vector3 pos = FileReaderHelper.GetPosition(line);

        float totalHead = FileReaderHelper.GetNextNumber(line);

        return new Reservoir(id, typeId, pos, totalHead);
    }

    private static BaseElement CreateTank(int id, int typeId, string line)
    {
        Vector3 pos = FileReaderHelper.GetPosition(line);

        float volume = FileReaderHelper.GetNextNumber(line);
        line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

        float elevation = FileReaderHelper.GetNextNumber(line);

        return new Tank(id, typeId, pos, volume, elevation);
    }

    private static BaseElement CreateValve(int id, int typeId, string line)
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

        return new Valve(id, typeId, startNodeID, endNodeID, diameter, flow, flowVelocity);
    }
}
