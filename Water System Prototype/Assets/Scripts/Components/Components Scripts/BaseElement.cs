using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseElement : MonoBehaviour
{
    protected GameObject propertiesWindow;
    public int typeID = -1;
    public string ID = "";
    public bool isNodeComponent { get; protected set; }

    public BaseElement(string id, int typeId, bool isNode)
    {
        ID = id;
        typeID = typeId;
        isNodeComponent = isNode;
    }

    public virtual void Initialize(int pTypeID = -1, string pID = "", bool isNode = true)
    {
        if (pTypeID != -1)
            typeID = pTypeID;
        if (!pID.Equals(""))
            ID = pID;

        isNodeComponent = isNode;

        gameObject.GetComponent<ComponentObject>().elementID = ID;

        //propertiesWindow = ComponentsFactory.Instance.CreatePropertiesWindow(typeID);
        //propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
        //ChangeWindowVisibility(false);
    }

    public virtual void UpdatePropertiesValues()
    {
        Debug.LogWarning("CALLED BaseElement::UpdatePropertiesValues()");
    }

    public virtual void UpdatePropertiesValues(List<float> values)
    {
        Debug.LogWarning("CALLED BaseElement::UpdatePropertiesValues(List<float> values)");
    }

    public void ChangeVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    public void ChangeWindowVisibility(bool isVisible)
    {
        propertiesWindow.SetActive(isVisible);
    }

    public void DestroyElement()
    {
        GameObject.Destroy(propertiesWindow);
        GameObject.Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}


