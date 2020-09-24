using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    private bool isReady = true;
    public static SaveGameManager Instance { get; private set; }

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

    public void SaveGame()
    {
        if (!isReady)
            return;

        isReady = false;

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
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("");
                sw.WriteLine("#ELEMENTS");
                foreach (var elem in ComponentsManager.Instance.componentsList)
                {
                    string line = "el ";
                    line += elem.typeID + ", " + elem.position.x + ", " + elem.position.y + ", " + elem.position.z;
                    sw.WriteLine(line);

                    Debug.Log("NEW ELEMENT LINE IS: " + line);
                }

                sw.WriteLine("");
                sw.WriteLine("#TIMELINE");
                foreach (var elem in ComponentsManager.Instance.modelList)
                {
                    string line = "yr ";
                    line += elem.year;
                    foreach (var index in elem.elementIDsList)
                        line += ", " + index;
                    sw.WriteLine(line);

                    Debug.Log("NEW TIMELINE LINE IS: " + line);
                }
            }
        }
        else
            Debug.Log("FILE ALREADY EXISTS!");

        isReady = true;
    }
}
