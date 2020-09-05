using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableElements : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static bool isDragging = false, isInstantiated = false;
    public bool useDebug = false;

    Vector3 origPos;
    public GameObject elementPrefab;

    public void OnDrag(PointerEventData eventData)
    {
        DragManager.Instance.DraggableMap = false;
        if (useDebug) Debug.Log("DRAGGING!");

        GameObject elem = null;
        if(!isInstantiated)
        {
            elem = Instantiate(elementPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            elem.GetComponent<DraggableObject>().useInitialDrag = true;
        }
        isInstantiated = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragManager.Instance.DraggableMap = true;
        isInstantiated = false;
        if (useDebug) Debug.Log("DROPPING!");


    }
}
