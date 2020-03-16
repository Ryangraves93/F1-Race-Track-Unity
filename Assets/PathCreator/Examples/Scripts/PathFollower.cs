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

        private bool readyToGo = false;
        private bool pitStopping = false;

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
            Debug.Log(pitStopping);
            if (readyToGo == true)
            {
                if (pathCreator != null)
                {
                    if (pitStopping == false)
                    {
                        LoopTrack();
                    }

                    else if (pitStopping == true)
                    {
                        stopping();
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
            speed = 60f;
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }

        void stopping()
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pitStop.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pitStop.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            speed -= .8f;
        }

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

        public IEnumerator carReady()
        {
            yield return new WaitForSeconds(timeToStart);
            readyToGo = true;
        }
    }
}