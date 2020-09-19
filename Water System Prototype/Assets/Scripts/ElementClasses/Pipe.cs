using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class Pipe : BaseElement
    {
        private float length, diameter, flow, flowVelocity;

        public Pipe(int id, int typeId, Vector3 pos, float pLength, float pDiameter, float pFlow, float pFlowVelocity) : base(id, typeId, pos)
        {
            length = pLength;
            diameter = pDiameter;
            flow = pFlow;
            flowVelocity = pFlowVelocity;
        }
        public Pipe(int id, int typeId, Vector3 pos) : base(id, typeId, pos)
        {
            length = 0.0f;
            diameter = 0.0f;
            flow = 0.0f;
            flowVelocity = 0.0f;
        }

        public override void UpdatePropertiesValues()
        {
            foreach (var txt in propertiesWindow.GetComponentsInChildren<Text>())
            {
                if (txt.gameObject.name.Equals("LN_Value"))
                    txt.text = length.ToString();

                else if (txt.gameObject.name.Equals("DM_Value"))
                    txt.text = diameter.ToString();

                else if (txt.gameObject.name.Equals("FL_Value"))
                    txt.text = flow.ToString();

                else if (txt.gameObject.name.Equals("FLV_Value"))
                    txt.text = flowVelocity.ToString();
            }

            Debug.LogWarning("CALLED Pipe::UpdatePropertiesValues()");
        }
    }
}


