using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public GameObject nodePrefab;

    private bool hasCreatedStart = false, hasCreatedEnd = false, canStart = false, isMouseDown = true;
    private int timesDownPressed = 0;
    private Vector3 startPosition;
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

                node = Instantiate(nodePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

                startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPosition.x -= node.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 10;
                node.transform.position = pos;
            }

            //Waiting for player to select a startPosition. Node isnt created yet
            if(node == null)
                yield return null;

            //float width = node.GetComponent<SpriteRenderer>().bounds.size.x;
            //node.GetComponent<SpriteRenderer>().bounds.size.x = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x);

            if(node != null)
                UpdatePositionAndScale();

            yield return null;
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

    private void UpdatePositionAndScale()
    {
        float oldWidth = node.GetComponent<SpriteRenderer>().bounds.size.x;
        var newWidht = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x;

        Debug.Log("OldWidth = " + oldWidth);
        Debug.Log("newWidht = " + newWidht);
        Debug.Log("X scale = " + newWidht / oldWidth);

        if (newWidht == 0)
            newWidht = oldWidth;

        Vector3 newScale = node.transform.localScale;
        newScale.x *= newWidht / oldWidth;
        node.transform.localScale = newScale;

        Vector3 newPos = node.transform.position;
        newPos.x += (newWidht - oldWidth) / 2;
        newPos.z = 10;
        node.transform.position = newPos;
    }
}
