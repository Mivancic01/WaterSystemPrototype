using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using componentsManager = MainSimulationManager.ComponentsManager;
using modelsManager = MainSimulationManager.ModelsManager;

public class LoadGameManager : MonoBehaviour
{
    public bool useDebug = false, isWebGLBuild = false, isTutorialScene = false;
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
        if(isTutorialScene)
        {
            MainSimulationManager.Instance.InitializeScene();
            return;
        }

        if(isWebGLBuild)
        {
            int count = 0;
            var saveFileData = PlayerPrefs.GetString("SaveFile");

            if (saveFileData.Equals("INVALID_NAME"))
            {
                MainSimulationManager.Instance.InitializeScene();
                return;
            }

            string dataLine = "";

            (dataLine, saveFileData) = FileReaderHelper.ReadSaveData(saveFileData);
            Debug.Log(dataLine);
            do
            {
                (dataLine, saveFileData) = FileReaderHelper.ReadSaveData(saveFileData);
                Debug.Log(dataLine);
                count++;

                ReadSaveFile(dataLine);
            } while (saveFileData!=null);
            MainSimulationManager.Instance.InitializeScene();
            return;
        }

        string fileName = PlayerPrefs.GetString("SaveFile", "INVALID_NAME");

        if (fileName.Equals("INVALID_NAME"))
        {
            MainSimulationManager.Instance.InitializeScene();
            return;
        }

        fileName = "SaveFiles/" + fileName + ".txt";

        System.IO.StreamReader file =
            new System.IO.StreamReader("SaveFiles/SaveFileConcept.txt");

        string line;
        while ((line = file.ReadLine()) != null)
            ReadSaveFile(line);

        file.Close();

        MainSimulationManager.Instance.InitializeScene();
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

            componentsManager.AddNodeComponent(ComponentsFactory.Instance.CreateComponentFromFile(ID, typeID, line), ID);
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

            int startNodeID = -1;
            int endNodeID = -1;

            var lineObj = ComponentsFactory.Instance.CreateComponentFromFile(ID, typeID, line);
            (startNodeID, endNodeID) = MainSimulationManager.ComponentsHelper.GetNodeIDsFromLineObject(lineObj, typeID);
            
            componentsManager.AddLineComponent(lineObj, typeID, startNodeID, endNodeID);
        }

        else if (line.StartsWith("yr"))
        {
            line = line.Remove(0, 3);

            int ID = (int)FileReaderHelper.GetNextNumber(line);
            line = line.Remove(0, FileReaderHelper.FindNextNumberIndex(line));

            var year = Int32.Parse(line.Substring(0, 4));
            line = line.Remove(0, 6);

            Model model = new Model(year);

            while (line.Length > 0)
            {
                var endOfNum = FileReaderHelper.FindEndOfNumber(line);
                var removeSize = line.Length > (endOfNum + 2) ? (endOfNum + 2) : line.Length;
                int index = Int32.Parse(line.Substring(0, endOfNum));
                model.Add(index);

                line = line.Remove(0, removeSize);
            }

            modelsManager.AddModel(model);
        }
    }
}
