using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentHighlight : MonoBehaviour
{
    public GameObject highlightObject;
    public bool hasParentComponent = false, hasChildComponent = false;
    public bool useDebug = false;

    private ComponentHighlight childComponent, parentComponent;
    private bool isReady = true;

    void Start()
    {
        highlightObject.SetActive(false);
        Reset();
    }

    void OnMouseEnter()
    {
        if(useDebug) Debug.Log("----------> ENTERED ComponentHighlight::OnMouseEnter()");
        highlightObject.SetActive(true);

        //if (GameStateManager.Instance.createPath)
        //    return;

        if (GameStateManager.Instance.dragComponents && !GameStateManager.Instance.createPath)
            SetHighlightVisibility(true, true);

        if (GameStateManager.Instance.createPath)
            SetHighlightVisibility(false, true);
    }

    void OnMouseExit()
    {
        SetHighlightVisibility(false, true);
    }

    public void SetHighlightVisibility(bool isVisible, bool setCompanionVisibility)
    {
        if(useDebug) Debug.Log("----------> ENTERED ComponentHighlight::SetHighlightVisibility() with game object " + gameObject.name + " At time " + Time.time);

        highlightObject.SetActive(isVisible);
        /**/
        if (hasChildComponent && setCompanionVisibility)
            childComponent.SetHighlightVisibility(isVisible, false);

        if (hasParentComponent && setCompanionVisibility)
            parentComponent.SetHighlightVisibility(isVisible, false);
            
    }

    public void Reset()
    {
        if (hasChildComponent)
        {
            //if(useDebug) Debug.Log(gameObject.name + " -----------------> HAS " + gameObject.GetComponentsInChildren<ComponentHighlight>().ToList().Count + " Children!");
            foreach (var component in gameObject.GetComponentsInChildren<ComponentHighlight>())
                if (component.gameObject != gameObject)
                    childComponent = component;

            if (childComponent == null)
            {
                isReady = false;
                //if(useDebug) Debug.LogError("COMPONENT HAS NO CHILD WHEN IT SHOULD HAVE 1!");
                return;
            }

           // if(useDebug) Debug.Log(gameObject.name + " Has a child with name: " + childComponent.gameObject.name);
        }

        if (hasParentComponent)
        {
            //if(useDebug) Debug.Log(gameObject.name + " -----------------> HAS " + gameObject.GetComponentsInParent<ComponentHighlight>().ToList().Count + " Parents!");
            foreach (var component in gameObject.GetComponentsInParent<ComponentHighlight>())
                if (component.gameObject != gameObject)
                    parentComponent = component;

            if (parentComponent == null)
            {
                isReady = false;
               // if(useDebug) Debug.LogError("COMPONENT HAS NO PARENT WHEN IT SHOULD HAVE 1!");
                return;
            }

           // if(useDebug) Debug.Log(gameObject.name + " Has a parent with name: " + parentComponent.gameObject.name);
            parentComponent.Reset();
        }
    }
}
