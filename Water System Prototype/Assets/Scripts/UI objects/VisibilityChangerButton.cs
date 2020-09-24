using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityChangerButton : MonoBehaviour
{
    public bool isActive = false;
    public GameObject obj;
    
    void Start()
    {
        obj.SetActive(isActive);
    }

    public void ChangeVisibility()
    {
        isActive = !isActive;
        obj.SetActive(isActive);
    }
}
