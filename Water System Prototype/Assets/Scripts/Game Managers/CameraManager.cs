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

    private float height, width;

    void Start()
    {
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        Debug.Log("Height = " + height + ", Width = " + width + "\n " + Time.time);

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
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        if (Input.GetMouseButtonDown(0))
            mouseOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var newPos = mouseOrigin - diference;

            /*if (transform.position.x > (mouseOrigin - diference).x)
            {
                if ((transform.position.x - width / 2) < -9.5f)
                    newPos.x = transform.position.x;
            }
            else
            {
                if ((transform.position.x + width / 2) > 9.5f)
                    newPos.x = transform.position.x;
            }

            if (transform.position.y > (mouseOrigin - diference).y)
            {
                if ((transform.position.y - height / 2) < -5f)
                    newPos.y = transform.position.y;
            }
            else
            {
                if ((transform.position.y + height / 2) > 6f)
                    newPos.y = transform.position.y;
            }*/

            transform.position = newPos;

        }
    }

    public void ChechMapType()
    {

    }

        void UpdateCameraZPos()
    {
        //var pos = Camera.main.transform.position;
        //pos.z = -10;
        //Camera.main.transform.position = pos;
    }
}
