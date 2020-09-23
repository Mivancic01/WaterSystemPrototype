using Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentsManager : MonoBehaviour
{
    public List<Elements.BaseElement> componentsList;
    public Dictionary<int, int> nodeComponentsListIDs, lineComponentsListIDs;

    public List<Elements.Model> modelList;

    public static ComponentsManager Instance { get; private set; }

    private int currentOpenModel;

    private void Awake()
    {
        componentsList = new List<Elements.BaseElement>();
        modelList = new List<Elements.Model>();
        nodeComponentsListIDs = new Dictionary<int, int>();
        lineComponentsListIDs = new Dictionary<int, int>();

        if (Instance != null && Instance != this)
        {
            // destroy the duplicate
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddComponent(BaseElement component)
    {
        componentsList.Add(component);

        if (component.typeID == 0 || component.typeID == 3 || component.typeID == 4)
            nodeComponentsListIDs.Add(component.ID, componentsList.Count - 1);

        else if(component.typeID == 1 || component.typeID == 2 || component.typeID == 5)
            lineComponentsListIDs.Add(component.ID, componentsList.Count - 1);
    }

    public void AddComponent(GameObject el, int typeID)
    {
        Debug.Log("CALLED ---> ComponentsManager::AddElement() with index = " + componentsList.Count + " and elementType = " + typeID);

        if (typeID == 0 || typeID == 3 || typeID == 4)
            componentsList.Add(ComponentsFactory.CreateNodeComponent(componentsList.Count, typeID, el.transform.position));

        componentsList[componentsList.Count - 1].Initialize(el);
        componentsList[componentsList.Count - 1].UpdatePropertiesValues();
        modelList[currentOpenModel].Add(componentsList.Count - 1);
    }

    public void AddModel(Model model)
    {
        modelList.Add(model);
    }

    public void InitializeScene()
    {
        // Debug.Log("CALLED ---> ComponentsManager::InitializeScene()");
        foreach (var el in componentsList)
        {
            el.Initialize();
            el.UpdatePropertiesValues();
        }

        for (int i = 0; i < modelList.Count; i++)
            modelList[i].ChangeListVisibility(false);

        modelList[0].ChangeListVisibility(true);

        currentOpenModel = 0;
    }

    public void SwitchModels(int newModelIndex)
    {
        modelList[currentOpenModel].ChangeListVisibility(false);
        modelList[newModelIndex].ChangeListVisibility(true);

        currentOpenModel = newModelIndex;
    }

    public void SwitchModels()
    {
        Debug.Log("CALLED ---> ComponentsManager::SwitchModels()");

        int newModelIndex = GetModelIndexFromScrollbar();
        modelList[currentOpenModel].ChangeListVisibility(false);
        modelList[newModelIndex].ChangeListVisibility(true);

        currentOpenModel = newModelIndex;
    }

    private int GetModelIndexFromScrollbar()
    {
        float value = GameObject.FindWithTag("Timeline").GetComponent<Scrollbar>().value;

        if (value < 0.25f)
            return 0;
        else if (value < 0.5f)
            return 1;
        else if (value < 0.75f)
            return 2;
        else if (value <= 1.0f)
            return 3;

        Debug.LogError("INVALID SCROLLBAR VALUE!");
        return 3;
    }

    public void OpenPropertiesWindow(int elementID)
    {
        foreach(var elem in componentsList)
            if(elem.ID == elementID)
                elem.ChangeWindowVisibility(true);
    }

    public void DestroyScene()
    {
        foreach (var el in componentsList)
            el.DestroyElement();

        foreach (var el in modelList)
            el.DestroyModel();

        componentsList.Clear();
        modelList.Clear();
        nodeComponentsListIDs.Clear();
        lineComponentsListIDs.Clear();
    }

    public void DeleteElement(int elementID)
    {
        Elements.BaseElement elem = null;
        foreach(var el in componentsList)
            if (el.ID == elementID)
            {
                elem = el;
                break;
            }
        
        if(elem == null)
        {
            Debug.LogError("Element for deletion doesent exist in the list!");
            return;
        }

        Debug.Log("CALLED ---> ComponentsManager::DeleteElement() with index = " + elementID + " and elementType = " + elem.typeID);

        foreach (var el in modelList)
            el.RemoveElement(elementID);

        elem.DestroyElement();
        componentsList.Remove(elem);

        //for (int i = index; i < componentsList.Count; i++)
        //    componentsList[i].UpdateListIndex(i);
    }

    public Vector3 GetComponentPosition(int componentID)
    {
        foreach (var el in componentsList)
            if (el.ID == componentID)
                return el.GetPosition();

        Debug.LogError("THE COMPONENT DOES NOT EXIST!");
        return new Vector3(0, 0, 0);
    }

    public void UpdatePropertiesValues(int componentID, List<float> values)
    {
        foreach (var el in componentsList)
            if (el.ID == componentID)
                el.UpdatePropertiesValues(values);
    }
}
