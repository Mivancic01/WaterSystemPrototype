using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    public int year;
    public List<string> elementIDsList;
    MainSimulationManager.ComponentsManager componentsManager;

    private Model()
    {

    }

    public Model(int yr)
    {
        year = yr;
        elementIDsList = new List<string>();
    }

    public void Add(string elementID)
    {
        //Debug.Log("ADDING INDEX: " + index + "For year: " + year);
        if (elementIDsList == null)
            elementIDsList = new List<string>();

        elementIDsList.Add(elementID);
    }

    public void DestroyModel()
    {
        elementIDsList.Clear();
    }

    public void RemoveElement(string elementID)
    {
        for (int i = 0; i < elementIDsList.Count; i++)
            if (elementIDsList[i].Equals(elementID))
                elementIDsList.RemoveAt(i);
    }

    public List<string> GetComponentsList()
    {
        return elementIDsList;
    }

}
