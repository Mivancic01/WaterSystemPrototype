using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elements
{
    public class Tank : BaseElement
    {
        private float volume, elevation;

        public Tank(int id, int typeId, Vector3 pos, float pVolume, float pElevation) : base(id, typeId, pos)
        {
            volume = pVolume;
            elevation = pElevation;
        }
        public Tank(int id, int typeId, Vector3 pos) : base(id, typeId, pos)
        {
            volume = 0.0f;
            elevation = 0.0f;
        }

        public override void UpdatePropertiesValues()
        {
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, volume);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, elevation);
            /*
            foreach (var txt in propertiesWindow.GetComponentsInChildren<Text>())
            {
                if (txt.gameObject.name.Equals("VL_Value"))
                    txt.text = volume.ToString();

                else if (txt.gameObject.name.Equals("EL_Value"))
                    txt.text = elevation.ToString();
            }
            */

            Debug.Log("CALLED Tank::UpdatePropertiesValues()");
        }

        public override void UpdatePropertiesValues(List<float> values)
        {
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
            propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
        }
    }
}


