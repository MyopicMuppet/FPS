using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public GameObject effectsPrefab;
    public Transform line;

    private Rigidbody rigid;
    // Start is called before the first frame update

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity.magnitude > 0) { 
        line.transform.rotation = Quaternion.LookRotation(rigid.velocity);

        }
    }
    private void OnCollisionEnter(Collision col)
    {
        // Get contact point from collision
        ContactPoint contact = col.contacts[0];
        // Spawn the Effect (i.e, Bullet Hole / Sparks)
        //Instantiate(effectsPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        // Destroy the bullet
        Destroy(gameObject);
    }

    public void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        // Add an instant force to the bullet
        rigid.AddForce(direction * speed, ForceMode.Impulse);
        // Set the line's origin (different from the bullet's starting position)
        line.transform.position = lineOrigin;
    }
}
