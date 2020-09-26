using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pump : BaseElement
{
    public int startNodeID, endNodeID;
    public float flow, flowVelocity;

    public Pump(int id, int typeId, int pStartNodeID, int pEndNodeID, float pFlow, float pFlowVelocity) : base(id, typeId)
    {
        flow = pFlow;
        flowVelocity = pFlowVelocity;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
    }

    public Pump(int id, int typeId, int pStartNodeID, int pEndNodeID) : base(id, typeId)
    {
        flow = 0.0f;
        flowVelocity = 0.0f;
        startNodeID = pStartNodeID;
        endNodeID = pEndNodeID;
    }

    public void Init(Pump pumpScript)
    {
        startNodeID = pumpScript.startNodeID;
        endNodeID = pumpScript.endNodeID;

        flow = pumpScript.flow;
        flowVelocity = pumpScript.flowVelocity;

    }

    public override void Initialize(int typeID = -1, int ID = -1)
    {
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

        Debug.Log("CALLED Pump::UpdatePropertiesValues()");
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
