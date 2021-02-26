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
                if (!mainManager.Instance.allIDs.Contains(i.ToString()))
                    return i;
        }

        public static void UpdatePropertiesValues(string componentID, List<float> values)
        {
            mainManager.Instance.componentsList[mainManager.Instance.componentsIdIndexMap[componentID]].UpdatePropertiesValues(values);
        }

        public static Vector3 GetComponentPosition(string componentID)
        {
            return mainManager.Instance.componentsList[mainManager.Instance.componentsIdIndexMap[componentID]].GetPosition();
        }

        public static (string, string) GetNodeIDsFromLineObject(GameObject line, int typeID)
        {

            if (typeID == 1)
                return (line.GetComponent<Pipe>().startNodeID, line.GetComponent<Pipe>().endNodeID);

            else if (typeID == 2)
                return (line.GetComponent<Pump>().startNodeID, line.GetComponent<Pump>().endNodeID);

            else if (typeID == 5)
                return (line.GetComponent<Valve>().startNodeID, line.GetComponent<Valve>().endNodeID);

            Debug.LogError("OBJECT IS NOT A LINE!");
            return ("", "");
        }

        public static bool IsSimulationViable()
        {
            foreach (var component in mainManager.Instance.componentsList)
                if (component.isNodeComponent && !mainManager.Instance.nodeConnections.ContainsKey(component.ID))
                    return false;

            return true;
        }
    }
}
