using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WheelRotator : MonoBehaviour
{

    public Transform frontWheelRight, frontWheelLeft;
    public Transform rearWheelRight, rearWheelLeft;

    public float wheelSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frontWheelLeft.Rotate(wheelSpeed,0,0);
        frontWheelRight.Rotate(wheelSpeed, 0, 0);
        rearWheelRight.Rotate(wheelSpeed, 0, 0);
        rearWheelLeft.Rotate(wheelSpeed, 0, 0);
    }

}
