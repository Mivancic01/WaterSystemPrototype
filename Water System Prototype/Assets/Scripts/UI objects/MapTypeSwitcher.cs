using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTypeSwitcher : MonoBehaviour
{
    public int mapTypeID = 1;
    public Sprite simpleMap_lv0, sateliteMap_lv0, vectorMap_lv0, simpleMap_lv1, sateliteMap_lv1, vectorMap_lv1, simpleMap_lv2, sateliteMap_lv2, vectorMap_lv2;

    public void SwitchMap(int type = 0)
    {
        var spriteComp = GetComponent<SpriteRenderer>();
        switch(mapTypeID)
        {
            case 0:
                SwitchLv0Map(type);
                break;
            case 1:
                SwitchLv1Map(type);
                break;
            case 2:
                SwitchLv2Map(type);
                break;
            default:
                Debug.LogError("SETTING AN INVALID MAP!");
                break;

        }
    }

    public void SwitchLv0Map(int type = 0)
    {
        var spriteComp = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case 0:
                spriteComp.sprite = simpleMap_lv0;
                break;
            case 1:
                spriteComp.sprite = sateliteMap_lv0;
                break;
            case 2:
                spriteComp.sprite = vectorMap_lv0;
                break;
            default:
                Debug.LogError("SETTING AN INVALID MAP!");
                break;

        }
    }

    public void SwitchLv1Map(int type = 0)
    {
        var spriteComp = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case 0:
                spriteComp.sprite = simpleMap_lv1;
                break;
            case 1:
                spriteComp.sprite = sateliteMap_lv1;
                break;
            case 2:
                spriteComp.sprite = vectorMap_lv1;
                break;
            default:
                Debug.LogError("SETTING AN INVALID MAP!");
                break;

        }
    }

    public void SwitchLv2Map(int type = 0)
    {
        var spriteComp = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case 0:
                spriteComp.sprite = simpleMap_lv2;
                break;
            case 1:
                spriteComp.sprite = sateliteMap_lv2;
                break;
            case 2:
                spriteComp.sprite = vectorMap_lv2;
                break;
            default:
                Debug.LogError("SETTING AN INVALID MAP!");
                break;

        }
    }
}
