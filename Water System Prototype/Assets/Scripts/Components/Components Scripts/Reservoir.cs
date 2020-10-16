using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reservoir : BaseElement
{
    public float totalHead;

    public Reservoir(int id, int typeId, float pTotalHead) : base(id, typeId, true)
    {
        totalHead = pTotalHead;
    }
    public Reservoir(int id, int typeId) : base(id, typeId, true)
    {
        totalHead = 0.0f;
    }

    public void Init(Reservoir reservoirScript)
    {
        totalHead = reservoirScript.totalHead;
    }

    public override void UpdatePropertiesValues()
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, totalHead);

        //Debug.Log("CALLED Reservoir::UpdatePropertiesValues()");
    }

    public override void UpdatePropertiesValues(List<float> values)
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
    }
}


