using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMenuManager : MonoBehaviour
{
    public Sprite activeImage, inactiveImage;
    private Dictionary<int, int> processBarCounter;
    private List<Image> childrenBars;
    private int activeProcess;

    public bool useDebug = false; 
    public static ProgressMenuManager Instance { get; private set; }

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

    void Start()
    {
        processBarCounter = new Dictionary<int, int>();
        childrenBars = gameObject.GetComponentsInChildren<Image>().ToList();

        activeProcess = -1;

        for (int i = 0; i < childrenBars.Count; i++)
            childrenBars[i].sprite = inactiveImage;

        gameObject.GetComponent<Image>().sprite = activeImage;
    }

    public void AddProcess(int processID)
    {
        if (useDebug) Debug.Log("CALLED ---> ProgressBar::AddProcess()");

        processBarCounter.Add(processID, 0);
        activeProcess = processID;
    }

    public void AddBarToProceess(int processID)
    {
        if (useDebug) Debug.Log("CALLED ---> ProgressBar::AddBarToProceess() with processID = " + processID);

        if (activeProcess < 0)
            Debug.LogError("THERE IS NO PROCESS ACTIVE!");

        processBarCounter[processID]++;

        if (activeProcess == processID)
            SwitchProcessDisplay(activeProcess);
    }

    public void SwitchProcessDisplay(int processID)
    {
        if (useDebug) Debug.Log("CALLED ---> ProgressBar::SwitchProcessDisplay() with processID = " + processID);

        activeProcess = processID;

        for (int i = 0; i < childrenBars.Count; i++)
            if (i <= processBarCounter[processID])
                childrenBars[i].sprite = activeImage;
            else
                childrenBars[i].sprite = inactiveImage;

        gameObject.GetComponent<Image>().sprite = activeImage;
    }

    public void RemoveProcess(int processID)
    {
        if (useDebug) Debug.Log("CALLED ---> ProgressBar::RemoveProcess() with processID = " + processID);

        processBarCounter.Remove(processID);

        if (processBarCounter.Count == 0) 
            for (int i = 0; i < childrenBars.Count; i++)
                childrenBars[i].sprite = inactiveImage;

        else if (activeProcess == processID)
            SwitchProcessDisplay(processBarCounter.First().Key);
    }
}
