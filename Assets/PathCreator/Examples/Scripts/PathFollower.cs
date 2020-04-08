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
        public bool isTarget = false;
        public bool staionary = false;
        public bool m_carChecked = false;

        public bool pitstopLap = false;
        [HideInInspector]
        public bool readyToGo = false;
        [HideInInspector]
        public bool m_slowingDown = false;

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
        void OnPathChanged()
        {
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

            if (m_slowingDown == true)
            {
                speed -= .85f;
                if (speed <= 0)
                {
                    speed = 0;
                    m_slowingDown = false;
                    StartCoroutine(carStationary());
                }
            }
        }

        public IEnumerator carStationary()
        {
            staionary = true;
            yield return new WaitForSeconds(3f);
            speed = 60f;
            staionary = false;
        }
        public IEnumerator carReady()
        {
 
            yield return new WaitForSeconds(timeToStart);
            readyToGo = true;
        }
    }
}