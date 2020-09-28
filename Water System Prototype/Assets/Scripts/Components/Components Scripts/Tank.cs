using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : BaseElement
{
    public float volume, elevation;

    public Tank(int id, int typeId, float pVolume, float pElevation) : base(id, typeId)
    {
        volume = pVolume;
        elevation = pElevation;
    }
    public Tank(int id, int typeId) : base(id, typeId)
    {
        volume = 0.0f;
        elevation = 0.0f;
    }

    public void Init(Tank tankScript)
    {
        volume = tankScript.volume;
        elevation = tankScript.elevation;
    }

    public override void UpdatePropertiesValues()
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, volume);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, elevation);

        //Debug.Log("CALLED Tank::UpdatePropertiesValues()");
    }

    public override void UpdatePropertiesValues(List<float> values)
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
    }
}

