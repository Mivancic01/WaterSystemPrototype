using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class Reservoir : BaseElement
    {
        private float totalHead;

        public Reservoir(int index, int id, Vector3 pos, float pTotalHead) : base(index, id, pos)
        {
            totalHead = pTotalHead;
        }

        public override void UpdatePropertiesValues()
        {
            foreach (var txt in propertiesWindow.GetComponentsInChildren<Text>())
                if (txt.gameObject.name.Equals("TH_Value"))
                    txt.text = totalHead.ToString();

            Debug.LogWarning("CALLED Reservoir::UpdatePropertiesValues()");
        }
    }
}


