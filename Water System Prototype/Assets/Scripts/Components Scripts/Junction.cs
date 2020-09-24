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
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, baseDemand);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, elevation);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, pressure);

            Debug.Log("CALLED Junction::UpdatePropertiesValues()");
        }

        public override void UpdatePropertiesValues(List<float> values)
        {
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, values[2]);
        }
    }
}


