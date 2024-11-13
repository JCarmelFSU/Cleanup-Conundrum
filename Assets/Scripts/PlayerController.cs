using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;
    public float p1HorizontalInput;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool p1IsOnGround = true;
    private int p1JumpCounter = 2;
    private Rigidbody playerRb1;
    private GameObject p1;
    private int p1Score;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb1 = GameObject.Find("Donna").GetComponent<Rigidbody>();
        p1 = GameObject.Find("Donna");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        p1HorizontalInput = Input.GetAxis("P1Horizontal");


        p1.transform.Translate(Vector3.right * p1HorizontalInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space) && p1JumpCounter != 0)
        {
            playerRb1.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            p1IsOnGround = false;
            p1JumpCounter -= 1;
        }
   
    }

    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.CompareTag("Ground") && gameObject == p1)
        {
            p1IsOnGround = true;
            p1JumpCounter = 2;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == gameObject.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
            gameManager.UpdateP1Score(1);
        }
        
        
    }
  
}
