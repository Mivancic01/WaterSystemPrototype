using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineScrollbar : MonoBehaviour
{
    public void ChangeTimeline()
    {
        MainSimulationManager.ModelsManager.SwitchModels(GetModelIndexFromScrollbar());
    }
    private int GetModelIndexFromScrollbar()
    {
        float value = GetComponent<Scrollbar>().value;

        if (value < 0.25f)
            return 0;
        else if (value < 0.5f)
            return 1;
        else if (value < 0.75f)
            return 2;
        else if (value <= 1.0f)
            return 3;

        Debug.LogError("INVALID SCROLLBAR VALUE!");
        return 3;
    }
}
