using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{

    public Rigidbody garbageRb;
    public BoxCollider garbageCollider;
    public Transform player, garbageContainer;
    public GameObject player1;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool held = false;
    public static bool slotFull = false;
    // Start is called before the first frame update
    void Start()
    {
        //Startup

        player1 = GameObject.Find("Donna");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!held && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) {
            Debug.Log("Picked up!");
            PickUp();
        }

        if (held && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }

        // Destroy garbage in collection zone
        if (transform.position.x >= 3)
        {
            Destroy(gameObject);
        }
    }

    private void PickUp()
    {
        held = true;
        slotFull = true;

        //Make garbage child of player and move it to position

        transform.SetParent(player);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic & BoxCollider trigger

        garbageRb.isKinematic = true;
        garbageCollider.isTrigger = true;
    }

    private void Drop()
    {
        held = false;
        slotFull = false;

        transform.SetParent(null);

        //Throwing garbage

        garbageRb.velocity = player1.GetComponent<Rigidbody>().velocity;

        garbageRb.AddForce(player.forward * dropForwardForce, ForceMode.Impulse);
        garbageRb.AddForce(player.forward * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        garbageRb.AddTorque(new Vector3(random, random, random));

        //Make Rigidbody not kinematic & BoxCollider trigger

        garbageRb.isKinematic = true;
        garbageCollider.isTrigger = true;
    }
}
