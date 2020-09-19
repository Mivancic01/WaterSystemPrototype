using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class Model
    {
        public int year;
        public List<int> elementIDsList;
        ElementsManager elementsManager;

        private Model()
        {

        }

        public Model(int yr)
        {
            year = yr;
            elementIDsList = new List<int>();
            elementsManager = ElementsManager.Instance;
        }

        public void Add(int elementID)
        {
            //Debug.Log("ADDING INDEX: " + index + "For year: " + year);
            if (elementIDsList == null)
                elementIDsList = new List<int>();

            elementIDsList.Add(elementID);
        }

        public void ChangeListVisibility(bool isVisible)
        {
            //Debug.Log("CALLED ---> Model::ChangeListVisibility() with isVisisble = " + isVisible + " and year: " + year);
            foreach (var ID in elementIDsList)
                foreach(var elem in elementsManager.elementList)
                    if(elem.ID == ID)
                        elem.ChangeVisibility(isVisible);
        }

        public void DestroyModel()
        {
            elementIDsList.Clear();
        }

        public void RemoveElement(int elementID)
        {
            for (int i = 0; i < elementIDsList.Count; i++)
                if (elementIDsList[i] == elementID)
                    elementIDsList.RemoveAt(i);
        }

        public void Print()
        {
            Debug.Log("//////////////     PRINTING NEW MODEL FOR YEAR " + year);
            foreach (var ID in elementIDsList)
                Debug.Log("YEAR " + year + ", ID = " + ID);
        }

    }
}

