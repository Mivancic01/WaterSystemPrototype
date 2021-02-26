using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Valve : BaseElement
{
    public string startNodeID, endNodeID;
    public float diameter, flow, flowVelocity;
    public int statusID, valveTypeID;

    public Valve(string id, int typeId, string pStartNodeID, string pEndNodeID, float pDiameter, float pFlow, float pFlowVelocity, int pStatusID, int pValveTypeID) : base(id, typeId, false)
    {
        diameter = pDiameter;
        flow = pFlow;
        flowVelocity = pFlowVelocity;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
        statusID = pStatusID;
        valveTypeID = pValveTypeID;
    }

    public Valve(string id, int typeId, string pStartNodeID, string pEndNodeID, int pStatusID, int pValveTypeID) : base(id, typeId, false)
    {
        diameter = 0.0f;
        flow = 0.0f;
        flowVelocity = 0.0f;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
        statusID = pStatusID;
        valveTypeID = pValveTypeID;
    }

    public void Init(Valve valveScript)
    {

        ID = valveScript.ID;
        typeID = valveScript.typeID;

        startNodeID = valveScript.startNodeID;
        endNodeID = valveScript.endNodeID;

        diameter = valveScript.diameter;
        flow = valveScript.flow;
        flowVelocity = valveScript.flowVelocity;
        statusID = valveScript.statusID;
        valveTypeID = valveScript.valveTypeID;

        isNodeComponent = false;
    }

    public override void UpdatePropertiesValues()
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, diameter);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, flow);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, flowVelocity);

        propertiesWindow.GetComponent<PropertiesWindow>().UpdateDropdowns(0, statusID);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateDropdowns(2, valveTypeID);

        //Debug.Log("CALLED Valve::UpdatePropertiesValues()");
    }

    public override void UpdatePropertiesValues(List<float> values)
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, values[2]);
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

