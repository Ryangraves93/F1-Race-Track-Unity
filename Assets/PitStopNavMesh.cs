using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class PitStopNavMesh : MonoBehaviour
{
    public GameObject oldWheel, newWheel;
    public int id;
    float smoothTime = 1.0f;
    NavMeshAgent navMeshAgent;
    Transform destination;
    public Quaternion facingCar = Quaternion.Euler(0, 90, 0);
    public GameObject pitStopControllerRef;
    pitStopController pitStopController;
    bool running = true;
    private Animator anim;
    public Vector3 offSet;
    public PitstopMeshController pitStopMeshController;

    
    Quaternion wheelRot;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        pitStopMeshController = pitStopControllerRef.GetComponent<PitstopMeshController>();
        pitStopController = pitStopControllerRef.GetComponent<pitStopController>();
    }

     void Update()
    {
        
        if (pitStopController.pitCrewMove && running)
        {
            if (destination == null)
            {
                GetDestination(pitStopMeshController.wheelPositions);
            }
            if (destination != null)
            {
                StartCoroutine(MovementState(destination));
            }
        }
    
    }

    void GetDestination (Transform[] wheelPos)
    {
        for (int i = 0; i <= pitStopMeshController.wheelPositions.Length; i++)
        {
            if (id == i)
            {
                destination = pitStopMeshController.wheelPositions[i];
                break;
            }
        }
    }

     IEnumerator MovementState(Transform target)
    {
        
        anim.SetBool("isRunning", true);
        navMeshAgent.SetDestination(target.position + offSet);

            float timeMoving = 0f;
            while (navMeshAgent.remainingDistance > 0.2f)
            {
                timeMoving += Time.deltaTime;
                yield return null;
             }
        Vector3 Direction = target.position - transform.position;
        wheelRot = Quaternion.LookRotation(Direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, wheelRot, Time.deltaTime * smoothTime);
        Crouching(target);
        
    }

    void Crouching(Transform target)
    {
        running = false;
        anim.SetBool("isCrouching", true);
        Debug.Log("Crouch ran");
        pitStopController.pitCrewMove = false;
        anim.SetBool("isRunning", false);
        pitStopController.pitCrewMove = false;
       StartCoroutine(TireInteraction(target.gameObject));

    }

    IEnumerator TireInteraction(GameObject tire)
    {
        yield return new WaitForSeconds(.2f);
        tire.SetActive(false);
        oldWheel.SetActive(true);
        yield return new WaitForSeconds(2f);
        newWheel.SetActive(false);
        tire.SetActive(true);
    }    
}
