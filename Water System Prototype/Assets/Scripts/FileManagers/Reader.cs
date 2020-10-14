using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Reader : MonoBehaviour
{
    public GameObject loadGamePanel;
    public Lean.Gui.LeanButton buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var fileEntries = Directory.GetFiles("SaveFiles/");
        foreach (var file in fileEntries)
        {
            if (!file.EndsWith(".txt"))
                continue;

            string saveGameName = file.Remove(file.LastIndexOf(".")).Substring(10);

            var btnObj = Instantiate(buttonPrefab, loadGamePanel.transform);
            btnObj.GetComponentInChildren<Text>().text = saveGameName;


            if (file.EndsWith(".txt"))
                Debug.Log("NEW FILE: " + file.Remove(file.LastIndexOf(".")).Substring(10));
        }
    }
}
