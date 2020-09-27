using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MainSimulationManager : MonoBehaviour
{
    private List<BaseElement> componentsList;
    private List<int> allIDs;
    private Dictionary<int, int> componentsIdIndexMap;
    private Dictionary<int, (int First, int Double)> allConnections;
    private Dictionary<int, List<int>> nodeConnections;
    private List<Model> modelList;

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
        currentOpenModel = 0;

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
            ModelsManager.SwitchComponentsVisibility(modelList[i].GetComponentsList(), false);

        ModelsManager.SwitchComponentsVisibility(modelList[0].GetComponentsList(), true);

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
