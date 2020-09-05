using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertiesWindow : MonoBehaviour
{
    //public GameObject windowObj;

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        DragManager.Instance.DraggableElements = true;
        DragManager.Instance.DraggableMap = true;
    }
}
