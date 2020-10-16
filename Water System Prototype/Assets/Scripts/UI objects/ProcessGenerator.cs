using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessGenerator : MonoBehaviour
{
    public GameObject simulationsPanel, timeProcessPrefab, errorPanel;

    public bool useDebug = false;

    public void AddProcess()
    {
        errorPanel.SetActive(false);
        if (useDebug) Debug.Log("CALLED ---> ProcessGenerator::AddProcess()");

        if(MainSimulationManager.ComponentsHelper.IsSimulationViable())
        {
            var newProcess = Instantiate(timeProcessPrefab, simulationsPanel.transform);
            newProcess.GetComponent<TimeProcess>().Initialize();
        }
        else
            errorPanel.SetActive(true);
    }
}
