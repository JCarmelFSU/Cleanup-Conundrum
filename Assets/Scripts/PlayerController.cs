using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 5;
    private float p1HorizontalInput;
    private float unstuckInput;
    private float jumpInput;
    public float jumpForce = 8;
    public float gravityModifier = 1.3f;
    public bool p1IsOnGround = true;
    private int p1JumpCounter = 2;
    private Vector3 startPos = new Vector3(3, 0, 0);
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
        jumpInput = Input.GetAxis("Jump");
        unstuckInput = Input.GetAxis("Unstuck");

        p1.transform.Translate(Vector3.right * p1HorizontalInput * Time.deltaTime * speed);

        if ((Input.GetKeyDown(KeyCode.Space) && p1JumpCounter != 0) || (Input.GetKeyDown(KeyCode.Z) && p1JumpCounter != 0))
        {
            playerRb1.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            p1IsOnGround = false;
            p1JumpCounter -= 1;
        }

        if(unstuckInput >= .8f)
        {
            transform.position = startPos;
        }
   
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground") && gameObject == p1)
        {
            p1IsOnGround = true;
            p1JumpCounter = 2;
        }

        if (collision.gameObject.CompareTag("Trash"))
        {
            Destroy(collision.gameObject);
            gameManager.UpdateP1Score(1);
        }

    }

}
