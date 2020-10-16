using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pump : BaseElement
{
    public int startNodeID, endNodeID;
    public float flow, flowVelocity;
    public int curveID;

    public Pump(int id, int typeId, int pStartNodeID, int pEndNodeID, float pFlow, float pFlowVelocity, int pCurveID) : base(id, typeId, false)
    {
        flow = pFlow;
        flowVelocity = pFlowVelocity;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
        curveID = pCurveID;
    }

    public Pump(int id, int typeId, int pStartNodeID, int pEndNodeID, int pCurveID) : base(id, typeId, false)
    {
        flow = 0.0f;
        flowVelocity = 0.0f;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
        curveID = pCurveID;
    }

    public void Init(Pump pumpScript)
    {
        startNodeID = pumpScript.startNodeID;
        endNodeID = pumpScript.endNodeID;

        flow = pumpScript.flow;
        flowVelocity = pumpScript.flowVelocity;
        curveID = pumpScript.curveID;

        isNodeComponent = false;
    }

    public override void Initialize(int pTypeID = -1, int pID = -1, bool isNode = false)
    {
        if (pTypeID != -1)
            typeID = pTypeID;
        if (pID != -1)
            ID = pID;

        isNodeComponent = isNode;

        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetPathCreationState();

        //elemIcon = LineGenerator.Instance.CreateAndReturnLineComponent(MainSimulationManager.ComponentsManager.Instance.GetComponentPosition(startNodeID), ComponentsManager.Instance.GetComponentPosition(endNodeID), 1);
        gameObject.GetComponent<ComponentObject>().elementID = ID;

        propertiesWindow = ComponentsFactory.Instance.CreatePropertiesWindow(typeID);
        propertiesWindow.GetComponent<PropertiesWindow>().elementID = ID;
        ChangeWindowVisibility(false);

        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();
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

    public int GetStartNodeID()
    {
        return startNodeID;
    }

    public int GetEndNodeID()
    {
        return endNodeID;
    }
}
