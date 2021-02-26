using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipe : BaseElement
{
    public string startNodeID, endNodeID;
    public float length, diameter, flow, flowVelocity;
    public int statusID;

    public Pipe(string id, int typeId, string pStartNodeID, string pEndNodeID, float pLength, float pDiameter, float pFlow, float pFlowVelocity, int pStatusID) : base(id, typeId, false)
    {
        length = pLength;
        diameter = pDiameter;
        flow = pFlow;
        flowVelocity = pFlowVelocity;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
        statusID = pStatusID;
    }
    public Pipe(string id, int typeId, string pStartNodeID, string pEndNodeID, int pStatusID) : base(id, typeId, false)
    {
        length = 0.0f;
        diameter = 0.0f;
        flow = 0.0f;
        flowVelocity = 0.0f;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
        statusID = pStatusID;
    }

    public void Init(Pipe pipeScript)
    {

        ID = pipeScript.ID;
        typeID = pipeScript.typeID;

        startNodeID = pipeScript.startNodeID;
        endNodeID = pipeScript.endNodeID;

        length = pipeScript.length;
        diameter = pipeScript.diameter;
        flow = pipeScript.flow;
        flowVelocity = pipeScript.flowVelocity;
        statusID = pipeScript.statusID;

        isNodeComponent = false;
    }

    public override void UpdatePropertiesValues()
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, length);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, diameter);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, flow);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(3, flowVelocity);

        propertiesWindow.GetComponent<PropertiesWindow>().UpdateDropdowns(0, statusID);

        //Debug.Log("CALLED Pipe::UpdatePropertiesValues()");
    }

    public override void UpdatePropertiesValues(List<float> values)
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, values[2]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(3, values[3]);
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

