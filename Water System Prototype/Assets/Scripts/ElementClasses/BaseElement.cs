using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Elements
{
    public class BaseElement
    {
        protected GameObject propertiesWindow, elemIcon;
        public int typeID, ID;
        public Vector3 position;

        public BaseElement(int id, int typeId, Vector3 pos)
        {
            ID = id;
            typeID = typeId;
            position = pos;
        }

        public void Initialize()
        {
            elemIcon = ElementsGameObjectFactory.Instance.CreateElement(typeID, position);
            elemIcon.GetComponent<DraggableObject>().elementID = ID;

            propertiesWindow = ElementsGameObjectFactory.Instance.CreatePropertiesWindow(typeID);
            propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
            ChangeWindowVisibility(false);
        }

        public virtual void UpdatePropertiesValues()
        {
            Debug.LogWarning("CALLED BaseElement::UpdatePropertiesValues()");
        }

        public void Initialize(GameObject icon)
        {
            elemIcon = icon;
            elemIcon.GetComponent<DraggableObject>().elementID = ID;

            propertiesWindow = ElementsGameObjectFactory.Instance.CreatePropertiesWindow(typeID);
            propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
            ChangeWindowVisibility(false);
        }

        public void ChangeVisibility(bool isVisible)
        {
            elemIcon.SetActive(isVisible);
        }

        public void ChangeWindowVisibility(bool isVisible)
        {
            propertiesWindow.SetActive(isVisible);
        }

        public void UpdateListIndex(int newIndex)
        {
            Debug.Log("CALLED ---> BaseElement::UpdateListIndex() with old index = " + ID + " and new index = " + newIndex);
            ID = newIndex;

            if (elemIcon.GetComponent<DraggableObject>() == null)
                Debug.LogError("DRAGGABLE OBJECT SCRIPT IS NULL!");
            elemIcon.GetComponent<DraggableObject>().elementID = ID;
            propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
        }

        public void DestroyElement()
        {
            GameObject.Destroy(elemIcon);
            GameObject.Destroy(propertiesWindow);
        }
    }
}


