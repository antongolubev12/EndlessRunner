using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed=10.0f;
    [SerializeField] private  float jumpForce=10.0f;

    //[SerializeField] private  float downAccel=0.75f;
    [SerializeField] private float xSpeed=10;

    private Rigidbody rb;
    private Vector3 velocity;
    private float jumpInput;

    private BoxCollider box;
    
    private Explode explode;

    private float laneMovement=0f;

    private bool isDead=false;

    // Start is called before the first frame update
    void Start()
    {   
        rb=GetComponent<Rigidbody>();
        box=GetComponent<BoxCollider>();
        explode=gameObject.GetComponent<Explode>();
        
    }

    private void Update() {
        SwitchLanes();
    }

    private void FixedUpdate() {
        Run();
        PlayerInput();
    }

    void Run(){
        Vector3 forwardMove= transform.forward*playerSpeed*Time.deltaTime;
        rb.MovePosition(rb.position+forwardMove);
        //rb.velocity=transform.forward*playerSpeed;
        //transform.position=Vector3.MoveTowards(transform.position,new Vector3(laneMovement,transform.position.y,playerSpeed),Time.deltaTime*playerSpeed);
    }
    
    void SwitchLanes(){
        transform.position=Vector3.MoveTowards(transform.position,new Vector3(laneMovement,transform.position.y,transform.position.z),Time.deltaTime*xSpeed);
    }
    void PlayerInput(){
        //Jump
        if(IsGrounded()&& Input.GetKeyDown(KeyCode.Space))
        {   
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        }

        //Move right
        if(Input.GetKeyDown(KeyCode.D))
        {
            if(laneMovement==0)
            {
                laneMovement=2.5f;
            }
            else if(laneMovement==-2.5f)
            {
                laneMovement=0;
            }
            
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            if(laneMovement==0)
            {
                laneMovement=-2.5f;
            }
            else if(laneMovement==2.5f)
            {
                laneMovement=0;
            }
        }

    }

    private bool IsGrounded(){
        if(transform.position.y<=2){
            return true;
        }
        return false;
    }
}
