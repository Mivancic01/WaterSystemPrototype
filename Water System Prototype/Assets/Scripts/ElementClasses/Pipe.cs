using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class Pipe : BaseElement
    {
        private int startNodeID, endNodeID;
        private float length, diameter, flow, flowVelocity;

        public Pipe(int id, int typeId, int pStartNodeID, int pEndNodeID, float pLength, float pDiameter, float pFlow, float pFlowVelocity) : base(id, typeId, new Vector3(0, 0, 0))
        {
            length = pLength;
            diameter = pDiameter;
            flow = pFlow;
            flowVelocity = pFlowVelocity;
            startNodeID = pStartNodeID;
            endNodeID = pEndNodeID;
        }
        public Pipe(int id, int typeId, int pStartNodeID, int pEndNodeID) : base(id, typeId, new Vector3(0, 0, 0))
        {
            length = 0.0f;
            diameter = 0.0f;
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
                ComponentsManager.Instance.GetComponentPosition(endNodeID), 0);
            elemIcon.GetComponent<ComponentObject>().elementID = ID;

            propertiesWindow = ComponentGameObjectsFactory.Instance.CreatePropertiesWindow(typeID);
            propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
            ChangeWindowVisibility(false);

            GameStateManager.Instance.SetInactiveState();
            GameStateManager.Instance.SetDragComponentsState();
        }

        public override void UpdatePropertiesValues()
        {
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, length);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, diameter);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, flow);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(3, flowVelocity);

            /*
            foreach (var txt in propertiesWindow.GetComponentsInChildren<Text>())
            {
                if (txt.gameObject.name.Equals("LN_Value"))
                {
                    Debug.Log("Updating LN_Value with " + length);
                    txt.text = length.ToString();
                }

                else if (txt.gameObject.name.Equals("DM_Value"))
                    txt.text = diameter.ToString();

                else if (txt.gameObject.name.Equals("FL_Value"))
                    txt.text = flow.ToString();

                else if (txt.gameObject.name.Equals("FLV_Value"))
                    txt.text = flowVelocity.ToString();
            }
            */

            Debug.Log("CALLED Pipe::UpdatePropertiesValues()");
        }

        public override void UpdatePropertiesValues(List<float> values)
        {
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, values[2]);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(3, values[3]);
        }
    }
}


