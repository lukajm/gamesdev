using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class RestartButton : MonoBehaviour
{
    RoomSpawner roomSpawner;
    public player player;
    
    

   void Start()
    {

    }
    void Update()
    {

    }
   public void gameReset()
    {
        SceneManager.LoadScene("SampleScene");
        
        GameObject.FindObjectOfType<RoomSpawner>().Spawn();
        Cinemachine.CinemachineVirtualCamera cam = GameObject.Find("Follow Player Camera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        player.strength = 0;
        player.range = 0;
        player.health = player.maxHealth;
        player.speed = 4;





        print("Button works");
    }
}
