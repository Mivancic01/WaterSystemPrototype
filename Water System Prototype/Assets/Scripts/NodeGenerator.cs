using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public GameObject nodePrefab, pumpSymbol, valveSymbol;

    private int nodeType = 0;
    private bool hasCreatedStart = false, hasCreatedEnd = false;
    private Vector3 startPosition, originalScale;
    private float oldZAngle = 0f, originalWidth;
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
        Debug.Log("CALLED ---> NodesGenerator::EnterNodeCreationMode()");
        DragManager.Instance.IsInNodeCreateState = true;
    }
    public void EnterNodeCreationMode(int type)
    {
        Debug.Log("CALLED ---> NodesGenerator::EnterNodeCreationMode(int type)");
        nodeType = type;
        DragManager.Instance.IsInNodeCreateState = true;
    }

    public void GenerateNode(Vector3 startPos)
    {
        Debug.Log("StartPosition = " + startPos + "\n"
            + "At time = " + Time.time);

        if (!hasCreatedStart)
            hasCreatedStart = true;
        else
        {
            hasCreatedStart = false;
            StopCoroutine("CreateNodeFromObjectPos");
            DragManager.Instance.IsInNodeCreateState = false;
            //SetEndPosition(startPos);
            CreateSymbol();
            Reset();
            return;
        }    

        startPosition = startPos;
        node = Instantiate(nodePrefab, startPos, Quaternion.identity);
        originalWidth = node.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        originalScale = node.transform.localScale;
        
        StartCoroutine("CreateNodeFromObjectPos");
    }

    private void CreateNodeStart(Vector3 startPos)
    {
        hasCreatedStart = true; 
        startPosition = startPos;
        node = Instantiate(nodePrefab, startPos, Quaternion.identity);
        originalWidth = node.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        originalScale = node.transform.localScale;

        StartCoroutine("CreateNodeFromObjectPos");
    }

    private void CreateNodeEnd(Vector3 endPos)
    {
        hasCreatedStart = false;
        StopCoroutine("CreateNodeFromObjectPos");
        DragManager.Instance.IsInNodeCreateState = false;
        SetEndPosition(endPos);
        CreateSymbol();
        Reset();
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

        Reset();
    }

    private void UpdateTransform()
    {
        var height = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - startPosition.y;
        var width = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x);
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
                "GetAxis->Y = " + Input.GetAxis("Mouse Y") + "\n" +
                "Mathf.Cos(zAngle) = " + Mathf.Cos(180 * Mathf.Deg2Rad)
                );
        }

        if (Input.GetKeyUp("space"))
            isSpaceDown = false;

        UpdateScale(Mathf.Sqrt(width * width + height * height));

        node.transform.Rotate(0.0f, 0.0f, -oldZAngle, Space.Self);
        node.transform.Rotate(0.0f, 0.0f, zAngle, Space.Self);
        oldZAngle = zAngle;
    }

    private void UpdateScale(float hipotenusa)
    {
        Vector3 newScale = originalScale;
        newScale.x *= hipotenusa / originalWidth;
        node.transform.localScale = newScale;
    }

    private void SetEndPosition(Vector3 endPos)
    {
        var height = endPos.y - startPosition.y;
        var width = endPos.x - startPosition.x;
        var zAngle = Mathf.Atan2(height, width) * Mathf.Rad2Deg;

        UpdateScale(Mathf.Sqrt(width * width + height * height));

        node.transform.Rotate(0.0f, 0.0f, -oldZAngle, Space.Self);
        node.transform.Rotate(0.0f, 0.0f, zAngle, Space.Self);
        oldZAngle = zAngle;
    }

    private void CreateSymbol()
    {
        var xPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x) / 2;
        var yPos = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - startPosition.y) / 2;
        Vector3 pos = new Vector3(xPos + startPosition.x, yPos + startPosition.y, 10);
        GameObject nodeSymbol;

        switch (nodeType)
        {
            case 0:
                return;
            case 1:
                nodeSymbol = Instantiate(pumpSymbol, node.transform, false);
                nodeSymbol.transform.position = pos;
                break;
            case 2:
                nodeSymbol = Instantiate(valveSymbol, node.transform, false);
                nodeSymbol.transform.position = pos;
                break;
            default:
                Debug.LogError("INVALID NODE TYPE!");
                break;
        }
    }

    private void Reset()
    {

        hasCreatedStart = false;
        hasCreatedEnd = false;
        oldZAngle = 0f;
        isSpaceDown = false;
        node = null;
    }
}
