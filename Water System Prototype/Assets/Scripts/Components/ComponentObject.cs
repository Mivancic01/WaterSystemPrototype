using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentObject : MonoBehaviour
{
    public bool useDebug = false, isNodeComponent = true;
    public int elementID;
    public float maxDeltaTime = 0.3f;

    private float mouseDownTime;
    private Vector3 originalPos;

    void OnMouseDrag()
    {
        if (!isNodeComponent)
            return;

        if (GameStateManager.Instance.dragComponents)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 10;

            transform.position = pos;
        }

    }

    void OnMouseUp()
    {
        if ((Time.time - mouseDownTime) > maxDeltaTime)
        {
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

        if(!GameStateManager.Instance.createPath)
        {
            //componentsManager.OpenPropertiesWindow(elementID);
            gameObject.GetComponent<BaseElement>().ChangeWindowVisibility(true);
            GameStateManager.Instance.SetInactiveState();
        }
    }

    void OnMouseDown()
    {
        originalPos = transform.position;
        mouseDownTime = Time.time;
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
            pos.z = 10;
            transform.position = pos;

            yield return null;
        }
        
    }
}
