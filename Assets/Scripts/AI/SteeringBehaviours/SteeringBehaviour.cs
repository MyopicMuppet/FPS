using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    public float weighting = 7.5f;
    //reference to owner (for getting velocity)
    protected AI owner;
    // Start is called before the first frame update
    void Awake()
    {
        //get the AI that this steering behaviour is attached to
        owner = GetComponent<AI>();
    }
       
    public virtual Vector3 GetForce()
    {
        //set force to zero
        Vector3 force = Vector3.zero;

        // Do nothing in the base class (always returns zero)

        // Return force
        return force;
    }
}
