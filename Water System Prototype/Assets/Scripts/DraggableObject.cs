using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour
{
    public bool useInitialDrag = false, useDebug = false;
    public int elementsListIndex;

    private float mouseDownTime;

    void OnMouseDrag()
    {
        if(DragManager.Instance.DraggableElements)
        {
            DragManager.Instance.DraggableMap = false;
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 10;

            transform.position = pos;
        }
        
    }

    void OnMouseEnter()
    {
        if(!useInitialDrag)
        {
            //if (useDebug) Debug.Log("MOUSE ENTERED GAME OBJECT!");
            DragManager.Instance.DraggableMap = false;
        }
    }

    void OnMouseExit()
    {
        if (!useInitialDrag)
            if (!Input.GetMouseButton(0) && DragManager.Instance.DraggableElements)
                DragManager.Instance.DraggableMap = true;
    }

    void OnMouseUp()
    {
        if (!DragManager.Instance.DraggableElements)
            return;

        if((Time.time - mouseDownTime) < 0.5f)
        {
            Debug.Log("CLICKED ON GAME OBJECT!");
            ElementsManager.Instance.OpenPropertiesWindow(elementsListIndex);
            DragManager.Instance.DraggableElements = false;
            DragManager.Instance.DraggableMap = false;
        }
    }
    
    void OnMouseDown()
    {
        mouseDownTime = Time.time;
    }

    void Update()
    {
        if (useInitialDrag)
            UpdateDragPosition();
    }

    void UpdateDragPosition()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 10;

        if (Input.GetMouseButton(0))
            transform.position = pos;
        else
        {
            useInitialDrag = false;
        }
    }
}
