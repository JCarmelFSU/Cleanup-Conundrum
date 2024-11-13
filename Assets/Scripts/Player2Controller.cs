using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{

    public float speed = 5;
    public float p2HorizontalInput;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool p2IsOnGround = true;
    private int p2JumpCounter = 2;
    private Rigidbody playerRb2;
    private GameObject p2;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb2 = GameObject.Find("Johnny").GetComponent<Rigidbody>();
        p2 = GameObject.Find("Johnny");
    }

    // Update is called once per frame
    void Update()
    {
        p2HorizontalInput = Input.GetAxis("P2Horizontal");

        p2.transform.Translate(Vector3.right * p2HorizontalInput * Time.deltaTime * speed);

       
        if (Input.GetKeyDown(KeyCode.UpArrow) && p2JumpCounter != 0)
        {
            playerRb2.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            p2IsOnGround = false;
            p2JumpCounter -= 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.CompareTag("Ground") && gameObject == p2)
        {
            p2IsOnGround = true;
            p2JumpCounter = 2;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == gameObject.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
            gameManager.UpdateP2Score(1);

        }
    }
}
