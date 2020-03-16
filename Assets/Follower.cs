using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public PathCreator pitStop;
    public float speed = 5;
    float distanceTraveled;

    bool isStopping = false;

    private void Update()
    {
       if (isStopping == false)
          {
                LoopRacetrack();
          }

       if (isStopping == true)
        {
            PitStop();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pit Stop"))
        {
            isStopping = true;
        }
    }

    private void LoopRacetrack()
    {
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
    }

    private void PitStop()
    {
        distanceTraveled += speed * Time.deltaTime;
        transform.position = pitStop.path.GetPointAtDistance(distanceTraveled);
    }
}
