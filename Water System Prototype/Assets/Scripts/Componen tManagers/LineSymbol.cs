using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSymbol : MonoBehaviour
{
    public bool useDebug = false;
    public float maxDeltaTime = 0.3f;
    private float mouseDownTime;

    void OnMouseUp()
    {
        if ((Time.time - mouseDownTime) > maxDeltaTime)
            return;

        if (useDebug) Debug.Log("ENTERED OnMouseUp() on object " + gameObject.name + "\n" + "At time " + Time.time);

        if (GameStateManager.Instance.createPath)
        {
            return;
        }

        if (!GameStateManager.Instance.createPath)
        {
            ComponentsManager.Instance.OpenPropertiesWindow(GetComponentInParent<ComponentObject>().elementID);
            GameStateManager.Instance.SetInactiveState();
        }
    }

    void OnMouseDown()
    {
        mouseDownTime = Time.time;
    }
}
