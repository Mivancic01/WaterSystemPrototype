using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComponentCreator : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static bool isInstantiated = false;
    public bool useDebug = false, isTutorial = false;

    public GameObject componentPrefab;
    public int componentTypeID, localEventID = -1;

    private const int nullID = -1;
    private const bool addToCurrentModel = true;

    public void OnDrag(PointerEventData eventData)
    {
        if (GameStateManager.Instance.createPath || GameStateManager.Instance.dragMap)
            return;

        if(isTutorial)
            if (!TutorialManager.Instance.CheckEvent(localEventID))
                return;

        GameStateManager.Instance.SetInactiveState();
        if (useDebug) Debug.Log("DRAGGING!");

        if (!isInstantiated)
        {
            if (useDebug) Debug.Log("CREATING A COMPONENT!" + "\n" +
                "At time " + Time.time);

            GameObject component = Instantiate(componentPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            component.GetComponent<ComponentObject>().StartInitialDrag();
            MainSimulationManager.ComponentsManager.AddNodeComponent(component, componentTypeID, nullID, addToCurrentModel);
            isInstantiated = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameStateManager.Instance.SetDragComponentsState();
        isInstantiated = false;

        if (useDebug) Debug.Log("DROPPING!");
    }
}
