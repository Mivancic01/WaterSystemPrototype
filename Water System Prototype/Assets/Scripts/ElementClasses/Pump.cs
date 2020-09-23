using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class Pump : BaseElement
    {
        private int startNodeID, endNodeID;
        private float flow, flowVelocity;

        public Pump(int id, int typeId, int pStartNodeID, int pEndNodeID, float pFlow, float pFlowVelocity) : base(id, typeId, new Vector3(0, 0, 0))
        {
            flow = pFlow;
            flowVelocity = pFlowVelocity;
            startNodeID = pStartNodeID;
            endNodeID = pEndNodeID;
        }
        public Pump(int id, int typeId, int pStartNodeID, int pEndNodeID) : base(id, typeId, new Vector3(0, 0, 0))
        {
            flow = 0.0f;
            flowVelocity = 0.0f;
            startNodeID = pStartNodeID;
            endNodeID = pEndNodeID;
        }

        public override void Initialize()
        {
            GameStateManager.Instance.SetInactiveState();
            GameStateManager.Instance.SetPathCreationState();

            elemIcon = LineGenerator.Instance.CreateAndReturnLineComponent(ComponentsManager.Instance.GetComponentPosition(startNodeID),
                ComponentsManager.Instance.GetComponentPosition(endNodeID), 1);
            elemIcon.GetComponent<ComponentObject>().elementID = ID;

            propertiesWindow = ComponentGameObjectsFactory.Instance.CreatePropertiesWindow(typeID);
            propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
            ChangeWindowVisibility(false);

            GameStateManager.Instance.SetInactiveState();
            GameStateManager.Instance.SetDragComponentsState();
        }

        public override void UpdatePropertiesValues()
        {
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, flow);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, flowVelocity);

            /*
            foreach (var txt in propertiesWindow.GetComponentsInChildren<Text>())
            {
                if (txt.gameObject.name.Equals("FL_Value"))
                    txt.text = flow.ToString();

                else if (txt.gameObject.name.Equals("FLV_Value"))
                    txt.text = flowVelocity.ToString();
            }
            */

            Debug.Log("CALLED Pump::UpdatePropertiesValues()");
        }

        public override void UpdatePropertiesValues(List<float> values)
        {
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
        }
    }
}

