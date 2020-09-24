using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    public bool useDebug = false;
    void Update()
    {
        if (mouse_over)
        {
            if (useDebug) Debug.Log("Mouse Over obj " + gameObject.name);
            GameStateManager.Instance.SetCanDragMap(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        if (useDebug) Debug.Log("Mouse enter obj " + gameObject.name);

        GameStateManager.Instance.SetCanDragMap(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        if (useDebug) Debug.Log("Mouse exit obj " + gameObject.name);

        GameStateManager.Instance.SetCanDragMap(true);
    }
}
