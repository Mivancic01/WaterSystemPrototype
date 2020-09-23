using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesWindow : MonoBehaviour
{
    public InputField field1, field2, field3, field4;
    private float value1 = 0.0f, value2 = 0.0f, value3 = 0.0f, value4 = 0.0f;
    public int elementID;

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();
    }

    public void DeleteElement()
    {
        GameStateManager.Instance.SetInactiveState();
        GameStateManager.Instance.SetDragComponentsState();
        ComponentsManager.Instance.DeleteElement(elementID);
    }

    public void UpdateInputField(int inputID, float value)
    {
        if(inputID == 0 && field1 != null)
        {
            value1 = value;
            field1.SetTextWithoutNotify(value.ToString());
        }

        else if (inputID == 1 && field2 != null)
        {
            value2 = value;
            field2.SetTextWithoutNotify(value.ToString());
        }

        else if (inputID == 2 && field3 != null)
        {
            value3 = value;
            field3.SetTextWithoutNotify(value.ToString());
        }

        else if (inputID == 3 && field4 != null)
        {
            value4 = value;
            field4.SetTextWithoutNotify(value.ToString());
        }
    }

    public void ChangeValue()
    {
        List<float> values = new List<float>();

        if (field1 != null)
            values.Add(value1);

        else if (field2 != null)
            values.Add(value2);

        else if (field3 != null)
            values.Add(value3);

        else if (field4 != null)
            values.Add(value4);
    }
}
