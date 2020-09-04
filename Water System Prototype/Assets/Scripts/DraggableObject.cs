using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour
{
    public bool useInitialDrag = false, useDebug = false;

    void OnMouseDrag()
    {
        Camera.main.GetComponent<CameraController>().canDrag = false;
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 10;

        transform.position = pos;
    }

    void OnMouseEnter()
    {
        if(!useInitialDrag)
        {
            if (useDebug) Debug.Log("MOUSE ENTERED GAME OBJECT!");
            Camera.main.GetComponent<CameraController>().canDrag = false;
        }
    }

    void OnMouseExit()
    {
        if (!useInitialDrag)
            if (!Input.GetMouseButton(0))
                Camera.main.GetComponent<CameraController>().canDrag = true;
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
            useInitialDrag = false;
    }
}
