using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentOutline : MonoBehaviour
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

        if (GameStateManager.Instance.createPath)
            return;

        if (!GameStateManager.Instance.dragComponents)
        {
            outlineObject.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        outlineObject.SetActive(false);
    }
}
