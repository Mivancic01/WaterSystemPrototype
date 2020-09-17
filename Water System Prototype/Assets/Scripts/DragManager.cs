using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    private bool draggableMap, draggableElements, isInNodeCreateState;
    public bool DraggableMap {
        get { return draggableMap; } 
        set{ draggableMap = value;}
    }

    public bool DraggableElements {
        get { return draggableElements; }
        set { draggableElements = value; }
    }

    public bool IsInNodeCreateState
    {
        get { return isInNodeCreateState; }
        set { isInNodeCreateState = value;
            draggableElements = !value;
            draggableMap = !value;
        }
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
        isInNodeCreateState = false;
    }
}
