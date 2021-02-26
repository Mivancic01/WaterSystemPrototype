using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsHelper : MonoBehaviour
{
    public PropertiesWindow junctionWindow, pipeWindow, pumpWindow,
        reservoirWindow, tankWindow, valveWindow;

    public static ComponentsHelper Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // destroy the duplicate
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateJunctionPropertiesWindow(Junction junction, string ID)
    {
        ResetVisibility();

        junctionWindow.UpdateInputField(0, junction.baseDemand);
        junctionWindow.UpdateInputField(1, junction.elevation);
        junctionWindow.GetComponent<PropertiesWindow>().UpdateInputField(2, junction.pressure);
        junctionWindow.elementID = ID;
        junctionWindow.gameObject.SetActive(true);
    }

    public void UpdatePipePropertiesWindow(Pipe pipe, string ID)
    {
        ResetVisibility();

        pipeWindow.UpdateInputField(0, pipe.length);
        pipeWindow.UpdateInputField(1, pipe.diameter);
        pipeWindow.UpdateInputField(2, pipe.flow);
        pipeWindow.UpdateInputField(3, pipe.flowVelocity);

        pipeWindow.UpdateDropdowns(0, pipe.statusID);
        pipeWindow.elementID = ID;
        pipeWindow.gameObject.SetActive(true);
    }

    public void UpdatePumpPropertiesWindow(Pump pump, string ID)
    {
        ResetVisibility();
        pumpWindow.UpdateInputField(0, pump.flow);
        pumpWindow.UpdateInputField(1, pump.flowVelocity);

        pumpWindow.UpdateDropdowns(0, pump.curveID);
        pumpWindow.elementID = ID;
        pumpWindow.gameObject.SetActive(true);
    }

    public void UpdateReservoirPropertiesWindow(Reservoir reservoir, string ID)
    {
        ResetVisibility();
        reservoirWindow.UpdateInputField(0, reservoir.totalHead);
        reservoirWindow.elementID = ID;
        reservoirWindow.gameObject.SetActive(true);
    }

    public void UpdateTankPropertiesWindow(Tank tank, string ID)
    {
        ResetVisibility();
        tankWindow.UpdateInputField(0, tank.volume);
        tankWindow.UpdateInputField(1, tank.elevation);
        tankWindow.elementID = ID;
        tankWindow.gameObject.SetActive(true);
    }

    public void UpdateValvePropertiesWindow(Valve valve, string ID)
    {
        ResetVisibility();
        valveWindow.UpdateInputField(0, valve.diameter);
        valveWindow.UpdateInputField(1, valve.flow);
        valveWindow.UpdateInputField(2, valve.flowVelocity);

        valveWindow.UpdateDropdowns(0, valve.statusID);
        valveWindow.UpdateDropdowns(2, valve.valveTypeID);
        valveWindow.elementID = ID;
        valveWindow.gameObject.SetActive(true);
    }

    private void ResetVisibility()
    {
        junctionWindow.gameObject.SetActive(false);
        pipeWindow.gameObject.SetActive(false);
        pumpWindow.gameObject.SetActive(false);
        reservoirWindow.gameObject.SetActive(false);
        tankWindow.gameObject.SetActive(false);
        valveWindow.gameObject.SetActive(false);
    }

}
