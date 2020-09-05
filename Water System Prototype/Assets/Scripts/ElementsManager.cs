using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsManager : MonoBehaviour
{

    public List<Elements.BaseElement> elementList;
    public List<Elements.Model> modelList;

    public GameObject element1, element2, element3, element4, element5, element6;

    public static ElementsManager Instance { get; private set; }

    private int currentOpenModel;

    private void Awake()
    {
        elementList = new List<Elements.BaseElement>();
        modelList = new List<Elements.Model>();

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

    public void LoadSaveFile(List<Elements.BaseElement> elList, List<Elements.Model> modList)
    {
        DestroyScene();

        elementList = elList;
        modelList = modList;
        // InitializeScene();
    }

    public void InitializeScene()
    {
        foreach (var el in elementList)
            el.Initialize();

        for (int i = 1; i < modelList.Count; i++)
            modelList[i].ChangeListVisibility(false);

        currentOpenModel = 0;
    }

    public void SwitchModels(int newModelIndex)
    {
        modelList[currentOpenModel].ChangeListVisibility(false);
        modelList[newModelIndex].ChangeListVisibility(true);

        currentOpenModel = newModelIndex;
    }

    public void DestroyScene()
    {
        foreach (var el in elementList)
            el.DestroyElement();

        foreach (var el in modelList)
            el.DestroyModel();

        elementList.Clear();
        modelList.Clear();
    }
}
