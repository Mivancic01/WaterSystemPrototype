using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject pipePrefab, pumpPrefab, valvePrefab, pipeSymbol, pumpSymbol, valveSymbol;
    private GameObject line;

    public bool isTutorial = false;
    public int localEventID = -1;

    private int nodeType = 0, startNodeID, endNodeID;
    private bool hasCreatedStart = false;
    private Vector3 startPosition, endPosition, originalScale;
    private float oldZAngle = 0f, originalWidth;

    private const int nullID = -1;
    private const bool addToCurrentModel = true;

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

        if (TutorialManager.Instance != null)
            isTutorial = true;
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

    public void SetNode(Vector3 nodePos, int nodeID)
    {
        if (!GameStateManager.Instance.createPath)
            return;

        Debug.Log("StartPosition = " + nodePos + "\n"
            + "At time = " + Time.time);

        if (hasCreatedStart)
        {
            CreateNodeEnd(nodePos);
            endNodeID = nodeID;
            MainSimulationManager.ComponentsManager.AddLineComponent(line, nodeType, startNodeID, endNodeID, nullID, addToCurrentModel);
            Reset();
            return;
        }

        CreateNodeStart(nodePos);
        startNodeID = nodeID;
    }

    public GameObject CreateAndReturnLineComponent(Vector3 startNode, Vector3 endNode, int typeID)
    {
        if (!GameStateManager.Instance.createPath)
            return null;

        nodeType = typeID;
        CreateNodeStart(startNode, false);
        CreateNodeEnd(endNode, false);

        GameObject lineCopy = line;
        Reset();

        return lineCopy;
    }

    public void UpdateLinePosition(GameObject existingLine, Vector3 startNodePos, Vector3 endNodePos)
    {
        existingLine.transform.rotation = pipePrefab.transform.rotation;
        existingLine.transform.position = startNodePos;

        LineGeneratorHelper.EndPositionVariables vars = new LineGeneratorHelper.EndPositionVariables(
            existingLine, startNodePos, endNodePos, pipePrefab.transform.localScale, pipePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2, 0.0f);
        existingLine = LineGeneratorHelper.SetEndPosition(vars);

        //existingLine = SetEndPosition(endNodePos);
    }

    private void CreateNodeStart(Vector3 startPos, bool runCoroutine = true)
    {
        hasCreatedStart = true;
        startPosition = startPos;
        CreateLineObject(startPos);
        originalWidth = line.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        originalScale = line.transform.localScale;

        if(runCoroutine)
            StartCoroutine("CreateLineFromMousePos");
    }

    private void CreateNodeEnd(Vector3 endPos, bool runCoroutine = true)
    {
        hasCreatedStart = false;
        endPosition = endPos;

        if(runCoroutine)
            StopCoroutine("CreateLineFromMousePos");
        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();

        LineGeneratorHelper.EndPositionVariables vars = new LineGeneratorHelper.EndPositionVariables(
            line, startPosition, endPosition, originalScale, originalWidth, oldZAngle);
        line = LineGeneratorHelper.SetEndPosition(vars);

        if (isTutorial)
            TutorialManager.Instance.CheckEvent(localEventID);

        //SetEndPosition(endPos);
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

            LineGeneratorHelper.EndPositionVariables vars = new LineGeneratorHelper.EndPositionVariables(
            line, startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition), originalScale, originalWidth, oldZAngle);
            (line, oldZAngle) = LineGeneratorHelper.SetEndPositionAndGetAngle(vars);

            //SetEndPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            yield return null;
        }
    }

    private void CreateSymbol()
    {
        var xPos = (endPosition.x - startPosition.x) / 2;
        var yPos = (endPosition.y - startPosition.y) / 2;
        Vector3 pos = new Vector3(xPos + startPosition.x, yPos + startPosition.y, 10);
        GameObject nodeSymbol;

        switch (nodeType)
        {
            case 1:
                nodeSymbol = Instantiate(pipeSymbol, line.transform, false);
                nodeSymbol.transform.position = pos;
                break;
            case 2:
                nodeSymbol = Instantiate(pumpSymbol, line.transform, false);
                nodeSymbol.transform.position = pos;
                break;
            case 5:
                nodeSymbol = Instantiate(valveSymbol, line.transform, false);
                nodeSymbol.transform.position = pos;
                break;
            default:
                Debug.LogError("INVALID NODE TYPE!");
                break;
        }
    }

    private void CreateLineObject(Vector3 startPos)
    {
        switch (nodeType)
        {
            case 1:
                line = Instantiate(pipePrefab, startPos, Quaternion.identity);
                break;
            case 2:
                line = Instantiate(pumpPrefab, startPos, Quaternion.identity);
                break;
            case 5:
                line = Instantiate(valvePrefab, startPos, Quaternion.identity);
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
        line = null;
    }
}
