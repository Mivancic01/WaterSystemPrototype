using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class Pump : BaseElement
    {
        private float flow, flowVelocity;

        public Pump(int id, int typeId, Vector3 pos, float pFlow, float pFlowVelocity) : base(id, typeId, pos)
        {
            flow = pFlow;
            flowVelocity = pFlowVelocity;
        }
        public Pump(int id, int typeId, Vector3 pos) : base(id, typeId, pos)
        {
            flow = 0.0f;
            flowVelocity = 0.0f;
        }

        public override void UpdatePropertiesValues()
        {
            foreach (var txt in propertiesWindow.GetComponentsInChildren<Text>())
            {
                if (txt.gameObject.name.Equals("FL_Value"))
                    txt.text = flow.ToString();

                else if (txt.gameObject.name.Equals("FLV_Value"))
                    txt.text = flowVelocity.ToString();
            }

            Debug.LogWarning("CALLED Pump::UpdatePropertiesValues()");
        }
    }
}

