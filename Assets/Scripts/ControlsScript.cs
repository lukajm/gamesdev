using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScript : MonoBehaviour
{
    public GameObject ControlPanel;
    public GameObject PlayButton;
    public GameObject ControlButton;
    public GameObject TitleText;
    
    public void ShowPanel()
    {
        ControlPanel.SetActive(true);
        PlayButton.SetActive(false);
        ControlButton.SetActive(false);
        TitleText.SetActive(false);

    }
    public void HidePanel()
    {
        ControlPanel.SetActive(false);
        PlayButton.SetActive(true);
        ControlButton.SetActive(true);
        TitleText.SetActive(true);
    }
}
