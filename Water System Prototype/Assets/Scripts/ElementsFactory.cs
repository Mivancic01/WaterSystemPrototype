using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsFactory : MonoBehaviour
{
    public GameObject element1, element2, element3, element4, element5, element6;

    public GameObject element1PropWindow, element2PropWindow, element3PropWindow, element4PropWindow, element5PropWindow, element6PropWindow;
    public static ElementsFactory Instance { get; private set; }

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

    public GameObject CreateElement(int type, Vector3 position)
    {
        switch(type)
        {
            case 0:
                return Instantiate(element1, position, Quaternion.identity);
            case 1:
                return Instantiate(element2, position, Quaternion.identity);
            case 2:
                return Instantiate(element3, position, Quaternion.identity);
            case 3:
                return Instantiate(element4, position, Quaternion.identity);
            case 4:
                return Instantiate(element5, position, Quaternion.identity);
            case 5:
                return Instantiate(element6, position, Quaternion.identity);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT!");
                return null;
        }
    }

    public GameObject CreatePropertiesWindow(int type)
    {
        switch (type)
        {
            case 0:
                return Instantiate(element1PropWindow, GameObject.FindWithTag("Canvas").transform);
            case 1:
                return Instantiate(element1PropWindow, GameObject.FindWithTag("Canvas").transform);
            case 2:
                return Instantiate(element1PropWindow, GameObject.FindWithTag("Canvas").transform);
            case 3:
                return Instantiate(element1PropWindow, GameObject.FindWithTag("Canvas").transform);
            case 4:
                return Instantiate(element1PropWindow, GameObject.FindWithTag("Canvas").transform);
            case 5:
                return Instantiate(element1PropWindow, GameObject.FindWithTag("Canvas").transform);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT!");
                return null;
        }
    }
}
