using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentObject : MonoBehaviour
{
    public bool useDebug = false, isNodeComponent = true, isTutorial = false;
    public int localEventID = -1;
    public string elementID;
    public float maxDeltaTime = 0.1f;

    private float mouseDownTime;
    private Vector3 originalPos;

    void Awake()
    {
        if (TutorialManager.Instance != null)
            isTutorial = true;
    }

    void OnMouseDrag()
    {
        if (!isNodeComponent)
            return;

        if (GameStateManager.Instance.dragComponents)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.y = 50;
            transform.position = pos;
        }

    }

    void OnMouseUp()
    {
        if ((Time.time - mouseDownTime) > maxDeltaTime)
        {

            Debug.Log("The elementID is: " + elementID);
            if(isNodeComponent)
                MainSimulationManager.ComponentsManager.UpdateLinesPosition(elementID);

            return;
        }

        transform.position = originalPos;

        if (useDebug) Debug.Log("ENTERED OnMouseUp() on object " + gameObject.name + "\n" + "At time " + Time.time);

        if (GameStateManager.Instance.createPath)
        {
            Debug.Log("CALLING GenerateNode() FROM OBJECT WITH NAME: " + this.name + "\n" + "At time " + Time.time);

            if(isNodeComponent)
                LineGenerator.Instance.SetNode(this.transform.position, elementID);

            return;
        }

        if(GameStateManager.Instance.dragComponents)
        {
            int typeID = gameObject.GetComponent<BaseElement>().typeID;
            switch(typeID)
            {
                case 0:
                    ComponentsHelper.Instance.UpdateJunctionPropertiesWindow(gameObject.GetComponent<Junction>(), elementID);
                    break;
                case 1:
                    ComponentsHelper.Instance.UpdatePipePropertiesWindow(gameObject.GetComponent<Pipe>(), elementID);
                    break;
                case 2:
                    ComponentsHelper.Instance.UpdatePumpPropertiesWindow(gameObject.GetComponent<Pump>(), elementID);
                    break;
                case 3:
                    ComponentsHelper.Instance.UpdateReservoirPropertiesWindow(gameObject.GetComponent<Reservoir>(), elementID);
                    break;
                case 4:
                    ComponentsHelper.Instance.UpdateTankPropertiesWindow(gameObject.GetComponent<Tank>(), elementID);
                    break;
                case 5:
                    ComponentsHelper.Instance.UpdateValvePropertiesWindow(gameObject.GetComponent<Valve>(), elementID);
                    break;
                default:
                    Debug.LogError("TRYING TO OPEN A WINDOW FROM AN UKNOWN TYPE OF COMPONENT!");
                    break;
            }
            //gameObject.GetComponent<BaseElement>().ChangeWindowVisibility(true);

            GameStateManager.Instance.SetInactiveState();
        }
    }

    void OnMouseDown()
    {
        originalPos = transform.position;
        mouseDownTime = Time.time;
        Debug.Log("ENTERED ON MOUSE DOWN! on elementID "  + elementID);
    }

    public void StartInitialDrag()
    {
        if (useDebug) Debug.Log("ENTERED StartInitialDrag() on object " + gameObject.name + "\n"
                    + "At time " + Time.time);

        StartCoroutine("DragPosition");
    }

    IEnumerator DragPosition()
    {
        while(Input.GetMouseButton(0))
        {
            if (useDebug) Debug.Log("ENTERED DragPosition() on object " + gameObject.name + "\n"
                    + "At time " + Time.time);

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.y = 50;
            transform.position = pos;

            transform.rotation = Quaternion.Euler(90, 0, 0);

            yield return null;
        }


        if (isTutorial)
            TutorialManager.Instance.CheckEvent(localEventID);
    }
}
