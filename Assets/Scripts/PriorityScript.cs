using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using PathCreation.Examples;

public class PriorityScript : MonoBehaviour
{

    public GameObject camRef;
    
    CinemachineVirtualCamera cam;
    public CinemachineVirtualCamera followCam;
    
    PathFollower target;
    
    void Start()
    {
        cam = camRef.GetComponent<CinemachineVirtualCamera>();
    }

   
     void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car") && other.GetComponent<PathFollower>().isTarget == true)
        {
            followCam.gameObject.SetActive(false);
            
            cam.gameObject.SetActive(true);
        }
    }
    
     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            followCam.gameObject.SetActive(true);
            
            cam.gameObject.SetActive(false);
        }
    }
}
