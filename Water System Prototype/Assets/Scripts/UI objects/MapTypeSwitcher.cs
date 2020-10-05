using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTypeSwitcher : MonoBehaviour
{
    public Sprite simpleMap, sateliteMap, topographicMap;

    public void SwitchMap(int type = 0)
    {
        var spriteComp = GetComponent<SpriteRenderer>();
        switch(type)
        {
            case 0:
                spriteComp.sprite = simpleMap;
                break;
            case 1:
                spriteComp.sprite = sateliteMap;
                break;
            case 2:
                spriteComp.sprite = topographicMap;
                break;
            default:
                Debug.LogError("SETTING AN INVALID MAP!");
                break;

        }
    }
}
