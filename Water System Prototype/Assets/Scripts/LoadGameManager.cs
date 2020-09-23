using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadGameManager : MonoBehaviour
{
    public List<Elements.BaseElement> nodeComponentsList;
    public List<Elements.Model> modelList;
    public bool useDebug = false;
    public static LoadGameManager Instance { get; private set; }

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
        nodeComponentsList = new List<Elements.BaseElement>();
        modelList = new List<Elements.Model>();

        string line;

        System.IO.StreamReader file =
            new System.IO.StreamReader("SaveFiles/SaveFileConcept.txt");

        while ((line = file.ReadLine()) != null)
            ReadSaveFile(line);

        file.Close();

        ComponentsManager.Instance.InitializeScene();
        return;
    }

    void ReadSaveFile(string line)
    {
        if (line.StartsWith("#"))
            return;

        if (line.StartsWith("el"))
        {
            //remove "el "
            line = line.Remove(0, 3);

            //Get ID
            int ID = (int)FileReaderHelper.GetNextNumber(line);
            line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

            //Get typeID
            int typeID = Int32.Parse(line.Substring(0, 1));
            line = line.Remove(0, 3);

            ComponentsManager.Instance.AddComponent(ComponentsFactory.CreateComponentFromFile(ID, typeID, line));
        }

        else if(line.StartsWith("ln"))
        {
            //remove "ln "
            line = line.Remove(0, 3);

            //Get ID
            int ID = (int)FileReaderHelper.GetNextNumber(line);
            line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

            //Get typeID
            int typeID = Int32.Parse(line.Substring(0, 1));
            line = line.Remove(0, 3);

            ComponentsManager.Instance.AddComponent(ComponentsFactory.CreateComponentFromFile(ID, typeID, line));
        }

        else if (line.StartsWith("yr"))
        {
            line = line.Remove(0, 3);

            int ID = (int)FileReaderHelper.GetNextNumber(line);
            line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

            var year = Int32.Parse(line.Substring(0, 4));
            line = line.Remove(0, 6);

            Elements.Model model = new Elements.Model(year);

            while (line.Length > 0)
            {
                var endOfNum = FileReaderHelper.FindEndOfNumber(line);
                var removeSize = line.Length > (endOfNum + 2) ? (endOfNum + 2) : line.Length;
                int index = Int32.Parse(line.Substring(0, endOfNum));
                model.Add(index);

                line = line.Remove(0, removeSize);
            }

            ComponentsManager.Instance.AddModel(model);
        }
    }
}
