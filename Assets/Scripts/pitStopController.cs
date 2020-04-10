using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class pitStopController : MonoBehaviour
{
    PitstopMeshController pitStopMeshController;
    public bool pitCrewMove = false;
    PathFollower m_carToCheck; //Empty script variable that gets assigned when a car collides through the collider

    public GameObject lapControllerRef;//Reference to the lap controller object which is filled in the editor
    LapController lapController;//The script variable which allows the pitcontroller to communicate with lapcontroller

    private void Start()
    {
        pitStopMeshController = GetComponent<PitstopMeshController>();
        lapController = lapControllerRef.GetComponent<LapController>(); //Assign the lapcontroller
    }
    private void OnTriggerEnter(Collider other) //Trigger to have the car stop for a pitstop
    {
        if (other.CompareTag("Car"))//Checks if the collision is a car
        {
             
            m_carToCheck = other.GetComponent<PathFollower>();//Assigns the carToCheck variable to PathFollower script of the collided car
            m_carToCheck.m_carChecked = false;

            if (m_carToCheck.pitstopLap == true) //If the car is on a pitstoplap, will trigger a function to slow down the car
            {
                pitStopMeshController.GetWheelPositions(other.gameObject);
                pitCrewMove = true;
                m_carToCheck.m_slowingDown = true;
                lapController.carHasPitstopped = true;
            }
        }
    }
}
