using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{

    public player player;
    public GameObject ShopPanel;
    public GameObject SHOP_ITEM;
    private Text points;
    public float duration = 1.0f;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        
        yield return new WaitForSeconds(duration);
        ShopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        points = ShopPanel.GetComponentInChildren<Text>();
        points.text = "Current points: "+player.points.ToString();
    }

    public void AddHealth()
    {
        if(player.points > 0)
        {
            player.maxHealth += 20;
            player.health +=20;
            player.points -=1;
        }
    }
    public void AddSpeed()
    {
        if(player.points > 0)
        {
            player.speed += 1;
            player.points -=1;
        }
        
    }
    

}
