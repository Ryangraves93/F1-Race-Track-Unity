using PathCreation.Examples;
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
    float smoothTime = 50f;
    NavMeshAgent navMeshAgent;
    Transform destination;
    public Quaternion facingCar = Quaternion.Euler(0, 90, 0);
    public GameObject pitStopControllerRef;
    pitStopController pitStopController;
    bool running = true;
    bool idle = true;
    private Animator anim;
    public Vector3 offSet;
    [HideInInspector]
    Vector3 origin;
    public PitstopMeshController pitStopMeshController;
    Transform m_target;
    public bool test = false;
    Quaternion wheelRot;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        pitStopMeshController = pitStopControllerRef.GetComponent<PitstopMeshController>();
        pitStopController = pitStopControllerRef.GetComponent<pitStopController>();

        origin = transform.position;
    }

     void Update()
    {
        //Debug.Log(pitStopController.pitCrewMove);
        if (idle == true)
        { 
            Idle();
        }
        if (pitStopController.pitCrewMove && running)
        {
            //idle = false;
            if (destination == null)
            {
                GetDestination(pitStopMeshController.wheelPositions);
            }
            else if (destination != null)
            {
                StartCoroutine(RunToTarget(destination));
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

    void Idle ()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("idle", true);
    }

     IEnumerator RunToTarget(Transform target)
    {
        Debug.Log(target.gameObject.name);
        anim.SetBool("idle", false);
        anim.SetBool("isRunning", true);
        yield return new WaitForSeconds(.2f);
        navMeshAgent.SetDestination(target.position + offSet);

            float timeMoving = 0f;
            while (navMeshAgent.remainingDistance > 0.2f)
            {
                timeMoving += Time.deltaTime;
                yield return null;
             }
        Vector3 Direction = target.position - transform.position;
        wheelRot = Quaternion.LookRotation(Direction);
       
        Crouching(target);
        
    }

    void Crouching(Transform target) { 

        //transform.rotation = Quaternion.Lerp(transform.rotation, wheelRot, Time.deltaTime* smoothTime);
        running = false;
        anim.SetBool("isRunning", false);
        anim.SetBool("isCrouching", true);       
        pitStopController.pitCrewMove = false;
       StartCoroutine(TireInteraction(target.gameObject,target));

    }

    IEnumerator TireInteraction(GameObject tire,Transform target)
    {
        yield return new WaitForSeconds(.2f);
        if (oldWheel || newWheel != null)
        {
            tire.SetActive(false);
            oldWheel.SetActive(true);
            yield return new WaitForSeconds(2f);
            newWheel.SetActive(false);
            tire.SetActive(true);
        }
        anim.SetBool("isCrouching", false);
        yield return new WaitForSeconds(3f);
        transform.LookAt(target);
        anim.SetBool("isWaving", true);
        yield return new WaitForSeconds(1.6f);
        anim.SetBool("isWaving", false);
        anim.SetBool("isWalking", true);
        yield return new WaitForSeconds(1f);
        MoveToOrigin();
    }

    void MoveToOrigin()
    {
        while (navMeshAgent.remainingDistance < 0.2f)
        {
            navMeshAgent.SetDestination(origin);
        }
            idle = true;
            anim.SetBool("idle", true);
            running = true;
            pitStopController.pitCrewMove = false;
            //pitStopController.cle
    }
}
