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
        Test();
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
       
        Crouching(target);
        
    }

    void Crouching(Transform target) { 
        transform.rotation = Quaternion.Lerp(transform.rotation, wheelRot, Time.deltaTime* smoothTime);
        running = false;
        anim.SetBool("isCrouching", true);
        Debug.Log("Crouch ran");
        pitStopController.pitCrewMove = false;
        anim.SetBool("isRunning", false);
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
        MoveToOrigin();
    }

    void Test()
    {
        if (test == true)
        {
            
            test = false;
            Debug.Log("Yeah");
        }
        
    }
    void MoveToOrigin()
    {
        anim.SetBool("isWalking", true);
        navMeshAgent.SetDestination(origin);
       
        if (navMeshAgent.remainingDistance < 0.1f)
        {
            anim.SetBool("idle", true);
        }
    }
}
