using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using PathCreation.Examples;

public class CameraFollow : MonoBehaviour
{
    
    public Transform[] carTarget;
    int i = 0;
    PathFollower target;

    public GameObject clearShotRef;
    CinemachineClearShot m_clearShot;

    CinemachineVirtualCamera followCamera;

    private void Start()
    {
        followCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        
        target = carTarget[0].GetComponent<PathFollower>();
        
        m_clearShot = clearShotRef.GetComponent<CinemachineClearShot>();
    }
    
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            target.isTarget = false;

            if (i >= carTarget.Length)
            {
                i = 0;
                
                target = carTarget[i].gameObject.GetComponent<PathFollower>();
                
                target.isTarget = true;
                
                m_clearShot.LookAt = carTarget[i].transform;
                followCamera.LookAt = carTarget[i].transform;
                followCamera.Follow = carTarget[i].transform;
            }
            else
            {
                i++;
                
                target = carTarget[i].gameObject.GetComponent<PathFollower>();
                
                target.isTarget = true;
                
                m_clearShot.LookAt = carTarget[i].transform;
                followCamera.LookAt = carTarget[i].transform;
                followCamera.Follow = carTarget[i].transform;
            }
        }
    }
}
