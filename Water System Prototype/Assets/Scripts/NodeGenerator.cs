using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public GameObject pipePrefab, pumpPrefab, valvePrefab;

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
        if (!hasCreatedStart)
            hasCreatedStart = true;
        else
        {
            hasCreatedStart = false;
            StopCoroutine("CreateNodeFromObjectPos");
            DragManager.Instance.IsInNodeCreateState = false;
            Reset();
            return;
        }    
        startPosition = startPos;
        InstantianteNode(startPos);
        originalWidth = node.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        originalScale = node.transform.localScale;
        
        StartCoroutine("CreateNodeFromObjectPos");
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

    private void Reset()
    {

        hasCreatedStart = false;
        hasCreatedEnd = false;
        oldZAngle = 0f;
        isSpaceDown = false;
        node = null;
    }
}
