using Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentsManager : MonoBehaviour
{
    public List<Elements.BaseElement> componentsList;
    public List<int> allIDs;
    public Dictionary<int, int> nodeComponentsListIDs, lineComponentsListIDs;
    public Dictionary<int, (int First, int Double)> allConnections;
    public Dictionary<int, List<int>> nodeConnections;

    public List<Elements.Model> modelList;

    public static ComponentsManager Instance { get; private set; }

    private int currentOpenModel;

    private void Awake()
    {
        componentsList = new List<Elements.BaseElement>();
        modelList = new List<Elements.Model>();
        nodeComponentsListIDs = new Dictionary<int, int>();
        lineComponentsListIDs = new Dictionary<int, int>();
        allConnections = new Dictionary<int, (int First, int Double)>();
        nodeConnections = new Dictionary<int, List<int>>();
        allIDs = new List<int>();

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

    public void AddNodeComponent(BaseElement nodeComponent)
    {
        componentsList.Add(nodeComponent);
        nodeComponentsListIDs.Add(nodeComponent.ID, componentsList.Count - 1);
        allIDs.Add(nodeComponent.ID);
    }

    public void AddLineComponent(BaseElement lineComponent, int startNodeID, int endNodeID)
    {
        componentsList.Add(lineComponent);
        lineComponentsListIDs.Add(lineComponent.ID, componentsList.Count - 1);
        allConnections.Add(lineComponent.ID, (startNodeID, endNodeID));
        allIDs.Add(lineComponent.ID);
    }

    private void AddNodeConnection(int lineComponentID, int nodeID)
    {
        if (nodeConnections.ContainsKey(nodeID))
        {
            var list = nodeConnections[nodeID];
            if (!list.Contains(lineComponentID))
                list.Add(lineComponentID);
            nodeConnections[nodeID] = list;
        }

        else
        {
            List<int> list = new List<int>();
            list.Add(lineComponentID);
            nodeConnections.Add(nodeID, list);
        }
    }

    public void AddNodeComponent(GameObject el, int typeID)
    {
        int ID = GetNextFreeID();

        var componentScript = ComponentsFactory.CreateNodeComponent(ID, typeID, el.transform.position);
        componentScript.Initialize(el);
        componentScript.UpdatePropertiesValues();

        componentsList.Add(componentScript);
        nodeComponentsListIDs.Add(ID, (componentsList.Count - 1));
        modelList[currentOpenModel].Add(ID);
        allIDs.Add(ID);
    }

    public void AddLineComponent(GameObject el, int typeID, int startNodeID, int endNodeID)
    {
        int ID = GetNextFreeID();

        var componentScript = ComponentsFactory.CreateLineComponent(componentsList.Count, typeID, startNodeID, endNodeID);
        componentScript.Initialize(el);
        componentScript.UpdatePropertiesValues();

        componentsList.Add(componentScript);
        lineComponentsListIDs.Add(ID, (componentsList.Count - 1));
        modelList[currentOpenModel].Add(componentsList.Count - 1);
        allIDs.Add(ID);
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
        allConnections.Clear();
        nodeConnections.Clear();
        allIDs.Clear();
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

    private int GetNextFreeID()
    {
        for(int i = 0; true; i++)
        {
            if (!allIDs.Contains(i))
                return i;
        }
    }
}
