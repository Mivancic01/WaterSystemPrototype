using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsFactory : MonoBehaviour
{
    public GameObject junction, pipe, pump, reservoir, tank, valve;

    public GameObject junctionWindow, pipePropWindow, pumpPropWindow, reservoirPropWindow, tankPropWindow, valvePropWindow;
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
                return Instantiate(junction, position, Quaternion.identity);
            case 1:
                return Instantiate(pipe, position, Quaternion.identity);
            case 2:
                return Instantiate(pump, position, Quaternion.identity);
            case 3:
                return Instantiate(reservoir, position, Quaternion.identity);
            case 4:
                return Instantiate(tank, position, Quaternion.identity);
            case 5:
                return Instantiate(valve, position, Quaternion.identity);
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
                return Instantiate(junctionWindow, GameObject.FindWithTag("Canvas").transform);
            case 1:
                return Instantiate(pipePropWindow, GameObject.FindWithTag("Canvas").transform);
            case 2:
                return Instantiate(pumpPropWindow, GameObject.FindWithTag("Canvas").transform);
            case 3:
                return Instantiate(reservoirPropWindow, GameObject.FindWithTag("Canvas").transform);
            case 4:
                return Instantiate(tankPropWindow, GameObject.FindWithTag("Canvas").transform);
            case 5:
                return Instantiate(valvePropWindow, GameObject.FindWithTag("Canvas").transform);
            default:
                Debug.LogError("TRYING TO CREATE A NON EXISTENT ELEMENT!");
                return null;
        }
    }
}
