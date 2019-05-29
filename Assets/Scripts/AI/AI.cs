using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{


    public float maxVelocity = 5f, maxDistance = 5f;
    protected NavMeshAgent agent;
    protected Vector3 velocity;
    protected SteeringBehaviour[] behaviours;

    public Vector3 Velocity
    {
        protected set { velocity = value; }
        get { return velocity; }
    }

    void Update()
    {
        CalculateForce();
    }

    // Start is called before the first frame update
    void Awake()
    {
        //get nav componenent
        agent = GetComponent<NavMeshAgent>();
        //Get all steering behaviours on AI
        behaviours = GetComponents<SteeringBehaviour>();
    }


    //Calculates all forces from all behaviours
    public virtual Vector3 CalculateForce()
    {
        //step 1). create a result Vector3
        //set force to zero
        Vector3 force = Vector3.zero;

        //step 2). loop through all behaviours and get force
        foreach (var behaviour in behaviours)
        {
            //apply force to behaviour.GetForce x Weighting
            force += behaviour.GetForce() * behaviour.weighting;
            //if force magnitude > maxSpeed
            //step 3). Limit the total force to max speed
            if (force.magnitude > maxVelocity)
            {
                //Set force to force normalised x maxSpeed
                force = force.normalized * maxVelocity;
                //break - exits the loop
                break;

            }
        }


        //step 4). Limit the total velocity to our max velocity if it exceeds
        velocity += force * Time.deltaTime;
        //if velocity magnitude > max velocity
        if (velocity.magnitude > maxVelocity)
        {
            // set velocity to velocity normalised x max velocity
            velocity = velocity.normalized * maxVelocity;

        }

        //step 5). sample destination for navmeshagent
        //if velocity magnitude > 0 (velocity not zero)
        if (velocity.magnitude > 0)
        {
            //set pos to curent (position) + velocity x delta
            Vector3 pos = transform.position + velocity * Time.deltaTime;
            NavMeshHit hit;
            //  IF NavMesh SamplePosition within NavMesh
            if (NavMesh.SamplePosition(pos, out hit, maxDistance, -1))
            {
                // set agent destination to hit position
                agent.SetDestination(hit.position);
            }
        }


        //step 6). return force
        return force;
    }
}
