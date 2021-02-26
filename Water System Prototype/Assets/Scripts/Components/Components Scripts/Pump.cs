using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pump : BaseElement
{
    public string startNodeID, endNodeID;
    public float flow, flowVelocity;
    public int curveID;

    public Pump(string id, int typeId, string pStartNodeID, string pEndNodeID, float pFlow, float pFlowVelocity, int pCurveID) : base(id, typeId, false)
    {
        flow = pFlow;
        flowVelocity = pFlowVelocity;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
        curveID = pCurveID;
    }

    public Pump(string id, int typeId, string pStartNodeID, string pEndNodeID, int pCurveID) : base(id, typeId, false)
    {
        flow = 0.0f;
        flowVelocity = 0.0f;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
        curveID = pCurveID;
    }

    public void Init(Pump pumpScript)
    {

        ID = pumpScript.ID;
        typeID = pumpScript.typeID;

        startNodeID = pumpScript.startNodeID;
        endNodeID = pumpScript.endNodeID;

        flow = pumpScript.flow;
        flowVelocity = pumpScript.flowVelocity;
        curveID = pumpScript.curveID;

        isNodeComponent = false;
    }

    public override void UpdatePropertiesValues()
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, flow);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, flowVelocity);

        propertiesWindow.GetComponent<PropertiesWindow>().UpdateDropdowns(0, curveID);

        //Debug.Log("CALLED Pump::UpdatePropertiesValues()");
    }

    public override void UpdatePropertiesValues(List<float> values)
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
    }

    public string GetStartNodeID()
    {
        return startNodeID;
    }

    public string GetEndNodeID()
    {
        return endNodeID;
    }
}
