using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public GameObject pipePrefab, pumpPrefab, valvePrefab;
    public static int nodeType = 0;

    private bool hasCreatedStart = false, hasCreatedEnd = false, canStart = false, isMouseDown = true;
    private int timesDownPressed = 0;
    private Vector3 startPosition, startRotation;
    private float oldZAngle = 0f;
    private GameObject node;
    bool isSpaceDown = false;

    public static NodeGenerator Instance { get; private set; }

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

    public void EnterNodeCreationMode()
    {
        DragManager.Instance.IsInNodeCreateState = true;
        /*
        DragManager.Instance.DraggableElements = false;
        DragManager.Instance.DraggableMap = false;
        StartCoroutine("CreateNode");
        */
    }

    public void EnterNodeCreationMode(Vector3 startPos)
    {
        if (!hasCreatedStart)
            hasCreatedStart = true;
        else
        {
            hasCreatedStart = false;
            StopCoroutine("CreateNodeFromObjectPos");
            DragManager.Instance.IsInNodeCreateState = false;
            return;
        }    
        startPosition = startPos;
        InstantianteNode(startPos);
        startRotation = node.transform.rotation.eulerAngles;

        StartCoroutine("CreateNodeFromObjectPos");
    }

    IEnumerator CreateNode()
    {
        while(!hasCreatedEnd)
        {
            UpdateStates();

            //wait for player to bring mouse button up after clicking the button
            if (!canStart)
                yield return null;

            //Selected a endPosition. Node creation is done
            if (timesDownPressed == 2)
            {
                DragManager.Instance.DraggableElements = true;
                DragManager.Instance.DraggableMap = true;
                Debug.Log("EXITING COROUTINE");
                canStart = false;
                hasCreatedStart = false;
                isMouseDown = true;
                yield break;
            }

            //Selected a startPosition. Create node 
            if (timesDownPressed == 1 && !hasCreatedStart)
            {
                Debug.Log("SELECTED A START POSITION!");

                hasCreatedStart = true;

                InstantianteNode();

                startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPosition.x -= node.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 10;
                node.transform.position = pos;
                startRotation = node.transform.rotation.eulerAngles;
            }

            if(node != null)
                UpdateTransform();

            yield return null;
        }
    }

    IEnumerator CreateNodeFromObjectPos()
    {
        while (!hasCreatedEnd)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if(node != null)
                    GameObject.Destroy(node);

                DragManager.Instance.IsInNodeCreateState = false;
                yield break;
            }

            UpdateTransform();

            yield return null;
        }
    }

    private void InstantianteNode()
    {
        switch(nodeType)
        {
            case 0:
                node = node = Instantiate(pipePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                return;
            case 1:
                node = node = Instantiate(pumpPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                return;
            case 2:
                node = node = Instantiate(valvePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                return;
            default:
                Debug.LogError("TRYING TO INSTANTIATE INVALID TYPE OF NODE!");
                return;
        }
    }

    private void InstantianteNode(Vector3 pos)
    {
        switch (nodeType)
        {
            case 0:
                node = Instantiate(pipePrefab, pos, Quaternion.identity);
                return;
            case 1:
                node = Instantiate(pumpPrefab, pos, Quaternion.identity);
                return;
            case 2:
                node = Instantiate(valvePrefab, pos, Quaternion.identity);
                return;
            default:
                Debug.LogError("TRYING TO INSTANTIATE INVALID TYPE OF NODE!");
                return;
        }
    }

    private void UpdateStates()
    {
        if (Input.GetMouseButtonDown(0) && !isMouseDown)
        {
            Debug.Log("PRESSED MOUSE BUTTON!");
            isMouseDown = true;
            timesDownPressed++;
        }

        else if (Input.GetMouseButtonUp(0) && isMouseDown)
        {
            Debug.Log("RELEASED MOUSE BUTTON!");
            isMouseDown = false;
            canStart = true; 
        }

        Debug.Log("canStart = " + canStart + "\n" +
            "isMouseDown = " + isMouseDown + "\n" +
            "hasCreatedStart = " + hasCreatedStart + "\n" +
            "timesDownPressed = " + timesDownPressed);
    }

    private void UpdateTransform()
    {
        float oldWidth = node.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        var newWidth = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x;

        //Debug.Log("OldWidth = " + oldWidth);
        //Debug.Log("newWidht = " + newWidth);
        //Debug.Log("X scale = " + newWidth / oldWidth);

        if (newWidth == 0)
            newWidth = oldWidth;

       // Debug.Log("Mouse Pos.x = " + Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
       // Debug.Log("Mouse Pos.y = " + Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        UpdateScale(oldWidth, newWidth);
        //UpdatePosition(oldWidth, newWidth);
        UpdateRotation(oldWidth, newWidth);


    }

    private void UpdateScale(float oldWidth, float newWidth)
    {
        Vector3 newScale = node.transform.localScale;
        newScale.x *= newWidth / oldWidth;
        node.transform.localScale = newScale;
    }

    private void UpdateRotation(float oldWidth, float newWidth)
    {
        var height = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - startPosition.y;
        var width = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x);
        //var zAngle = (height / width) * Mathf.Rad2Deg;
        var zAngle = Mathf.Atan2(height, width) * Mathf.Rad2Deg;

        if (Input.GetKeyDown("space") && !isSpaceDown)
        {
            isSpaceDown = true;
            Debug.Log("");
            Debug.Log("-----------------> NEW INFORMATION at time " + Time.time + "\n" + 
                "height  = " + height + "\n" +
                "width = " + width + "\n" +
                "height / width = " + height / width + "\n" +
                "Mathf.Tan(height / width) = " + Mathf.Tan(height / width) + "\n" +
                "zAngle = " + zAngle + "\n" + 
                "GetAxis->Y = " + Input.GetAxis("Mouse Y")
                );

            //node.transform.Rotate(0.0f, 0.0f, -2.0f, Space.Self);
        }

        if (Input.GetKeyUp("space"))
            isSpaceDown = false;

        //var factor = Mathf.Abs( height / width);
        //var speed = 10f;
        //node.transform.Rotate(0, 0, Input.GetAxis("Mouse X") * factor * speed);
        //node.transform.Rotate(new Vector3(0, 0, (height / width) * Mathf.Rad2Deg));


        node.transform.Rotate(0.0f, 0.0f, -oldZAngle, Space.Self);
        node.transform.Rotate(0.0f, 0.0f, zAngle, Space.Self);
        oldZAngle = zAngle;
        
    }

    private float GetSpeed(float width)
    {
        if (width < 1f)
            return 0.5f;
        else if (width < 8f)
            return 1f;

        return 1.0f;
    }

    private void UpdatePosition(float oldWidth, float newWidth)
    {
        Vector3 newPos = node.transform.position;
        //newPos.x += (newWidth - oldWidth) / 2;
        newPos.z = 10;
        node.transform.position = newPos;
    }
}
