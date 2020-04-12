using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using System;

public class PitstopMeshController : MonoBehaviour
{
    PathFollower m_pathfollower;

    Transform frontLeftWheel, frontRightWheel;
    Transform rearLeftWheel, rearRightWheel;
    Transform carRear;

    public Transform[] wheelPositions;
    public bool test = false;

    private void Awake()
    {
        wheelPositions = new Transform[5];
    }
     void Update()
    {

        if (test == true)
        {
            Debug.Log("Cleared Variables");
            ClearVariables();
        }
        if (m_pathfollower != null)
        {
            if (m_pathfollower.staionary)
            {
                AssignWheels();
                Debug.Log(m_pathfollower);
                if (m_pathfollower.staionary == false)
                {
                    ClearVariables();
                }
            }
        }
    }
    public void GetWheelPositions(GameObject car)
    {
        if (m_pathfollower != null)
        {
            m_pathfollower = null;
        }
        m_pathfollower = car.GetComponent<PathFollower>();
    }

    void AssignWheels ()
    {
        frontLeftWheel = m_pathfollower.transform.GetChild(9);
        frontRightWheel = m_pathfollower.transform.GetChild(10);
        rearLeftWheel = m_pathfollower.transform.GetChild(11);
        rearRightWheel = m_pathfollower.transform.GetChild(12);
        carRear = m_pathfollower.transform.GetChild(4);

        wheelPositions[0] = frontLeftWheel;
        wheelPositions[1] = frontRightWheel;
        wheelPositions[2] = rearLeftWheel;
        wheelPositions[3] = rearRightWheel;
        wheelPositions[4] = carRear;

    }

    void ClearVariables()
    {
        m_pathfollower = null;
        wheelPositions = new Transform[5];
        test = false;
    }
}
