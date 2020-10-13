using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    public void LoadGame()
    {
        string fileName = GetComponentInChildren<Text>().text;
        if (fileName.Equals("New Simulation"))
            fileName = "INVALID_NAME";

        PlayerPrefs.SetString("SaveFile", fileName);

        SceneManager.LoadScene("Prototype");
    }
}
