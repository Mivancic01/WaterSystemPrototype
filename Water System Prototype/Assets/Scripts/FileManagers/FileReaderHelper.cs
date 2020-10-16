using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileReaderHelper : MonoBehaviour
{
    public static Vector3 GetPosition(string line)
    {
        float x = GetNextNumber(line);
        line = line.Remove(0, FindNextNumberIndex(line));

        float y = GetNextNumber(line);
        line = line.Remove(0, FindNextNumberIndex(line));

        float z = GetNextNumber(line);
        line = line.Remove(0, FindNextNumberIndex(line));

        return new Vector3(x, y, z);
    }

    public static float GetNextNumber(string line)
    {
        int numEnd = FindEndOfNumber(line);

        if (line.Substring(0, 1).Equals("-"))
        {
            if (line.Substring(numEnd, 1).Equals(","))
                numEnd--;

            return (float)Convert.ToDouble(line.Substring(1, numEnd)) * -1;
        }

        return (float)Convert.ToDouble(line.Substring(0, numEnd));
    }

    public static int GetNextIntNumber(string line)
    {
        int numEnd = FindEndOfNumber(line);

        if (line.Substring(0, 1).Equals("-"))
        {
            if (line.Substring(numEnd, 1).Equals(","))
                numEnd--;

            return (int)Convert.ToDouble(line.Substring(1, numEnd)) * -1;
        }

        return (int)Convert.ToDouble(line.Substring(0, numEnd));
    }

    public static int FindEndOfNumber(string line, int startIndex = 0)
    {
        int i;
        for (i = startIndex; i < line.Length; i++)
            if (line.Substring(i, 1).Equals(","))
                return i;


        return i;
    }

    public static int FindNextNumberIndex(string line)
    {
        int endOfCurrentNumber = FindEndOfNumber(line);

        int i;
        for (i = endOfCurrentNumber + 1; i < line.Length; i++)
            if (!line.Substring(i, 1).Equals(",") && !line.Substring(i, 1).Equals(" "))
                return i;

        return i;
    }

    public static (string, string) ReadSaveData(string fileData)
    {

        if (fileData.IndexOf("\n") <= 0)
            return ("#", null);

        string line = fileData.Substring(0, fileData.IndexOf("\n"));
        string restOfData = fileData.Substring(fileData.IndexOf("\n")+1);

        return (line, restOfData);
    }
}
