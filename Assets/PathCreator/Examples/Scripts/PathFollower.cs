using UnityEngine;
using System.Collections;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public PathCreator pitStop;

        public EndOfPathInstruction endOfPathInstruction;
        public float speed;
        float distanceTravelled;
        public float timeToStart;

        public int id;

        public bool pitstopLap = false;
        private bool readyToGo = false;
  
        /*  private bool pitStopping = false;
        private bool stationary = false;
*/
        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }

            StartCoroutine(carReady());

        }

        void Update()
        {
           
            if (readyToGo == true)
            {
                if (pathCreator != null)
                {
                    if (pitstopLap == false)
                    {
                        LoopTrack();
                    }
                    else if (pitstopLap == true)
                    {
                        pitstop();
                    }
                
                }
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        void LoopTrack()
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }

        void pitstop()
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pitStop.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pitStop.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

        }
/*
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pit Stop Start"))
            {
                pitStopping = true;
            }
            else if (other.CompareTag("Pit Stop Finish"))
            {
                pitStopping = false;
            }
        }

        public IEnumerator carStationary()
        {
            stationary = true;
            Debug.Log("Stationary");
            yield return new WaitForSeconds(3f);
            stationary = false;
            speed = 60f;
            //stopping();
        }*/
        public IEnumerator carReady()
        {
            yield return new WaitForSeconds(timeToStart);
            readyToGo = true;
        }
    }
}