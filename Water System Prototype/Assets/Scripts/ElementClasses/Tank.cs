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

        public override void UpdatePropertiesValues()
        {
            foreach (var txt in propertiesWindow.GetComponentsInChildren<Text>())
            {
                if (txt.gameObject.name.Equals("VL_Value"))
                    txt.text = volume.ToString();

                else if (txt.gameObject.name.Equals("EL_Value"))
                    txt.text = elevation.ToString();
            }

            Debug.LogWarning("CALLED Tank::UpdatePropertiesValues()");
        }
    }
}


