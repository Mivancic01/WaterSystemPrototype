using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    public bool isWebGLBuild = false;

    public void LoadGame()
    {
        if(isWebGLBuild)
        {
            string btnText = GetComponentInChildren<Text>().text;
            if(btnText.Equals("New Simulation"))
                PlayerPrefs.SetString("SaveFile", "INVALID_NAME");
            else
                PlayerPrefs.SetString("SaveFile", Reader.saveGameData);

            //Debug.Log("THE PLAYERPREF IS: " + Reader.saveGameData);
            //PlayerPrefs.SetString("SaveFile", Reader.saveGameData);
            SceneManager.LoadScene("Prototype");
        }
        else
        {
            string fileName = GetComponentInChildren<Text>().text;
            if (fileName.Equals("New Simulation"))
                fileName = "INVALID_NAME";

            PlayerPrefs.SetString("SaveFile", fileName);

            SceneManager.LoadScene("Prototype");
        }
        
    }
}
