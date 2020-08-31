using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Zoom variables
    public float scaleFactor = 5.0f;
    private float maxFov, minFov, maxOrthSize, minOrthSize;

    //Drag variables
    private Vector3 ResetCamera; // original camera position
    private Vector3 Origin; // place where mouse is first pressed
    private Vector3 Diference; // change in position of mouse relative to origin

    void Start()
    {
        maxFov = Camera.main.fieldOfView;
        minFov = maxFov / scaleFactor;

        maxOrthSize = Camera.main.orthographicSize;
        minOrthSize = maxOrthSize / scaleFactor;

        ResetCamera = Camera.main.transform.position;
    }

    void Update()
    {
        UpdateZoom();
        UpdateDrag();
    }

    private void UpdateZoom()
    {
        // -------------------Code for Zooming Out------------
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= maxFov)
                Camera.main.fieldOfView += 2;
            if (Camera.main.orthographicSize <= maxOrthSize)
                Camera.main.orthographicSize += 0.5f;

        }
        // ---------------Code for Zooming In------------------------
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > minFov)
                Camera.main.fieldOfView -= 2;
            if (Camera.main.orthographicSize >= minOrthSize)
                Camera.main.orthographicSize -= 0.5f;
        }
    }

    private void UpdateDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Origin = MousePos();
        }
        if (Input.GetMouseButton(0))
        {
            Diference = MousePos() - transform.position;
            transform.position = Origin - Diference;
        }
        if (Input.GetMouseButton(1)) // reset camera to original position
        {
            transform.position = ResetCamera;
        }
    }
    // return the position of the mouse in world coordinates (helper method)
    Vector3 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
