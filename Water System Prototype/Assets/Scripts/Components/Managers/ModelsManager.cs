using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MainSimulationManager
{
    public class ModelsManager : MonoBehaviour
    {
        MainSimulationManager mainManager;
        public static ModelsManager Instance { get; private set; }
        private void Awake()
        {
            mainManager = MainSimulationManager.Instance;
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

        public void AddModel(Model model)
        {
            mainManager.modelList.Add(model);
        }

        public void SwitchModels(int newModelIndex)
        {
            var oldModelIDsList = mainManager.modelList[mainManager.currentOpenModel].GetComponentsList();
            var newModelIDsList = mainManager.modelList[newModelIndex].GetComponentsList();

            SwitchComponentsVisibility(oldModelIDsList, false);
            SwitchComponentsVisibility(newModelIDsList, true);

            mainManager.currentOpenModel = newModelIndex;
        }

        public void SwitchComponentsVisibility(List<int> IDsList, bool isVisible)
        {
            foreach (var id in IDsList)
                mainManager.componentsList[mainManager.componentsIdIndexMap[id]].ChangeVisibility(isVisible);
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
    }
}

