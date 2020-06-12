using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class PitStopNavMesh : MonoBehaviour
{
    pitStopController pitStopController;
    public GameObject pitStopControllerRef;
    NavMeshAgent navMeshAgent;
    public GameObject oldWheel, newWheel;
    public int id;
    float smoothTime = 50f;
   
    Transform destination;
    public Quaternion facingCar = Quaternion.Euler(0, 90, 0);
  
    bool running = true;
    bool idle = true;
    
    private Animator anim;
    public Vector3 offSet;
    [HideInInspector]
    Vector3 origin;
    Transform m_target;

    Quaternion wheelRot;
    //Initialize references and animator components
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        anim = GetComponent<Animator>();    
        
        pitStopController = pitStopControllerRef.GetComponent<pitStopController>();

        origin = transform.position;
    }
    //When idle is false, runs functionality to retrieve wheel pos and run to wheels
     void Update()
    {
        if (idle == true)
        { 
            Idle();
        }
        
        if (pitStopController.pitCrewMove && running)
        {
            GetDestination(pitStopController.wheelPositions);
            StartCoroutine(RunToTarget(destination));
        }
    
    }

    //Loop through transform array and assign wheels based on id
    void GetDestination (Transform[] wheelPos)
    {
        for (int i = 0; i <= pitStopController.wheelPositions.Length; i++)
        {
            if (id == i)
            {
                destination = pitStopController.wheelPositions[i];
                break;
            }
        }
    }
    //Sets animations to false to keep idle animations
    void Idle ()
    {
        anim.SetBool("isWalking", false);
        
        anim.SetBool("idle", true);
    }
    
    //Moves actor towards wheel position which is passed as a parameter
     IEnumerator RunToTarget(Transform target)
    {        
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
    
    //Init crouching animator controller
    void Crouching(Transform target) { 
        running = false;
        
        anim.SetBool("isRunning", false);
        anim.SetBool("isCrouching", true);    
        
        pitStopController.pitCrewMove = false;
        
        StartCoroutine(TireInteraction(target.gameObject,target));
    }

    //Functionality to create tire replacment interaciton 
    IEnumerator TireInteraction(GameObject tire,Transform target)
    {
        yield return new WaitForSeconds(.2f);
        
        if (oldWheel || newWheel != null)
        {
            tire.SetActive(false);
            oldWheel.SetActive(true);
            
            yield return new WaitForSeconds(2f);
            
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

    //Reset transform after animations
    void MoveToOrigin()
    {
        oldWheel.SetActive(false);
        
        Quaternion.RotateTowards(transform.rotation, destination.rotation,90);
        
        while (navMeshAgent.remainingDistance < 0.2f)
        {
            navMeshAgent.SetDestination(origin);
        }
        
            idle = true;
            anim.SetBool("idle", true);
            running = true;
            
            pitStopController.pitCrewMove = false;
            
    }
}
