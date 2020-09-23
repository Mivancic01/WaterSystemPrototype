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

        public virtual void Initialize()
        {
            if(typeID == 1 || typeID == 2 || typeID == 5)
            {
                Debug.LogWarning("CREATING A NODE, But the code doesent exist yet!");
                return;
            }    
            elemIcon = ComponentGameObjectsFactory.Instance.CreateElement(typeID, position);
            elemIcon.GetComponent<ComponentObject>().elementID = ID;

            propertiesWindow = ComponentGameObjectsFactory.Instance.CreatePropertiesWindow(typeID);
            propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
            ChangeWindowVisibility(false);
        }

        public virtual void UpdatePropertiesValues()
        {
            Debug.LogWarning("CALLED BaseElement::UpdatePropertiesValues()");
        }

        public virtual void UpdatePropertiesValues(List<float> values)
        {
            Debug.LogWarning("CALLED BaseElement::UpdatePropertiesValues(List<float> values)");
        }

        public void Initialize(GameObject icon)
        {
            elemIcon = icon;
            elemIcon.GetComponent<ComponentObject>().elementID = ID;

            propertiesWindow = ComponentGameObjectsFactory.Instance.CreatePropertiesWindow(typeID);
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

        public void DestroyElement()
        {
            GameObject.Destroy(elemIcon);
            GameObject.Destroy(propertiesWindow);
        }

        public Vector3 GetPosition()
        {
            return elemIcon.transform.position;
        }
    }
}


