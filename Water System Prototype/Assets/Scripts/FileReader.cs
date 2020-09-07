using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    public List<Elements.BaseElement> elementList;
    public List<Elements.Model> modelList;
    public bool useDebug = false;
    public static FileReader Instance { get; private set; }

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

    void Start()
    {
        elementList = new List<Elements.BaseElement>();
        modelList = new List<Elements.Model>();

        int counter = 0;
        string line;

        // Read the file and display it line by line.  
        //System.IO.StreamReader file =
       //     new System.IO.StreamReader("Assets/SaveFiles/SaveFileConcept.txt");
        System.IO.StreamReader file =
            new System.IO.StreamReader("SaveFiles/SaveFileConcept.txt");
        while ((line = file.ReadLine()) != null)
        {
            ReadLine(line);
            counter++;
        }

        file.Close();
        Debug.Log("There were " + counter + " lines.");

        ElementsManager.Instance.LoadSaveFile(elementList, modelList);
        // Suspend the screen.  
        System.Console.ReadLine();
    }

    void ReadLine(string line)
    {
        if (line.StartsWith("#"))
            return;

        if(line.StartsWith("el"))
        {
            //remove "el "
            line = line.Remove(0, 3);

            //Get typeID
            int typeID = Int32.Parse(line.Substring(0, 1));

            line = line.Remove(0, 3);

            float x = GetNextNumber(line);
            line = line.Remove(0, FindNextNumberIndex(line));
            //  Debug.Log("X = " + x);
            //  Debug.Log("REST OF THE LINE = " + line);

            float y = GetNextNumber(line);
            line = line.Remove(0, FindNextNumberIndex(line));
            //  Debug.Log("Y = " + y);
            //  Debug.Log("REST OF THE LINE = " + line);

            float z = GetNextNumber(line);
            //  Debug.Log("Z = " + z);

            Vector3 pos = new Vector3(x, y, z);
            elementList.Add(new Elements.BaseElement(elementList.Count, typeID, pos));
        }
        else if (line.StartsWith("yr"))
        {
            line = line.Remove(0, 3);
            var year = Int32.Parse(line.Substring(0, 4));
            line = line.Remove(0, 6);

            Elements.Model model = new Elements.Model(year);

            while (line.Length > 0)
            {
                var endOfNum = FindEndOfNumber(line);
                var removeSize = line.Length > (endOfNum + 2) ? (endOfNum + 2) : line.Length;
                int index = Int32.Parse(line.Substring(0, endOfNum));
                model.Add(index);

                line = line.Remove(0, removeSize);
            }

            // model.Print();
            modelList.Add(model);
        }


        //Debug.Log(line);
    }

    float GetNextNumber(string line)
    {
        int numEnd = FindEndOfNumber(line);
        //if(numEnd < line.Length)
        //    Debug.Log("LAST CHARACTER IS: " + line.Substring(numEnd, 1));
        

        //Debug.Log("CONVERTED LINE IS: " + line.Substring(0, numEnd));
        if (line.Substring(0, 1).Equals("-"))
        {
            if (line.Substring(numEnd, 1).Equals(","))
                numEnd--;
            return (float)Convert.ToDouble(line.Substring(1, numEnd)) * -1;
        }

        return (float)Convert.ToDouble(line.Substring(0, numEnd));
    }

    int FindEndOfNumber(string line, int startIndex = 0)
    {
        int i;
        //Debug.Log("ORIGINAL LINE = " + line);
        for (i = startIndex; i < line.Length; i++)
        {
            //Debug.Log("NEXT CHAR = " + line.Substring(i, 1));
            if (line.Substring(i, 1).Equals(","))
                return i;
        }
            

        return i;
    }

    int FindNextNumberIndex(string line)
    {
        int endOfCurrentNumber = FindEndOfNumber(line);

        int i;
        //Debug.Log("ORIGINAL LINE = " + line);
        for (i = endOfCurrentNumber + 1 ; i < line.Length; i++)
        {
            //Debug.Log("NEXT CHAR = " + line.Substring(i, 1));
            if (!line.Substring(i, 1).Equals(",") && !line.Substring(i, 1).Equals(" "))
                return i;
        }

        return i;
    }

    public void SaveGame()
    {
        //string pathStart = "Assets/SaveFiles/SaveFile";
        string pathStart = "SaveFiles/SaveFile";
        string pathEnd = ".txt";
        int saveFileCounter = 1;

        string path = pathStart + saveFileCounter + pathEnd;

        while (File.Exists(path))
        {
            saveFileCounter++;
            path = pathStart + saveFileCounter + pathEnd;
        }

        Debug.Log("STARTING SAVE GAME!");
        Debug.Log(path);
        //string path = "Assets/SaveFiles/SaveFile1.txt";
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("");
                sw.WriteLine("#ELEMENTS");
                foreach (var elem in ElementsManager.Instance.elementList)
                {
                    string line = "el ";
                    line += elem.typeID + ", " + elem.position.x + ", " + elem.position.y + ", " + elem.position.z;
                    sw.WriteLine(line);

                    Debug.Log("NEW ELEMENT LINE IS: " + line);
                }

                sw.WriteLine("");
                sw.WriteLine("#TIMELINE");
                foreach (var elem in ElementsManager.Instance.modelList)
                {
                    string line = "yr ";
                    line += elem.year;
                    foreach (var index in elem.elementIndicesList)
                        line += ", " + index;
                    sw.WriteLine(line);

                    Debug.Log("NEW TIMELINE LINE IS: " + line);
                }
            }
        }
        else
            Debug.Log("FILE ALREADY EXISTS!");
    }

}
