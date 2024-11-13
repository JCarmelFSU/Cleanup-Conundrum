using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{

    public float speed = 5;
    public float p2HorizontalInput;
    public float jumpForce = 8;
    public float gravityModifier = 1.3f;
    private float unstuckInputP2;
    private float jumpInputP2;
    public bool p2IsOnGround = true;
    private int p2JumpCounter = 2;
    private Vector3 startPosP2 = new Vector3(4, 0, 0);
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
        jumpInputP2 = Input.GetAxis("JumpP2");
        unstuckInputP2 = Input.GetAxis("UnstuckP2");

        p2.transform.Translate(Vector3.right * p2HorizontalInput * Time.deltaTime * speed);


        if ((Input.GetKeyDown(KeyCode.Alpha7) && p2JumpCounter != 0) || (Input.GetKeyDown(KeyCode.Alpha4) && p2JumpCounter != 0) || (Input.GetKeyDown(KeyCode.Keypad7) && p2JumpCounter != 0) || (Input.GetKeyDown(KeyCode.Keypad4) && p2JumpCounter != 0))
        {
            playerRb2.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            p2IsOnGround = false;
            p2JumpCounter -= 1;
        }

        if (unstuckInputP2 >= .8f || Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            transform.position = startPosP2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.CompareTag("Ground") && gameObject == p2)
        {
            p2IsOnGround = true;
            p2JumpCounter = 2;
        }

        if (collision.gameObject.CompareTag("Trash"))
        {
            Destroy(collision.gameObject);
            gameManager.UpdateP2Score(1);
        }

    }


}
