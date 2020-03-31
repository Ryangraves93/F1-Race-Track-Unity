using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class LapController : MonoBehaviour
{
    PathFollower carToCheck;
    
    public int lapId = 4;
    public bool carHasPitstopped = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(id);
    }


     public void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Car"))
        {
            carToCheck = other.GetComponent<PathFollower>();
           
            if (carHasPitstopped == true && carToCheck.m_carChecked == false)
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
            carToCheck = null;
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
