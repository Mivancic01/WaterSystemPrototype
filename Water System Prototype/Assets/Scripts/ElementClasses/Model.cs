using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class Model
    {
        public int year;
        public List<int> elementIndicesList;
        ElementsManager elementsManager;

        private Model()
        {

        }

        public Model(int yr)
        {
            year = yr;
            elementIndicesList = new List<int>();
            elementsManager = ElementsManager.Instance;
        }

        public void Add(int index)
        {
            if (elementIndicesList == null)
                elementIndicesList = new List<int>();

            elementIndicesList.Add(index);
        }

        public void ChangeListVisibility(bool isVisible)
        {
            foreach (var index in elementIndicesList)
                elementsManager.elementList[index].ChangeVisibility(isVisible);
        }

        public void DestroyModel()
        {
            elementIndicesList.Clear();
        }

        public void RemoveElement(int index)
        {
            foreach (var elemIndex in elementIndicesList)
                if (elemIndex == index)
                    elementIndicesList.Remove(elemIndex);
        }

        public void Print()
        {
            Debug.Log("//////////////     PRINTING NEW MODEL FOR YEAR " + year);
            foreach (var el in elementIndicesList)
                Debug.Log("YEAR " + year + ", INDEX = " + el);
        }

    }
}

