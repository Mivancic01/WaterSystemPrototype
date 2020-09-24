using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessGenerator : MonoBehaviour
{
    public GameObject simulationsPanel, timeProcessPrefab;

    public bool useDebug = false;

    public void AddProcess()
    {
        if (useDebug) Debug.Log("CALLED ---> ProcessGenerator::AddProcess()");
        var newProcess = Instantiate(timeProcessPrefab, simulationsPanel.transform);
        newProcess.GetComponent<TimeProcess>().Initialize();
    }
}
