using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class Reservoir : BaseElement
    {
        private float totalHead;

        public Reservoir(int id, int typeId, Vector3 pos, float pTotalHead) : base(id, typeId, pos)
        {
            totalHead = pTotalHead;
        }
        public Reservoir(int id, int typeId, Vector3 pos) : base(id, typeId, pos)
        {
            totalHead = 0.0f;
        }

        public override void UpdatePropertiesValues()
        {
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, totalHead);
            /*
            foreach (var txt in propertiesWindow.GetComponentsInChildren<Text>())
                if (txt.gameObject.name.Equals("TH_Value"))
                    txt.text = totalHead.ToString();
            */

            Debug.Log("CALLED Reservoir::UpdatePropertiesValues()");
        }

        public override void UpdatePropertiesValues(List<float> values)
        {
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
        }
    }
}


