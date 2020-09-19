using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class Pump : BaseElement
    {
        private float flow, flowVelocity;

        public Pump(int index, int id, Vector3 pos, float pFlow, float pFlowVelocity) : base(index, id, pos)
        {
            flow = pFlow;
            flowVelocity = pFlowVelocity;
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

