﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Valve : BaseElement
{
    public int startNodeID, endNodeID;
    public float diameter, flow, flowVelocity;

    public Valve(int id, int typeId, int pStartNodeID, int pEndNodeID, float pDiameter, float pFlow, float pFlowVelocity) : base(id, typeId)
    {
        diameter = pDiameter;
        flow = pFlow;
        flowVelocity = pFlowVelocity;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
    }

    public Valve(int id, int typeId, int pStartNodeID, int pEndNodeID) : base(id, typeId)
    {
        diameter = 0.0f;
        flow = 0.0f;
        flowVelocity = 0.0f;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
    }

    public void Init(Valve valveScript)
    {
        startNodeID = valveScript.startNodeID;
        endNodeID = valveScript.endNodeID;

        diameter = valveScript.diameter;
        flow = valveScript.flow;
        flowVelocity = valveScript.flowVelocity;
    }

    public override void Initialize(int pTypeID = -1, int pID = -1)
    {
        if (pTypeID != -1)
            typeID = pTypeID;
        if (pID != -1)
            ID = pID;

        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetPathCreationState();

       // elemIcon = LineGenerator.Instance.CreateAndReturnLineComponent(ComponentsManager.Instance.GetComponentPosition(startNodeID), ComponentsManager.Instance.GetComponentPosition(endNodeID), 2);
        gameObject.GetComponent<ComponentObject>().elementID = ID;

        propertiesWindow = ComponentsFactory.Instance.CreatePropertiesWindow(typeID);
        propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
        ChangeWindowVisibility(false);

        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();
    }

    public override void UpdatePropertiesValues()
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, diameter);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, flow);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, flowVelocity);

        //Debug.Log("CALLED Valve::UpdatePropertiesValues()");
    }

    public override void UpdatePropertiesValues(List<float> values)
    {
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(0, values[0]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(1, values[1]);
        propertiesWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, values[2]);
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

