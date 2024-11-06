using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;
    public float horizontalInput;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    private int jumpCounter = 2;
    private Rigidbody playerRb;
    public float collectionRange = 3;
    public bool hasItem = false;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter != 0)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            jumpCounter -= 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCounter = 2;
        }
        else if (collision.gameObject.CompareTag("Garbage") && Input.GetKeyDown(KeyCode.E))
        {
            hasItem = true;
        }
        
    }

    
}
