using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    public int year;
    public List<int> elementIDsList;
    MainSimulationManager.ComponentsManager componentsManager;

    private Model()
    {

    }

    public Model(int yr)
    {
        year = yr;
        elementIDsList = new List<int>();
        componentsManager = MainSimulationManager.ComponentsManager.Instance;
    }

    public void Add(int elementID)
    {
        //Debug.Log("ADDING INDEX: " + index + "For year: " + year);
        if (elementIDsList == null)
            elementIDsList = new List<int>();

        elementIDsList.Add(elementID);
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

    public List<int> GetComponentsList()
    {
        return elementIDsList;
    }

}
