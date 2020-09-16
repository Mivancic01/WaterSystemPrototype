using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementsManager : MonoBehaviour
{

    public List<Elements.BaseElement> elementList;
    public List<Elements.Model> modelList;

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
        InitializeScene();

        var startTime = Time.time;
    }

    public void InitializeScene()
    {
        Debug.Log("CALLED ---> ElementsManager::InitializeScene()");
        foreach (var el in elementList)
            el.Initialize();

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
        Debug.Log("CALLED ---> ElementsManager::SwitchModels()");

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

    public void OpenPropertiesWindow(int index)
    {
        elementList[index].ChangeWindowVisibility(true);
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

    public void DeleteElement(int index)
    {
        Debug.Log("CALLED ---> ElementsManager::DeleteElement() with index = " + index + " and elementType = " + elementList[index].typeID);

        // modelList[0].RemoveElement(index);
        foreach (var el in modelList)
            el.RemoveElement(index);

        elementList[index].DestroyElement();
        elementList.RemoveAt(index);

        for (int i = index; i < elementList.Count; i++)
            elementList[i].UpdateListIndex(i);
    }

    public void AddElement(GameObject el, int typeID)
    {
        Debug.Log("CALLED ---> ElementsManager::AddElement() with index = " + elementList.Count + " and elementType = " + typeID);

        elementList.Add(new Elements.BaseElement(elementList.Count, typeID, el.transform.position));
        elementList[elementList.Count - 1].Initialize(el);
        modelList[currentOpenModel].Add(elementList.Count - 1);
    }
}
