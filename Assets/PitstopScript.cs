using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class PitstopScript : MonoBehaviour
{
    PathFollower carToCheck;
    public int id = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     public int OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car"))
        {
            carToCheck = other.GetComponent<PathFollower>();
            Debug.Log(id);
            if (carToCheck.id == this.id)
            {
                carToCheck.pitstopLap = true;
                Debug.Log("Path changed to pitstop");
                id++;
                return id;
            }
            else
            {
                carToCheck.pitstopLap = false;
                return id;
            }   

        }

        return id; 
    }
}
