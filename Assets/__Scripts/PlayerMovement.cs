using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpForce = 10.0f;

    public float xSpeed = 10;

    [SerializeField] private float speedIncrease = 0.05f;

    private Rigidbody rb;

    private float jumpInput;

    private BoxCollider box;

    private Explode explode;

    private float laneMovement = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
        explode = gameObject.GetComponent<Explode>();
    }

    private void Update()
    {
        SwitchLanes();
        PlayerInput();
        IncreaseSpeed();
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void IncreaseSpeed()
    {
        //only increase speed when game isnt paused
        if (Time.timeScale == 1)
        {
            playerSpeed = playerSpeed * speedIncrease;
        }

    }

    void Run()
    {   
        Vector3 forwardMove = transform.forward * playerSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMove);
        //rb.velocity=transform.forward*playerSpeed;
    }

    void SwitchLanes()
    {   
                            //move player from:     current position   ->   desired position                          
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(laneMovement, transform.position.y, transform.position.z),
        //distance to move per call. Time.delta time allows for smoothness
        Time.deltaTime * xSpeed);
    }
    
    void PlayerInput()
    {
        //Jump
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //Move right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (laneMovement == 0)
            {
                laneMovement = 2.5f;
            }
            else if (laneMovement == -2.5f)
            {
                laneMovement = 0;
            }

        }
        //Move left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (laneMovement == 0)
            {
                laneMovement = -2.5f;
            }
            else if (laneMovement == 2.5f)
            {
                laneMovement = 0;
            }
        }
    }

    private bool IsGrounded()
    {
        if (transform.position.y <= -1.5f)
        {
            return true;
        }
        return false;
    }

}
