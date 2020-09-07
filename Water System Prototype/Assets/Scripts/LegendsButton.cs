using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendsButton : MonoBehaviour
{
    public List<GameObject> itemsList;
    public GameObject listPanel;
    public GameObject ItemPrefab;

    public float itemSizeFactor = 1.5f;
    public bool isDropdown = true;

    private bool isListVisible = false;

    void Start()
    {
        var itemHeight = listPanel.GetComponent<RectTransform>().anchorMax.y - listPanel.GetComponent<RectTransform>().anchorMin.y;

        if (isDropdown)
        {
            var anc = listPanel.GetComponent<RectTransform>().anchorMin;
            anc.y -= itemHeight * 5 / itemSizeFactor;
            listPanel.GetComponent<RectTransform>().anchorMin = anc;
        }
        else
        {
            var anc = listPanel.GetComponent<RectTransform>().anchorMax;
            anc.y += itemHeight * 5 / itemSizeFactor;
            listPanel.GetComponent<RectTransform>().anchorMax = anc;
        }
    }

    public void ChangeListVisibility()
    {
        isListVisible = !isListVisible;
        listPanel.SetActive(isListVisible);
    }
}
