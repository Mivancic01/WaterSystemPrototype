using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MainSimulationManager : MonoBehaviour
{
    private List<BaseElement> componentsList;
    private List<string> allIDs;
    private Dictionary<string, int> componentsIdIndexMap;
    private Dictionary<string, (string First, string Second)> allConnections;
    private Dictionary<string, List<string>> nodeConnections;
    private List<Model> modelList;

    private const int defaultModelSize = 4;

    public static MainSimulationManager Instance { get; private set; }

    private int currentOpenModel;

    private void Awake()
    {
        componentsList = new List<BaseElement>();
        modelList = new List<Model>();
        componentsIdIndexMap = new Dictionary<string, int>();
        allConnections = new Dictionary<string, (string First, string Double)>();
        nodeConnections = new Dictionary<string, List<string>>();
        allIDs = new List<string>();
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

        if(modelList.Count == 0)
            for(int i = 0, startYear = 2020; i < defaultModelSize; i++)
                modelList.Add(new Model(startYear + 5*i));

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
