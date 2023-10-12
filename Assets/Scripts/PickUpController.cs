using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    // public BallController ballScript;
    public Rigidbody rb;
    public SphereCollider collider;
    public Transform player, ballContainer, camera;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        // setup
        if(!equipped)
        {
            // ballScript.enabled = false; if you have one
            // rb.isKinematic = false;
            rb = gameObject.AddComponent<Rigidbody>();
            collider.isTrigger = false;
        }
        if (equipped)
        {
            // ballScript.enabled = true; if you have one
            // rb.isKinematic = true;
            Destroy(rb);
            collider.isTrigger = true;
            slotFull = true;
        }
    }
    void Update()
    {
        // check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        // drop if equipped and "Q" is pressed
        if(equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        // make ball a child of the hand and move it to default position.
        transform.SetParent(ballContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // make Rigidbody Kinematic and BoxCollider a trigger
        // rb.isKinematic = true;
        Destroy(rb);
        collider.isTrigger = true;

        // enable ballScript if you have one
        // ballScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        // set parent to null
        transform.SetParent(null);

        // make Rigidbody not Kinematic and BoxCollider a normal
        // rb.isKinematic = false;
        rb = gameObject.AddComponent<Rigidbody>();
        collider.isTrigger = false;

        // ball carries momentum of the player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        // add force
        rb.AddForce(camera.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(camera.up * dropUpwardForce, ForceMode.Impulse);

        // add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        // disable ballScript if you have one
        // ballScript.enabled = false;
    }
}
