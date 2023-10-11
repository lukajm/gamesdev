using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject exit;
    
    private void OnDestroy()
    {
        exit.SetActive(true);
    }
    
}
