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

            //Debug.Log("startNodeID = " + startNodeID + ", endNodeID = " + endNodeID + ", lineID = " + ID + "\n " + Time.time);
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
                //Debug.Log("Adding another nodeConnection to an existing line. lineComponentID = " + lineComponentID + ", nodeID " + nodeID);
                var list = mainInstace.nodeConnections[nodeID];
                if (!list.Contains(lineComponentID))
                    list.Add(lineComponentID);
                mainInstace.nodeConnections[nodeID] = list;
            }

            else
            {
                //Debug.Log("Adding another nodeConnection to a new line. lineComponentID = " + lineComponentID + ", nodeID " + nodeID);
                List<int> list = new List<int>();
                list.Add(lineComponentID);
                mainInstace.nodeConnections.Add(nodeID, list);
            }
        }

        public static void DeleteElement(int componentID)
        {
            var mainInstace = mainManager.Instance;
            BaseElement component = mainInstace.componentsList[mainInstace.componentsIdIndexMap[componentID]];

            foreach (var model in mainInstace.modelList)
                model.RemoveElement(componentID);

            component.DestroyElement();
            mainInstace.componentsList.Remove(component);
        }

        public static void UpdateLinesPosition(int componentID)
        {
            foreach(var lineID in mainManager.Instance.nodeConnections[componentID])
            {
                //Debug.Log("USING lineID " + lineID + " with componentID " + componentID + " for the next update" + "\n" + Time.time);
                var startNodePosition = helper.GetComponentPosition(mainManager.Instance.allConnections[lineID].First);
                var endNodePosition = helper.GetComponentPosition(mainManager.Instance.allConnections[lineID].Second);

                var lineObject = mainManager.Instance.componentsList[mainManager.Instance.componentsIdIndexMap[lineID]].gameObject;
                LineGenerator.Instance.UpdateLinePosition(lineObject, startNodePosition, endNodePosition);
            }
        }
    }
}
