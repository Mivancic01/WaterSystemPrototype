using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Junction : BaseElement
{
    public float baseDemand, elevation, pressure;

    public Junction(int id, int typeId, float pBaseDemand, float pElevation, float pPressure) : base(id, typeId, true)
    {

        baseDemand = pBaseDemand;
        elevation = pElevation;
        pressure = pPressure;
    }
    public Junction(int id, int typeId) : base(id, typeId, true)
    {

        baseDemand = 0.0f;
        elevation = 0.0f;
        pressure = 0.0f;
    }

    public void Init(Junction junctionScript)
    {
        baseDemand = junctionScript.baseDemand;
        elevation = junctionScript.elevation;
        pressure = junctionScript.pressure;
    }

    public override void UpdatePropertiesValues()
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, baseDemand);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, elevation);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, pressure);

        //Debug.Log("CALLED Junction::UpdatePropertiesValues()");
    }

    public override void UpdatePropertiesValues(List<float> values)
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, values[2]);
    }
}


