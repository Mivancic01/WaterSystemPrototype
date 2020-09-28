using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mainManager = MainSimulationManager;
using helper = MainSimulationManager.ComponentsHelper;

public partial class MainSimulationManager
{
    public class ComponentsManager : MonoBehaviour
    {
        public static void AddNodeComponent(GameObject component, int typeID = -1, int ID = -1, bool addToCurrentModel = false)
        {
            var componentScript = component.GetComponent<BaseElement>();

            if (ID == -1)
            {
                ID = helper.GetNextFreeID();
                componentScript.Initialize(typeID, ID);
            }
            else
                componentScript.Initialize();

            componentScript.UpdatePropertiesValues();

            var mainInstace = mainManager.Instance;

            mainInstace.componentsIdIndexMap.Add(ID, (mainInstace.componentsList.Count));
            mainInstace.componentsList.Add(componentScript);
            mainInstace.allIDs.Add(ID);

            if(addToCurrentModel)
                mainInstace.modelList[mainInstace.currentOpenModel].Add(ID);
        }

        public static void AddLineComponent(GameObject component, int typeID, int startNodeID, int endNodeID, int ID = -1, bool addToCurrentModel = false)
        {
            var componentScript = component.GetComponent<BaseElement>();
            if (ID == -1)
            {
                ID = helper.GetNextFreeID();
                componentScript.Initialize(typeID, ID);
            }
            else
                componentScript.Initialize();
            componentScript.UpdatePropertiesValues();

            AddNodeConnection(ID, startNodeID);
            AddNodeConnection(ID, endNodeID);

            var mainInstace = mainManager.Instance;
            mainInstace.allConnections.Add(ID, (startNodeID, endNodeID));

            mainInstace.componentsIdIndexMap.Add(ID, (mainInstace.componentsList.Count));
            mainInstace.componentsList.Add(componentScript);
            mainInstace.allIDs.Add(ID);

            if (addToCurrentModel)
                mainInstace.modelList[mainInstace.currentOpenModel].Add(ID);
        }

        private static void AddNodeConnection(int lineComponentID, int nodeID)
        {
            var mainInstace = mainManager.Instance;

            if (mainInstace.nodeConnections.ContainsKey(nodeID))
            {
                var list = mainInstace.nodeConnections[nodeID];
                if (!list.Contains(lineComponentID))
                    list.Add(lineComponentID);
                mainInstace.nodeConnections[nodeID] = list;
            }

            else
            {
                List<int> list = new List<int>();
                list.Add(lineComponentID);
                mainInstace.nodeConnections.Add(nodeID, list);
            }
        }

        public static void DeleteElement(int elementID)
        {
            var mainInstace = mainManager.Instance;
            BaseElement elem = mainInstace.componentsList[mainInstace.componentsIdIndexMap[elementID]];

            foreach (var model in mainInstace.modelList)
                model.RemoveElement(elementID);

            elem.DestroyElement();
            mainInstace.componentsList.Remove(elem);
        }
    }
}
