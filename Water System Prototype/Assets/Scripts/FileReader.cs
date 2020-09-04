using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    public struct Element
    {
        public Element(int id, Vector3 pos)
        {
            typeID = id;
            position = pos;
        }

        public int typeID;
        public Vector3 position;
    }

    public struct Model
    {
        public Model(int yr)
        {
            year = yr;
            elementIndicesList = new List<int>();
        }
        public void Add(int index)
        {
            if(elementIndicesList == null)
                elementIndicesList = new List<int>();

            elementIndicesList.Add(index);
        }

        public void Print()
        {
            Debug.Log("//////////////     PRINTING NEW MODEL FOR YEAR " + year);
            foreach (var el in elementIndicesList)
                Debug.Log("YEAR " + year + ", INDEX = " + el);
        }

        public int year;
        public List<int> elementIndicesList;
    }

    public List<Element> elementList;
    public List<Model> modelList;
    public bool useDebug = false;

    void Start()
    {
        elementList = new List<Element>();
        modelList = new List<Model>();

        int counter = 0;
        string line;

        // Read the file and display it line by line.  
        System.IO.StreamReader file =
            new System.IO.StreamReader("Assets/SaveFiles/SaveFileConcept.txt");
        while ((line = file.ReadLine()) != null)
        {
            ReadLine(line);
            counter++;
        }

        file.Close();
        Debug.Log("There were " + counter + " lines.");
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

            int x = GetNextNumber(line);
            line = line.Remove(0, FindEndOfNumber(line) + 2);
            // Debug.Log("X = " + x);
            // Debug.Log("REST OF THE LINE = " + line);

            int y = GetNextNumber(line);
            line = line.Remove(0, FindEndOfNumber(line) + 2);
            // Debug.Log("Y = " + y);
            // Debug.Log("REST OF THE LINE = " + line);

            int z = GetNextNumber(line);
            // Debug.Log("Z = " + z);

            Vector3 pos = new Vector3(x, y, z);
            elementList.Add(new Element(typeID, pos));
        }
        else if (line.StartsWith("yr"))
        {
            line = line.Remove(0, 3);
            var year = Int32.Parse(line.Substring(0, 4));
            line = line.Remove(0, 6);

            Model model = new Model(year);

            while (line.Length > 0)
            {
                var endOfNum = FindEndOfNumber(line);
                var removeSize = line.Length > (endOfNum + 2) ? (endOfNum + 2) : line.Length;
                int index = Int32.Parse(line.Substring(0, endOfNum));
                model.Add(index);

                line = line.Remove(0, removeSize);
            }

            model.Print();
            modelList.Add(model);
        }


        Debug.Log(line);
    }

    int GetNextNumber(string line)
    {
        int numEnd = FindEndOfNumber(line);
        //Debug.Log(line.Substring(0, numEnd));
        return Int32.Parse(line.Substring(0, numEnd));
    }

    int FindEndOfNumber(string line)
    {
        int i;
        //Debug.Log("ORIGINAL LINE = " + line);
        for (i = 0; i < line.Length; i++)
        {
            //Debug.Log("NEXT CHAR = " + line.Substring(i, 1));
            if (line.Substring(i, 1).Equals(","))
                return i;
        }
            

        return i;
    }

}
