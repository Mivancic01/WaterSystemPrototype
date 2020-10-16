using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Zoom variables
    public float scaleFactor = 5.0f;
    private float maxFov, minFov, maxOrthSize, minOrthSize;
    private bool isMouseDown = false;

    //Drag variables
    private Vector3 originalCameraPos; // original camera position
    private Vector3 mouseOrigin; // place where mouse is first pressed
    private Vector3 diference; // change in position of mouse relative to origin

    void Start()
    {
        maxFov = Camera.main.fieldOfView;
        minFov = maxFov / scaleFactor;

        maxOrthSize = Camera.main.orthographicSize;
        minOrthSize = maxOrthSize / scaleFactor;

        originalCameraPos = Camera.main.transform.position;
    }

    void Update()
    {
        UpdateCameraZPos();
        UpdateZoomFromMouseWheel();

        if (!GameStateManager.Instance.dragMap)
            return;

        if (Input.GetKeyDown(0) && !isMouseDown)
            isMouseDown = true;

        else if (Input.GetKeyUp(0) && isMouseDown)
        {
            isMouseDown = false;
            GameStateManager.Instance.SetInactiveState();
        }

        if (GameStateManager.Instance.canDragMap && Input.GetMouseButton(0))
            UpdateDrag();
    }

    private void UpdateZoomFromMouseWheel(float strengthFactor = 1.0f)
    {
        // -------------------Zooming Out------------
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= maxFov)
                Camera.main.fieldOfView += 2 * strengthFactor;
            if (Camera.main.orthographicSize <= maxOrthSize)
                Camera.main.orthographicSize += 0.5f * strengthFactor;
        }

        // ---------------Zooming In------------------------
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > minFov)
                Camera.main.fieldOfView -= 2 * strengthFactor;
            if (Camera.main.orthographicSize >= minOrthSize)
                Camera.main.orthographicSize -= 0.5f * strengthFactor;
        }
    }

    public void UpdateZoom(bool useZoomIn)
    {
        float strengthFactor = 1.0f;

        if (useZoomIn)
        {
            if (Camera.main.fieldOfView > minFov)
                Camera.main.fieldOfView -= 2 * strengthFactor;
            if (Camera.main.orthographicSize >= minOrthSize)
                Camera.main.orthographicSize -= 0.5f * strengthFactor;
        }

        else 
        {
            if (Camera.main.fieldOfView <= maxFov)
                Camera.main.fieldOfView += 2 * strengthFactor;
            if (Camera.main.orthographicSize <= maxOrthSize)
                Camera.main.orthographicSize += 0.5f * strengthFactor;
        }
    }

    private void UpdateDrag()
    {
        if (Input.GetMouseButtonDown(0))
            mouseOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.position = mouseOrigin - diference;
        }

        if (Input.GetMouseButton(1))
            transform.position = originalCameraPos;
    }

    void UpdateCameraZPos()
    {
        var pos = Camera.main.transform.position;
        pos.z = -10;
        Camera.main.transform.position = pos;
    }
}
