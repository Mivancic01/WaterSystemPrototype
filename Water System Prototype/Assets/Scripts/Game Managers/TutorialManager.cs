using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private int eventID = 0;

    //Instructions to add a junction, then a reservoir, then a pipe connecting them
    public GameObject clickJunctionInstruction, placeJunctionInstruction, clickReservoirInstruction, placeReservoirInstruction, clickPipeInstruction, placePipeInstruction;

    //Buttons and sidebar instructions
    public GameObject clickGraphInstruction, clickclickProgressInstruction, clickStatsInstruction, clickLegendsInstruction, moveSideBarInstruction;
    
    //Map interaction instructions
    public GameObject clickMapTypeInstruction, selectMapInstruction, zoomInInstruction, zoomOutInstruction;

    //Progress panel instructions
    public GameObject addSimulationInstruction, showProgressInstruction;


    public static TutorialManager Instance { get; private set; }

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
        SetEvent();
    }

    public bool CheckEvent(int localEventID)
    {
        if (localEventID == -1)
            Debug.LogError("ERROR! Set EventID == -1 \n" + Time.time);

        if (localEventID == eventID)
        {
            StartEvent();
            return true;
        }

        return false;
    }

    public void CheckEventExecution(int localEventID)
    {
        if (localEventID == -1)
            Debug.LogError("ERROR! Set EventID == -1 \n" + Time.time);

        if (localEventID == eventID)
            StartEvent();
    }

    private void SetEvent()
    {
        Reset();

        switch (eventID)
        {
            case 0:
                clickJunctionInstruction.SetActive(true);
                break;
            case 1:
                placeJunctionInstruction.SetActive(true);
                break;
            case 2:
                clickReservoirInstruction.SetActive(true);
                break;
            case 3:
                placeReservoirInstruction.SetActive(true);
                break;
            case 4:
                clickPipeInstruction.SetActive(true);
                break;
            case 5:
                placePipeInstruction.SetActive(true);
                break;
            case 6:
                clickGraphInstruction.SetActive(true);
                break;
            case 7:
                clickclickProgressInstruction.SetActive(true);
                break;
            case 8:
                clickStatsInstruction.SetActive(true);
                break;
            case 9:
                clickLegendsInstruction.SetActive(true);
                break;
            case 10:
                moveSideBarInstruction.SetActive(true);
                break;
            case 11:
                clickMapTypeInstruction.SetActive(true);
                break;
            case 12:
                selectMapInstruction.SetActive(true);
                break;
            case 13:
                zoomInInstruction.SetActive(true);
                break;
            case 14:
                zoomOutInstruction.SetActive(true);
                break;
            case 15:
                addSimulationInstruction.SetActive(true);
                break;
            case 16:
                showProgressInstruction.SetActive(true);
                break;
        }
    }

    private void StartEvent()
    {
        eventID++;
        SetEvent();
    }

    private void Reset()
    {
        clickJunctionInstruction.SetActive(false);
        placeJunctionInstruction.SetActive(false);
        clickReservoirInstruction.SetActive(false);
        placeReservoirInstruction.SetActive(false);
        clickPipeInstruction.SetActive(false);
        placePipeInstruction.SetActive(false);

        clickGraphInstruction.SetActive(false);
        clickclickProgressInstruction.SetActive(false);
        clickStatsInstruction.SetActive(false);
        clickLegendsInstruction.SetActive(false);
        moveSideBarInstruction.SetActive(false);

        clickMapTypeInstruction.SetActive(false);
        selectMapInstruction.SetActive(false);
        zoomInInstruction.SetActive(false);
        zoomOutInstruction.SetActive(false);

        addSimulationInstruction.SetActive(false);
        showProgressInstruction.SetActive(false);
    }
}
