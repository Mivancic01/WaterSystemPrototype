using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using mainManager = MainSimulationManager;

public partial class MainSimulationManager
{
    public class ModelsManager : MonoBehaviour
    {
        public static void AddModel(Model model)
        {
            mainManager.Instance.modelList.Add(model);
        }

        public static void SwitchModels(int newModelIndex)
        {
            var mainInstace = mainManager.Instance;

            var oldModelIDsList = mainInstace.modelList[mainInstace.currentOpenModel].GetComponentsList();
            var newModelIDsList = mainInstace.modelList[newModelIndex].GetComponentsList();

            SwitchComponentsVisibility(oldModelIDsList, false);
            SwitchComponentsVisibility(newModelIDsList, true);

            mainInstace.currentOpenModel = newModelIndex;
        }

        public static void SwitchComponentsVisibility(List<int> IDsList, bool isVisible)
        {
            foreach (var id in IDsList)
                mainManager.Instance.componentsList[mainManager.Instance.componentsIdIndexMap[id]].ChangeVisibility(isVisible);
        }

        private static int GetModelIndexFromScrollbar()
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

