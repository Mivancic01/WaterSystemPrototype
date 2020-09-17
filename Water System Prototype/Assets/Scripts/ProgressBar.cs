using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Sprite activeImage, inactiveImage;
    private List<int> activeBarCounter;
    private List<Image> childrenBars;
    private int activeProcess;

    public bool useDebug = false;

    void Start()
    {
        activeBarCounter = new List<int>();
        childrenBars = gameObject.GetComponentsInChildren<Image>().ToList();

        activeProcess = -1;

        for (int i = 0; i < childrenBars.Count; i++)
            childrenBars[i].sprite = inactiveImage;

        gameObject.GetComponent<Image>().sprite = activeImage;
    }

    public void AddProcess()
    {
        if (useDebug) Debug.Log("CALLED ---> ProgressBar::AddProcess()");

        activeProcess = activeBarCounter.Count;
        activeBarCounter.Add(0);
    }

    public void AddBarToProceess(int processID)
    {
        if (useDebug) Debug.Log("CALLED ---> ProgressBar::AddBarToProceess() with processID = " + processID);

        if (activeProcess < 0)
            Debug.LogError("THERE IS NO PROCESS ACTIVE!");

        activeBarCounter[processID]++;

        if (activeProcess == processID)
            SwitchProcessDisplay(activeProcess);
    }

    public void SwitchProcessDisplay(int processID)
    {
        if (useDebug) Debug.Log("CALLED ---> ProgressBar::SwitchProcessDisplay() with processID = " + processID);

        activeProcess = processID;

        for (int i = 0; i < childrenBars.Count; i++)
            if(i <= activeBarCounter[processID])
                childrenBars[i].sprite = activeImage;
            else
                childrenBars[i].sprite = inactiveImage;

        gameObject.GetComponent<Image>().sprite = activeImage;
    }

    public void RemoveProcess(int processID)
    {
        if (useDebug) Debug.Log("CALLED ---> ProgressBar::RemoveProcess() with processID = " + processID);

        activeBarCounter.RemoveAt(processID);

        if (activeProcess == processID)
        {
            if(activeBarCounter.Count == 0)
                for (int i = 0; i < childrenBars.Count; i++)
                    childrenBars[i].sprite = inactiveImage;
            else
                SwitchProcessDisplay(0);
        }
    }
}
