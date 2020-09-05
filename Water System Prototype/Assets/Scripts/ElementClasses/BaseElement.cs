using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Elements
{
    public class BaseElement
    {
        private GameObject propertiesWindow, elemIcon;
        public int typeID;
        public Vector3 position;

        public int listIndex;

        public BaseElement(int index, int id, Vector3 pos)
        {
            listIndex = index;
            typeID = id;
            position = pos;
        }

        public void Initialize()
        {
            elemIcon = ElementsFactory.Instance.CreateElement(typeID, position);
            elemIcon.GetComponent<DraggableObject>().elementsListIndex = listIndex;

            propertiesWindow = ElementsFactory.Instance.CreatePropertiesWindow(typeID);
            propertiesWindow.GetComponent<PropertiesWindow>().elementsListIndex = listIndex;
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
            listIndex = newIndex;
            elemIcon.GetComponent<DraggableObject>().elementsListIndex = listIndex;
            propertiesWindow.GetComponent<PropertiesWindow>().elementsListIndex = listIndex;
        }

        public void DestroyElement()
        {
            GameObject.Destroy(elemIcon);
            GameObject.Destroy(propertiesWindow);
        }
    }
}


