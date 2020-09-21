using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsOutline : MonoBehaviour
{
    public GameObject outlineObject;

    void Start()
    {
        outlineObject.SetActive(false);
    }

    void OnMouseEnter()
    {
        Debug.Log("ENTERED ElementsOutline::OnMouseEnter()");
        outlineObject.SetActive(true);

        if (DragManager.Instance.IsInNodeCreateState)
            return;

        if (!DragManager.Instance.DraggableElements)
        {
            outlineObject.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        outlineObject.SetActive(false);
    }
}
