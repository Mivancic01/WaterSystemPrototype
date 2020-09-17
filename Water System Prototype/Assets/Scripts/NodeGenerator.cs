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

    public void EnterNodeCreationMode()
    {
        DragManager.Instance.DraggableElements = false;
        DragManager.Instance.DraggableMap = false;
        StartCoroutine("CreateNode");
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
        float oldWidth = node.GetComponent<SpriteRenderer>().bounds.size.x;
        var newWidth = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x;

        Debug.Log("OldWidth = " + oldWidth);
        Debug.Log("newWidht = " + newWidth);
        Debug.Log("X scale = " + newWidth / oldWidth);

        if (newWidth == 0)
            newWidth = oldWidth;


        UpdateScale(oldWidth, newWidth);
        UpdatePosition(oldWidth, newWidth);
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
        var zAngle = Mathf.Tan(height / newWidth) * Mathf.Rad2Deg;

        node.transform.Rotate(0.0f, 0.0f, -oldZAngle, Space.Self);
        node.transform.Rotate(0.0f, 0.0f, zAngle, Space.Self);
        oldZAngle = zAngle;
    }

    private void UpdatePosition(float oldWidth, float newWidth)
    {
        Vector3 newPos = node.transform.position;
        //newPos.x += (newWidth - oldWidth) / 2;
        newPos.z = 10;
        node.transform.position = newPos;
    }
}
