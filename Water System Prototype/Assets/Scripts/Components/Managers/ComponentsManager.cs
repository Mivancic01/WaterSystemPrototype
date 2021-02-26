using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using mainManager = MainSimulationManager;
using helper = MainSimulationManager.ComponentsHelper;

public partial class MainSimulationManager
{
    public class ComponentsManager : MonoBehaviour
    {
        public static void AddNodeComponent(GameObject component, int typeID = -1, string ID = "", bool addToCurrentModel = false)
        {
            var componentScript = component.GetComponent<BaseElement>();

            if (ID.Equals(""))
            {
                ID = helper.GetNextFreeID().ToString();
                Debug.Log("Adding Component with ID: " + ID);
                componentScript.Initialize(typeID, ID, true);
            }
            else
                componentScript.Initialize();

            var mainInstace = mainManager.Instance;

            mainInstace.componentsIdIndexMap.Add(ID, (mainInstace.componentsList.Count));
            mainInstace.componentsList.Add(componentScript);
            mainInstace.allIDs.Add(ID);

            if(addToCurrentModel)
                mainInstace.modelList[mainInstace.currentOpenModel].Add(ID);
        }

        public static void AddLineComponent(GameObject component, int typeID, string startNodeID, string endNodeID, string ID = "", bool addToCurrentModel = false)
        {
            var componentScript = component.GetComponent<BaseElement>();
            if (ID.Equals(""))
            {
                ID = helper.GetNextFreeID().ToString();
                componentScript.Initialize(typeID, ID, false);
            }
            else
                componentScript.Initialize();

            //componentScript.UpdatePropertiesValues();

            //Debug.Log("startNodeID = " + startNodeID + ", endNodeID = " + endNodeID + ", lineID = " + ID + "\n " + Time.time);
            AddNodeConnection(ID, startNodeID);
            AddNodeConnection(ID, endNodeID);

            var mainInstace = mainManager.Instance;
            mainInstace.allConnections.Add(ID, (startNodeID, endNodeID));

            //Debug.Log("Adding line component with ID: " + ID);

            mainInstace.componentsIdIndexMap.Add(ID, (mainInstace.componentsList.Count));
            mainInstace.componentsList.Add(componentScript);
            mainInstace.allIDs.Add(ID);

            if (addToCurrentModel)
                mainInstace.modelList[mainInstace.currentOpenModel].Add(ID);
        }

        private static void AddNodeConnection(string lineComponentID, string nodeID)
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
                List<string> list = new List<string>();
                list.Add(lineComponentID);
                mainInstace.nodeConnections.Add(nodeID, list);
            }
        }

        public static void DeleteElement(string componentID)
        {
            var mainInstace = mainManager.Instance;
            Debug.Log("Deleting component with ID: " + componentID);
            BaseElement component = mainInstace.componentsList[mainInstace.componentsIdIndexMap[componentID]];

            foreach (var model in mainInstace.modelList)
                model.RemoveElement(componentID);

            List<string> lines_to_remove = new List<string>();

            foreach (var connection in mainInstace.allConnections)
                if (connection.Value.First == componentID || connection.Value.Second == componentID)
                    //mainInstace.allConnections.Remove(connection.Key);
                    lines_to_remove.Add(connection.Key);

            for(int i = 0; i < lines_to_remove.Count; i++)
            {
                RemoveLineComponent(lines_to_remove[i]);
            }

            mainInstace.componentsList.Remove(component);
            mainInstace.allIDs.Remove(componentID);
            mainInstace.componentsIdIndexMap.Remove(componentID);
            mainInstace.nodeConnections.Remove(componentID);

            for (int i = 0; i < mainInstace.componentsList.Count; i++)
                mainInstace.componentsIdIndexMap[mainInstace.componentsList[i].ID] = i;


            component.DestroyElement();
        }

        public static void UpdateLinesPosition(string componentID)
        {
            if (!mainManager.Instance.nodeConnections.ContainsKey(componentID))
                return;

            foreach(var lineID in mainManager.Instance.nodeConnections[componentID])
            {
                //Debug.Log("USING lineID " + lineID + " with componentID " + componentID + " for the next update" + "\n" + Time.time);
                var startNodePosition = helper.GetComponentPosition(mainManager.Instance.allConnections[lineID].First);
                var endNodePosition = helper.GetComponentPosition(mainManager.Instance.allConnections[lineID].Second);

                var lineObject = mainManager.Instance.componentsList[mainManager.Instance.componentsIdIndexMap[lineID]].gameObject;
                LineGenerator.Instance.UpdateLinePosition(lineObject, startNodePosition, endNodePosition);
            }
        }

        public static void RemoveLineComponent(string componentID)
        {
            var mainInstace = mainManager.Instance;
            Debug.Log("Deleting component with ID: " + componentID);
            BaseElement component = mainInstace.componentsList[mainInstace.componentsIdIndexMap[componentID]];

            foreach (var model in mainInstace.modelList)
                model.RemoveElement(componentID);

            mainInstace.componentsList.Remove(component);


            mainInstace.nodeConnections[mainInstace.allConnections[componentID].First].Remove(componentID);
            mainInstace.nodeConnections[mainInstace.allConnections[componentID].Second].Remove(componentID);

            mainInstace.allConnections.Remove(componentID);
            mainInstace.allIDs.Remove(componentID);
            mainInstace.componentsIdIndexMap.Remove(componentID);

            for (int i = 0; i < mainInstace.componentsList.Count; i++)
                mainInstace.componentsIdIndexMap[mainInstace.componentsList[i].ID] = i;


            component.DestroyElement();
        }
    }
}
