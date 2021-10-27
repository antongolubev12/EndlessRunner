using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed=10.0f;
    [SerializeField] float jumpForce=10.0f;

    [SerializeField] float downAccel=0.75f;

    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 run;
    private float jumpInput;
    private bool isGrounded=true;

    // Start is called before the first frame update
    void Start()
    {   
        // run= new Vector3(0f,0f,playerSpeed);
        // jump= new Vector3(0f,jumpForce,0f);
        rb=GetComponent<Rigidbody>();
        velocity=Vector3.zero;
    }

    private void Update() {
        
    }

    private void FixedUpdate() {
        
        PlayerInput();
        Run();
        
        Jump();
        CheckGrounded();
        rb.velocity=velocity;
        
    }
    void Run(){
        //rb.AddForce(Vector3.forward*playerSpeed,ForceMode.Force);
        //rb.velocity=velocity;
        velocity.z=playerSpeed;
        //transform.position+=run;
    }

    void PlayerInput(){
        if(Input.GetKeyDown(KeyCode.Space))
        {   
            jumpInput=1;
        }

    }

    void Jump(){
        if(jumpInput==1 && isGrounded){
            velocity.y=jumpForce;
        }

        else if(jumpInput==0 && isGrounded){
            velocity.y=0;
        }
        
        else{
             velocity.y-=downAccel;
         }
        
        jumpInput=0;


    }

    void CheckGrounded(){
        //Create a ray pointing down to check if player is grounded
        Ray ray= new Ray(transform.position + Vector3.up*0.1f, Vector3.down);

        RaycastHit[] hits= Physics.RaycastAll(ray,0.5f);

        isGrounded=false;
        rb.useGravity=true;

        print(velocity.y);
        foreach(var hit in hits)
        {
            if(!hit.collider.isTrigger){
                if(velocity.y<=0){
                    print("In velocity");
                    rb.position=Vector3.MoveTowards(rb.position,
                        new Vector3(hit.point.x,hit.point.y+0.1f,hit.point.z),Time.deltaTime*10);
                }
                rb.useGravity=false;
                isGrounded=true;
                break;
            }
        }
    }

}
