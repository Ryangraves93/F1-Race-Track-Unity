using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class LapController : MonoBehaviour
{
    PathFollower carToCheck;
    //public int id = 1;

    public bool carHasPitstopped = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     public void OnTriggerEnter(Collider other)
    {
       
        if(other.CompareTag("Car"))
        {
            carToCheck = other.GetComponent<PathFollower>();
           
            if (carHasPitstopped == true)
            {
                carToCheck.pitstopLap = true;
                Debug.Log("Path changed to pitstop");
                carHasPitstopped = false;
                
            }
            else if (carHasPitstopped == false && carToCheck.pitstopLap == false)
            {
                carToCheck.pitstopLap = false;        
            }   

        }
    }
}
