using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private bool isLeftMouseUp = true, isRightMouseUp = true;
    private bool isLeftMouseClicked = false, isRightMouseClicked = false;

    private List<GameObject> observers;

    public static MouseManager Instance { get; private set; }

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

        observers = new List<GameObject>();
    }

    void Update()
    {
        isLeftMouseClicked = false;
        isRightMouseClicked = false;

        if (Input.GetKeyDown(0) && isLeftMouseUp)
        {
            isLeftMouseClicked = true;
            isLeftMouseUp = false;
        }

        if(!Input.GetKeyUp(0) && !isLeftMouseUp)
        {
            isLeftMouseUp = true;
        }

        if (Input.GetKeyDown(0) && isRightMouseUp)
        {
            isRightMouseClicked = true;
            isRightMouseUp = false;
        }

        if (!Input.GetKeyUp(0) && !isRightMouseUp)
        {
            isRightMouseUp = true;
        }
    }

    void StartMouseDownEvent(bool isLeftButton)
    {

    }

    void StartMouseUpEvent(bool isLeftButton)
    {

    }

    public void AddObserver(GameObject obj)
    {
        if (!observers.Contains(obj))
            observers.Add(obj);
    }

    public void RemoveObserver(GameObject obj)
    {
        observers.Remove(obj);
    }
}
