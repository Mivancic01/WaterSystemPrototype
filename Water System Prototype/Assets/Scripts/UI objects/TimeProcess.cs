using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeProcess : MonoBehaviour
{
    private ProgressMenuManager progressManager;
    float processTime, startTime, deltaBarTime, timeFromLastAddedBar;
    int processID;
    static int processCounter = 0;

    public bool useDebug = false;

    public void Initialize()
    {
        if (useDebug) Debug.Log("CALLED ---> ProcessPanel::Initialize()");

        processTime = Random.Range(5, 25);
        startTime = Time.time;

        timeFromLastAddedBar = 0;
        deltaBarTime = processTime / 20.0f;
        processID = processCounter++;

        progressManager = ProgressMenuManager.Instance;
        progressManager.AddProcess(processID);

        StartCoroutine("StartProcess");
    }

    IEnumerator StartProcess()
    {
        while ((Time.time - startTime) < processTime)
        {
            if (useDebug) Debug.Log("CALLED ---> ProcessPanel::StartProcess()");

            var deltaTime = Time.time - timeFromLastAddedBar;
            if (deltaTime > deltaBarTime)
            {
                timeFromLastAddedBar = Time.time;
                progressManager.AddBarToProceess(processID);
            }

            yield return new WaitForSeconds(.1f);
        }


        EndProcess();

        //yield return null;
    }

    private void EndProcess()
    {
        if (useDebug) Debug.Log("CALLED ---> ProcessPanel::EndProcess()");

        progressManager.RemoveProcess(processID);

        GameObject.Destroy(gameObject);
    }

    public void DisplayProcess()
    {
        if (useDebug) Debug.Log("CALLED ---> ProcessPanel::DisplayProcess()");

        progressManager.SwitchProcessDisplay(processID);
    }
}
