using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class Junction : BaseElement
    {
        private float baseDemand, elevation, pressure;

        public Junction(int id, int typeId, Vector3 pos, float pBaseDemand, float pElevation, float pPressure) : base(id, typeId, pos)
        {

            baseDemand = pBaseDemand;
            elevation = pElevation;
            pressure = pPressure;
        }
        public Junction(int id, int typeId, Vector3 pos) : base(id, typeId, pos)
        {

            baseDemand = 0.0f;
            elevation = 0.0f;
            pressure = 0.0f;
        }

        public override void UpdatePropertiesValues()
        {
            foreach (var txt in propertiesWindow.GetComponentsInChildren<Text>())
            {
                if (txt.gameObject.name.Equals("BD_Value"))
                    txt.text = baseDemand.ToString();

                else if (txt.gameObject.name.Equals("EL_Value"))
                    txt.text = elevation.ToString();

                else if (txt.gameObject.name.Equals("PR_Value"))
                    txt.text = pressure.ToString();
            }

            Debug.LogWarning("CALLED Junction::UpdatePropertiesValues()");
        }
    }
}


