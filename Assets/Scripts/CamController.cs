using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{

    private CinemachineVirtualCamera virtualCam;
    private CinemachineBrain cinemachineBrain;
    // Start is called before the first frame update
    void Start()
    {
        virtualCam = GameObject.Find("Follow Player Camera").GetComponent<CinemachineVirtualCamera>();
        cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
