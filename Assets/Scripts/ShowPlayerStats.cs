using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerStats : MonoBehaviour
{
    public player player;
    public GameObject StatsPanel;

    private Text Strength;
    private Text Health;
    private Text Speed;
    private Text Range;
    private bool activePanel;
    // Start is called before the first frame update
    void Start()
    {
        Strength = StatsPanel.GetComponentInChildren<Text>();
        Health = StatsPanel.transform.GetChild(1).GetComponent<Text>();
        Speed = StatsPanel.transform.GetChild(2).GetComponent<Text>();
        Range = StatsPanel.transform.GetChild(3).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(!activePanel)
            {
            openPanel();
            }
            else
            {   
            closePanel();
            }
        }
        
        // StatsPanel.SetActive(!StatsPanel.activeSelf);
        updatePanel();
            
            
        
    }

    private void updatePanel()
    {
        Strength.text = "Strength: " + player.strength.ToString();
        Health.text = "Health: " + player.health.ToString();   
        Speed.text = "Speed: " + player.speed.ToString();
        Range.text = "Reach: " + player.range.ToString();

    }

    private void closePanel()
    {
        activePanel = false;
        StatsPanel.SetActive(false);
    }
    private void openPanel()
    {
        activePanel = true;
        StatsPanel.SetActive(true);
        updatePanel();
    }
}
