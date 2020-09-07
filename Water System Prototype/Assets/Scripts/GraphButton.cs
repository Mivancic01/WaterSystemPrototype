using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphButton : MonoBehaviour
{
    private bool openMainPanel = true;
    private bool openGraphPanel = false;
    public bool OpenMainPanel
    {
        get { return openMainPanel; }
        set { 
            openMainPanel = value;
            openGraphPanel = !value;
        }
    }
    public bool OpenGraphPanel
    {
        get { return openGraphPanel; }
        set
        {
            openGraphPanel = value;
            openMainPanel = !value;
        }
    }

    public GameObject mainPanel, graphPanel;

    public void SwitchGraphMainPanels()
    {
        Debug.Log("SWITCHING PANELS!");

        OpenMainPanel = !OpenMainPanel;

        mainPanel.SetActive(openMainPanel);
        graphPanel.SetActive(OpenGraphPanel);
    }
}
