using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MainSimulationManager
{
    public class ComponentsHelper : MonoBehaviour
    {
        MainSimulationManager mainManager;
        public static ComponentsHelper Instance { get; private set; }
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

        public int GetNextFreeID()
        {
            for (int i = 0; true; i++)
            {
                if (!MainSimulationManager.Instance.allIDs.Contains(i))
                    return i;
            }
        }

        public void UpdatePropertiesValues(int componentID, List<float> values)
        {
            foreach (var el in mainManager.componentsList)
                if (el.ID == componentID)
                    el.UpdatePropertiesValues(values);
        }

        public Vector3 GetComponentPosition(int componentID)
        {
            foreach (var el in mainManager.componentsList)
                if (el.ID == componentID)
                    return el.GetPosition();

            Debug.LogError("THE COMPONENT DOES NOT EXIST!");
            return new Vector3(0, 0, 0);
        }

        public void OpenPropertiesWindow(int elementID)
        {
            foreach (var elem in mainManager.componentsList)
                if (elem.ID == elementID)
                    elem.ChangeWindowVisibility(true);
        }
    }
}
