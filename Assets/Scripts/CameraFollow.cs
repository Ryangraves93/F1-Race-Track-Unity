using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform[] carTarget;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    int i = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (i == (carTarget.Length + 1))
            {
                i = 0;
            }
            else
            {
                i++;
            }
            
        }
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = carTarget[i].position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = carTarget[i].position + offset;

        transform.LookAt(carTarget[i]);
    }
}
