using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    private bool draggableMap, draggableElements;
    public bool DraggableMap {
        get { return draggableMap; } 
        set{ draggableMap = value;}
    }
    public bool DraggableElements {
        get { return draggableElements; }
        set { draggableElements = value; }
    }
    public static DragManager Instance { get; private set; }

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

        draggableMap = true;
        draggableElements = true;
    }
}
