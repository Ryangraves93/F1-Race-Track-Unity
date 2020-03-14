using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WheelRotator : MonoBehaviour
{

    public Quaternion frontWheelRight, frontWheelLeft;
    public Transform rearWheelRight, rearWheelLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateWheels();
    }

    void rotateWheels()
    {
        frontWheelLeft.x += .5f;
    }
}
