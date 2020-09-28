using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mainManager = MainSimulationManager;

public partial class MainSimulationManager
{
    public class ComponentsHelper : MonoBehaviour
    {
        public static int GetNextFreeID()
        {
            for (int i = 0; true; i++)
                if (!mainManager.Instance.allIDs.Contains(i))
                    return i;
        }

        public static void UpdatePropertiesValues(int componentID, List<float> values)
        {
            mainManager.Instance.componentsList[mainManager.Instance.componentsIdIndexMap[componentID]].UpdatePropertiesValues(values);
        }

        public static Vector3 GetComponentPosition(int componentID)
        {
            return mainManager.Instance.componentsList[mainManager.Instance.componentsIdIndexMap[componentID]].GetPosition();
        }
    }
}
