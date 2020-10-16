using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Reader : MonoBehaviour
{
    public GameObject loadGamePanel;
    public Lean.Gui.LeanButton buttonPrefab;
    public bool isWebGlBuild = false;

    public static string saveGameData = "";

    // Start is called before the first frame update
    void Start()
    {
        if (isWebGlBuild)
            StartCoroutine(textLoad());
        else
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


    IEnumerator textLoad()
    {
        bool successful = true;
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://localhost:9000/tounity.php", form);
        yield return www;

        if (www.error != null)
            successful = false;
        else
        {
            saveGameData = www.text;

            var btnObj = Instantiate(buttonPrefab, loadGamePanel.transform);
            btnObj.GetComponentInChildren<Text>().text = "Save1";

            Debug.Log(www.text);
            successful = true;
        }
    }
}
