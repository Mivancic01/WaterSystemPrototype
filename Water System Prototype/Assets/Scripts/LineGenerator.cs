using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject nodePrefab, pipeSymbol, pumpSymbol, valveSymbol;

    private int nodeType = 0;
    private bool hasCreatedStart = false;
    private Vector3 startPosition, endPosition, originalScale;
    private float oldZAngle = 0f, originalWidth;
    private GameObject line;
    bool isSpaceDown = false;

    public static LineGenerator Instance { get; private set; }

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
        Debug.Log("CALLED ---> LineGenerator::EnterNodeCreationMode()");
        GameStateManager.Instance.SetPathCreationState();
    }

    public void EnterNodeCreationMode(int type)
    {
        Debug.Log("CALLED ---> LineGenerator::EnterNodeCreationMode(int type)");
        nodeType = type;
        GameStateManager.Instance.SetPathCreationState();
    }

    public void SetNode(Vector3 nodePos)
    {
        if (!GameStateManager.Instance.createPath)
            return;

        Debug.Log("StartPosition = " + nodePos + "\n"
            + "At time = " + Time.time);

        if (hasCreatedStart)
        {
            CreateNodeEndAndDestroy(nodePos);
            return;
        }

        CreateNodeStart(nodePos);
    }

    public GameObject CreateAndReturnLineComponent(Vector3 startNode, Vector3 endNode, int typeID)
    {
        if (!GameStateManager.Instance.createPath)
            return null;

        //Debug.Log("StartPosition = " + startNode + ", EndPosition = " + endNode + "\n" + "At time = " + Time.time);

        nodeType = typeID;
        CreateNodeStart(startNode);
        CreateNodeEnd(endNode);

        GameObject lineCopy = line;
        Reset();

        return lineCopy;
    }

    private void CreateNodeStart(Vector3 startPos)
    {
        hasCreatedStart = true;
        startPosition = startPos;
        line = Instantiate(nodePrefab, startPos, Quaternion.identity);
        originalWidth = line.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        originalScale = line.transform.localScale;

        StartCoroutine("CreateLineFromMousePos");
    }

    private void CreateNodeEndAndDestroy(Vector3 endPos)
    {
        hasCreatedStart = false;
        endPosition = endPos;

        StopCoroutine("CreateLineFromMousePos");
        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();
        SetEndPosition(endPos);
        CreateSymbol();
        Reset();
    }

    private void CreateNodeEnd(Vector3 endPos)
    {
        hasCreatedStart = false;
        endPosition = endPos;

        StopCoroutine("CreateLineFromMousePos");
        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();
        SetEndPosition(endPos);
        CreateSymbol();
    }

    IEnumerator CreateLineFromMousePos()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (line != null)
                    GameObject.Destroy(line);

                GameStateManager.Instance.SetInactiveState();
                GameStateManager.Instance.SetDragComponentsState();
                yield break;
            }

            SetEndPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            yield return null;
        }
    }

    private void UpdateTransform()
    {
        var height = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - startPosition.y;
        var width = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startPosition.x);
        var zAngle = Mathf.Atan2(height, width) * Mathf.Rad2Deg;

        UpdateScale(Mathf.Sqrt(width * width + height * height));
        UpdateRotation(zAngle);
    }
    private void SetEndPosition(Vector3 endPos)
    {
        var height = endPos.y - startPosition.y;
        var width = endPos.x - startPosition.x;
        var zAngle = Mathf.Atan2(height, width) * Mathf.Rad2Deg;

        UpdateScale(Mathf.Sqrt(width * width + height * height));
        UpdateRotation(zAngle);
        UpdatePosition();
    }

    private void UpdateScale(float hipotenusa)
    {
        Vector3 newScale = originalScale;
        newScale.x *= hipotenusa / originalWidth;
        line.transform.localScale = newScale;
    }

    private void UpdateRotation(float zAngle)
    {
        line.transform.Rotate(0.0f, 0.0f, -oldZAngle, Space.Self);
        line.transform.Rotate(0.0f, 0.0f, zAngle, Space.Self);
        oldZAngle = zAngle;
    }

    private void UpdatePosition()
    {
        var tempPos = line.transform.position;
        tempPos.z = 9f;
        line.transform.position = tempPos;
    }

    private void CreateSymbol()
    {
        var xPos = (endPosition.x - startPosition.x) / 2;
        var yPos = (endPosition.y - startPosition.y) / 2;
        Vector3 pos = new Vector3(xPos + startPosition.x, yPos + startPosition.y, 10);
        GameObject nodeSymbol;

        switch (nodeType)
        {
            case 0:
                nodeSymbol = Instantiate(pipeSymbol, line.transform, false);
                nodeSymbol.transform.position = pos;
                break;
            case 1:
                nodeSymbol = Instantiate(pumpSymbol, line.transform, false);
                nodeSymbol.transform.position = pos;
                break;
            case 2:
                nodeSymbol = Instantiate(valveSymbol, line.transform, false);
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
        oldZAngle = 0f;
        isSpaceDown = false;
        line = null;
    }
}
