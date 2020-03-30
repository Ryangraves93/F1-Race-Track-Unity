using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class pitStopController : MonoBehaviour
{

    PathFollower m_carToCheck;

    public GameObject lapControllerRef;
    LapController lapController;

    private void Start()
    {
        lapController = lapControllerRef.GetComponent<LapController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            m_carToCheck = other.GetComponent<PathFollower>();

            if (m_carToCheck.pitstopLap == true)
            {
                m_carToCheck.m_slowingDown = true;
                lapController.carHasPitstopped = true;
            }
        }
    }
}
