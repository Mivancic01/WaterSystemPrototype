using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessGenerator : MonoBehaviour
{
    public ProgressBar progressBarPanel;
    public GameObject simulationsPanel, processPanelPrefab;

    public bool useDebug = false;
    
    public void AddProcess()
    {
        if (useDebug) Debug.Log("CALLED ---> ProcessGenerator::AddProcess()");
        var newProcess = Instantiate(processPanelPrefab, simulationsPanel.transform);
        newProcess.GetComponent<ProcessPanel>().Initialize(progressBarPanel);
    }
}
