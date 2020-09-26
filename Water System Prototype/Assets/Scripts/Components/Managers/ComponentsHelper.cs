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
            {
                if (!mainManager.Instance.allIDs.Contains(i))
                    return i;
            }
        }

        public static void UpdatePropertiesValues(int componentID, List<float> values)
        {
            foreach (var el in mainManager.Instance.componentsList)
                if (el.ID == componentID)
                    el.UpdatePropertiesValues(values);
        }

        public static Vector3 GetComponentPosition(int componentID)
        {
            foreach (var el in mainManager.Instance.componentsList)
                if (el.ID == componentID)
                    return el.GetPosition();

            Debug.LogError("THE COMPONENT DOES NOT EXIST!");
            return new Vector3(0, 0, 0);
        }

        public static void OpenPropertiesWindow(int elementID)
        {
            foreach (var elem in mainManager.Instance.componentsList)
                if (elem.ID == elementID)
                    elem.ChangeWindowVisibility(true);
        }
    }
}
