using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public bool dragMap { get; private set; }
    public bool dragComponents { get; private set; }
    public bool createPath { get; private set; }
    public bool canDragMap { get; private set; }
    public static GameStateManager Instance { get; private set; }

    public bool useDebug = false;

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

        dragMap = false;
        dragComponents = true;
        createPath = false;
        canDragMap = true;
    }

    public void SetDragMapState()
    {
        if (dragMap == true)
        {
            if (useDebug) Debug.LogWarning("SetDragMapState Inactive!" +
                   "\n" + "At time " + Time.time);
            SetInactiveState();
            SetDragComponentsState();
            return;
        }

        if (!createPath)
        {
            if (useDebug) Debug.LogWarning("SetDragMapState Active!" + 
                "\n" + "At time " + Time.time);
            canDragMap = true;
            dragMap = true;
            dragComponents = false;
            createPath = false;
        }
    }

    public void SetDragComponentsState()
    {
        if (!dragMap && !createPath)
        {
            if (useDebug) Debug.LogWarning("SetDragComponentsState Active!" +
                "\n" + "At time " + Time.time); ;
            dragMap = false;
            dragComponents = true;
            createPath = false;
        }
    }

    public void SetPathCreationState()
    {
        if (useDebug) Debug.LogWarning("SetPathCreationState Active!" +
                "\n" + "At time " + Time.time);
        dragMap = false;
        dragComponents = false;
        createPath = true;
    }

    public void SetInactiveState()
    {
        if (useDebug) Debug.LogWarning("SetInactiveState!" +
                "\n" + "At time " + Time.time);
        dragMap = false;
        dragComponents = false;
        createPath = false;
    }

    public void SetCanDragMap(bool value)
    {
        if (useDebug) Debug.LogWarning("SetCanDragMap with value = " + value +
                "\n" + "At time " + Time.time);
        canDragMap = value;
    }
}
