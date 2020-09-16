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
        activeProcess = activeBarCounter.Count;
        activeBarCounter.Add(activeProcess);
    }

    public void AddBarToProceess(int processIndex)
    {
        if (activeProcess < 0)
            Debug.LogError("THERE IS NO PROCESS ACTIVE!");

        activeBarCounter[processIndex]++;

        if (activeProcess == processIndex)
            SwitchProcessDisplay(activeProcess);
    }

    public void SwitchProcessDisplay(int processIndex)
    {
        activeProcess = processIndex;

        for (int i = 0; i < childrenBars.Count; i++)
            if(i <= activeBarCounter[processIndex])
                childrenBars[i].sprite = activeImage;
            else
                childrenBars[i].sprite = inactiveImage;

        gameObject.GetComponent<Image>().sprite = activeImage;
    }


}
