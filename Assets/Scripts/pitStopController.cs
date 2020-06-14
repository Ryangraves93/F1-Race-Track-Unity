using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class pitStopController : MonoBehaviour
{
    public bool pitCrewMove = false;
    PathFollower m_carToCheck; //Empty script variable that gets assigned when a car collides through the collider

    public GameObject lapControllerRef;//Reference to the lap controller object which is filled in the editor
    LapController lapController;//The script variable which allows the pitcontroller to communicate with lapcontroller


    Transform frontLeftWheel, frontRightWheel;
    Transform rearLeftWheel, rearRightWheel;
    Transform carRear;


    public Transform[] wheelPositions;
    private void Start()
    {
        wheelPositions = new Transform[5];
        lapController = lapControllerRef.GetComponent<LapController>(); //Assign the lapcontroller
    }
    
     void OnTriggerEnter(Collider other) //Trigger to have the car stop for a pitstop
    {
        if (other.CompareTag("Car"))//Checks if the collision is a car
        {
            other.gameObject.GetComponent<PathFollower>().m_carChecked = false;

            if (other.gameObject.GetComponent<PathFollower>().pitstopLap == true) //If the car is on a pitstoplap, will trigger a function to slow down the car
            {
                m_carToCheck = other.GetComponent<PathFollower>();//Assigns the carToCheck variable to PathFollower script of the collided car
                
                m_carToCheck.m_slowingDown = true;
                
                lapController.carHasPitstopped = true;
            }
        }
    }

     void Update()
    {
        if (m_carToCheck != null)
        {
            if (m_carToCheck.staionary)
            {
                AssignWheels();
                pitCrewMove = true;
            }
        }
    }

    void AssignWheels()
    {
        frontLeftWheel = m_carToCheck.transform.GetChild(9);
        frontRightWheel = m_carToCheck.transform.GetChild(10);
        rearLeftWheel = m_carToCheck.transform.GetChild(11);
        rearRightWheel = m_carToCheck.transform.GetChild(12);
        carRear = m_carToCheck.transform.GetChild(4);

        wheelPositions[0] = frontLeftWheel;
        wheelPositions[1] = frontRightWheel;
        wheelPositions[2] = rearLeftWheel;
        wheelPositions[3] = rearRightWheel;
        wheelPositions[4] = carRear;

    }
}
