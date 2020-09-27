using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipe : BaseElement
{
    public int startNodeID, endNodeID;
    public float length, diameter, flow, flowVelocity;

    public Pipe(int id, int typeId, int pStartNodeID, int pEndNodeID, float pLength, float pDiameter, float pFlow, float pFlowVelocity) : base(id, typeId)
    {
        length = pLength;
        diameter = pDiameter;
        flow = pFlow;
        flowVelocity = pFlowVelocity;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
    }
    public Pipe(int id, int typeId, int pStartNodeID, int pEndNodeID) : base(id, typeId)
    {
        length = 0.0f;
        diameter = 0.0f;
        flow = 0.0f;
        flowVelocity = 0.0f;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
    }

    public void Init(Pipe pipeScript)
    {
        startNodeID = pipeScript.startNodeID;
        endNodeID = pipeScript.endNodeID;

        length = pipeScript.length;
        diameter = pipeScript.diameter;
        flow = pipeScript.flow;
        flowVelocity = pipeScript.flowVelocity;

    }

    public override void Initialize(int pTypeID = -1, int pID = -1)
    {
        if (pTypeID != -1)
            typeID = pTypeID;
        if (pID != -1)
            ID = pID;

        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetPathCreationState();

        //elemIcon = LineGenerator.Instance.CreateAndReturnLineComponent(ComponentsManager.Instance.GetComponentPosition(startNodeID), ComponentsManager.Instance.GetComponentPosition(endNodeID), 0);
        gameObject.GetComponent<ComponentObject>().elementID = ID;

        propertiesWindow = ComponentsFactory.Instance.CreatePropertiesWindow(typeID);
        propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
        ChangeWindowVisibility(false);

        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();
    }

    public override void UpdatePropertiesValues()
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, length);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, diameter);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, flow);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(3, flowVelocity);

        Debug.Log("CALLED Pipe::UpdatePropertiesValues()");
    }

    public override void UpdatePropertiesValues(List<float> values)
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, values[2]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(3, values[3]);
    }

    public int GetStartNodeID()
    {
        return startNodeID;
    }

    public int GetEndNodeID()
    {
        return endNodeID;
    }
}

