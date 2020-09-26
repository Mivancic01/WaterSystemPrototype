using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MainSimulationManager : MonoBehaviour
{
    public List<BaseElement> componentsList;
    public List<int> allIDs;
    public Dictionary<int, int> componentsIdIndexMap;
    public Dictionary<int, (int First, int Double)> allConnections;
    public Dictionary<int, List<int>> nodeConnections;
    public List<Model> modelList;

    public ComponentsHelper helper;
    public ModelsManager modelsManager;
    public ComponentsManager componentsManager;

    public static MainSimulationManager Instance { get; private set; }

    private int currentOpenModel;

    private void Awake()
    {
        componentsList = new List<BaseElement>();
        modelList = new List<Model>();
        componentsIdIndexMap = new Dictionary<int, int>();
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

    public void InitializeScene()
    {
        for (int i = 0; i < modelList.Count; i++)
            modelsManager.SwitchComponentsVisibility(modelList[i].GetComponentsList(), false);

        modelsManager.SwitchComponentsVisibility(modelList[9].GetComponentsList(), true);

        currentOpenModel = 0;
    }

    public void DestroyScene()
    {
        foreach (var el in componentsList)
            el.DestroyElement();

        foreach (var el in modelList)
            el.DestroyModel();

        componentsList.Clear();
        modelList.Clear();
        componentsIdIndexMap.Clear();
        allConnections.Clear();
        nodeConnections.Clear();
        allIDs.Clear();
    }
}
