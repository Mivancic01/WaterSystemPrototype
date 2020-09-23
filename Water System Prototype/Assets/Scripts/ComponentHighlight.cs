using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentHighlight : MonoBehaviour
{
    public GameObject highlightObject;
    public bool hasParentComponent = false, hasChildComponent = false;

    private ComponentHighlight childComponent, parentComponent;
    private bool isReady = true;

    void Start()
    {
        highlightObject.SetActive(false);
        Reset();

        /*
        if (hasChildComponent)
        {
            Debug.Log(gameObject.name + " -----------------> HAS " + gameObject.GetComponentsInChildren<ComponentHighlight>().ToList().Count + " Children!");
            childComponent = gameObject.GetComponentInChildren<ComponentHighlight>();
        }

        if (hasParentComponent)
        {
            Debug.Log(gameObject.name + " -----------------> HAS " + gameObject.GetComponentsInParent<ComponentHighlight>().ToList().Count + " Parents!");
            parentComponent = gameObject.GetComponentInParent<ComponentHighlight>();
        }
        */
    }

    void OnMouseEnter()
    {
        Debug.Log("----------> ENTERED ComponentHighlight::OnMouseEnter()");
        highlightObject.SetActive(true);

        if (GameStateManager.Instance.createPath)
            return;

        if (GameStateManager.Instance.dragComponents)
            SetHighlightVisibility(true, true);
    }

    void OnMouseExit()
    {
        SetHighlightVisibility(false, true);
    }

    public void SetHighlightVisibility(bool isVisible, bool setCompanionVisibility)
    {
        //Debug.Log("----------> ENTERED ComponentHighlight::SetHighlightVisibility()");

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
            //Debug.Log(gameObject.name + " -----------------> HAS " + gameObject.GetComponentsInChildren<ComponentHighlight>().ToList().Count + " Children!");
            foreach (var component in gameObject.GetComponentsInChildren<ComponentHighlight>())
                if (component.gameObject != gameObject)
                    childComponent = component;

            if (childComponent == null)
            {
                isReady = false;
                //Debug.LogError("COMPONENT HAS NO CHILD WHEN IT SHOULD HAVE 1!");
                return;
            }

           // Debug.Log(gameObject.name + " Has a child with name: " + childComponent.gameObject.name);
        }

        if (hasParentComponent)
        {
            //Debug.Log(gameObject.name + " -----------------> HAS " + gameObject.GetComponentsInParent<ComponentHighlight>().ToList().Count + " Parents!");
            foreach (var component in gameObject.GetComponentsInParent<ComponentHighlight>())
                if (component.gameObject != gameObject)
                    parentComponent = component;

            if (parentComponent == null)
            {
                isReady = false;
               // Debug.LogError("COMPONENT HAS NO PARENT WHEN IT SHOULD HAVE 1!");
                return;
            }

           // Debug.Log(gameObject.name + " Has a parent with name: " + parentComponent.gameObject.name);
            parentComponent.Reset();
        }
    }
}
