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
            Debug.Log("ADDING INDEX: " + index);
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
            //Debug.Log("STARTING REMOVEELEMENT()");
            //foreach (var elem in elementIndicesList)
            //    Debug.Log("EXISITNG ELEMENT: " + elem);

            //Debug.Log("REMOVING INDEX: " + index);

            for (int i = 0; i < elementIndicesList.Count; i++)
                if (elementIndicesList[i] == index)
                    elementIndicesList.RemoveAt(i);

            for (int i = 0; i < elementIndicesList.Count; i++)
                if (elementIndicesList[i] > index)
                    elementIndicesList[i] -= 1;
        }

        public void Print()
        {
            Debug.Log("//////////////     PRINTING NEW MODEL FOR YEAR " + year);
            foreach (var el in elementIndicesList)
                Debug.Log("YEAR " + year + ", INDEX = " + el);
        }

    }
}

