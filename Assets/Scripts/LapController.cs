using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class LapController : MonoBehaviour
{
    PathFollower carToCheck;//Empty variable that will be used to get information about the car colliding with triggers

    public int lapId = 1; // Variable to determine which car will go on a pitstop lap
    public bool carHasPitstopped = true; // Ensures that two cars do not pit stop at the same time
 



     public void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Car")) //Checks if the collision is tagged with car
        {
            carToCheck = other.GetComponent<PathFollower>();//Assigns empty variable to the car which it collided 
           
            if (carHasPitstopped == true && carToCheck.m_carChecked == false)//Checks 
            {
                Debug.Log(carToCheck.id + "car id before function");
                CheckId(carToCheck.id);
                Debug.Log(carToCheck.id + "car id after function");  
            }
            else 
            {
                carToCheck.pitstopLap = false;        
            }
            carToCheck.m_carChecked = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            carToCheck = null; //Emptys car variable so it can be reassigned and reused with another car
        }
    }

    int CheckId(int id)
    {
        Debug.Log(id + "When passed into checkid");
        Debug.Log(lapId + " id to be check againist");
        if (id == lapId)
        {
            carToCheck.pitstopLap = true;
            Debug.Log("Path changed to pitstop");
            lapId++;
            carHasPitstopped = false;
            Debug.Log(lapId + " After being incremented ");
        }
        Debug.Log(lapId + " Before being returned ");
        return lapId;

    }
}
