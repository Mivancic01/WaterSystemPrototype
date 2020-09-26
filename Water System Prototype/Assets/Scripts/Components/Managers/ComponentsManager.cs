using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MainSimulationManager
{
    public class ComponentsManager : MonoBehaviour
    {
        MainSimulationManager mainManager;
        public ComponentsHelper helper;

        public static ComponentsManager Instance { get; private set; }
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

        public void AddNodeComponent(GameObject component, int typeID = -1, int ID = -1)
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

            mainManager.componentsList.Add(componentScript);
            mainManager.componentsIdIndexMap.Add(ID, (mainManager.componentsList.Count - 1));
            mainManager.modelList[mainManager.currentOpenModel].Add(ID);
            mainManager.allIDs.Add(ID);
        }

        public void AddLineComponent(GameObject component, int typeID, int startNodeID, int endNodeID, int ID = -1)
        {
            var componentScript = component.GetComponent<BaseElement>();
            if (ID == -1)
            {
                ID = helper.GetNextFreeID();
                componentScript.Initialize(typeID, ID);
            }
            else
                componentScript.Initialize();

            //if (ID == -1)
            //    ID = helper.GetNextFreeID();

            AddNodeConnection(ID, startNodeID);
            AddNodeConnection(ID, endNodeID);
            mainManager.allConnections.Add(ID, (startNodeID, endNodeID));

            componentScript.Initialize();
            componentScript.UpdatePropertiesValues();

            mainManager.componentsList.Add(componentScript);
            mainManager.componentsIdIndexMap.Add(ID, (mainManager.componentsList.Count - 1));
            mainManager.modelList[mainManager.currentOpenModel].Add(mainManager.componentsList.Count - 1);
            mainManager.allIDs.Add(ID);
        }

        private void AddNodeConnection(int lineComponentID, int nodeID)
        {
            if (mainManager.nodeConnections.ContainsKey(nodeID))
            {
                var list = mainManager.nodeConnections[nodeID];
                if (!list.Contains(lineComponentID))
                    list.Add(lineComponentID);
                mainManager.nodeConnections[nodeID] = list;
            }

            else
            {
                List<int> list = new List<int>();
                list.Add(lineComponentID);
                mainManager.nodeConnections.Add(nodeID, list);
            }
        }

        public void DeleteElement(int elementID)
        {
            BaseElement elem = null;
            foreach (var el in mainManager.componentsList)
                if (el.ID == elementID)
                {
                    elem = el;
                    break;
                }

            if (elem == null)
                return;

            foreach (var model in mainManager.modelList)
                model.RemoveElement(elementID);

            elem.DestroyElement();
            mainManager.componentsList.Remove(elem);
        }
    }
}
