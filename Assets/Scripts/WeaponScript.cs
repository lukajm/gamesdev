using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private bool swing = false;
    int degree = 0;
    private float wepX = 0.3f;
    private float wepY = -0.2f;   

    Vector3 pos;
    public GameObject player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //spacebar enabled rendering of weapon and sets it to active
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (swing)
        {
            degree -= 7;
            if(degree < -65)
            {
                degree = 0;
                swing = false;
                GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }

            transform.eulerAngles = Vector3.forward * degree;
        }
    }

    void Attack()
    {

        if (player.GetComponent<player>().lookLeft)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            wepX = -0.3f;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            wepX = 0.3f;
            wepY = -0.2f; // set the wepY value as well to avoid any potential issues
        }


        pos = player.transform.position;
        pos.x += wepX;
        pos.y += wepY;
        transform.position = pos;

        swing = true;

    }
}
