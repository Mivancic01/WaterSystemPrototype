using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessPanel : MonoBehaviour
{
    private ProgressBar progressBarObj;
    float processTime, startTime, deltaBarTime, timeFromLastAddedBar;
    int processID;
    static int processCounter = 0;
    static List<ProcessPanel> runningProcesses;

    public bool useDebug = false;

    public void Initialize(ProgressBar bar)
    {
        if (useDebug) Debug.Log("CALLED ---> ProcessPanel::Initialize()");

        progressBarObj = bar;
        progressBarObj.AddProcess();

        processTime = Random.Range(5, 25);
        startTime = Time.time;

        timeFromLastAddedBar = 0;
        deltaBarTime = processTime / 20.0f;
        processID = processCounter++;

        if (runningProcesses == null)
            runningProcesses = new List<ProcessPanel>();

        runningProcesses.Add(this);

        StartCoroutine("StartProcess");
    }

    IEnumerator StartProcess()
    {
        while((Time.time - startTime) < processTime)
        {
            if (useDebug) Debug.Log("CALLED ---> ProcessPanel::StartProcess()");

            var deltaTime = Time.time - timeFromLastAddedBar;
            if (deltaTime > deltaBarTime)
            {
                timeFromLastAddedBar = Time.time;
                progressBarObj.AddBarToProceess(processID);
            }

            yield return new WaitForSeconds(.1f);
        }


        EndProcess();

        //yield return null;
    }

    private void EndProcess()
    {
        if (useDebug) Debug.Log("CALLED ---> ProcessPanel::EndProcess()");

        progressBarObj.RemoveProcess(processID);
        processCounter--;

        foreach (var process in runningProcesses)
            process.UpdateID(processID);

        runningProcesses.Remove(this);
        GameObject.Destroy(gameObject);
    }

    public void DisplayProcess()
    {
        if (useDebug) Debug.Log("CALLED ---> ProcessPanel::DisplayProcess()");

        progressBarObj.SwitchProcessDisplay(processID);
    }

    public void UpdateID(int finishedProcessID)
    {
        if (processID > finishedProcessID)
            processID--;
    }
}
