using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is needed because of the way the player cube moves
//I cannot add the camera as a child because its position and rotation
// go crazy
public class NoRotate : MonoBehaviour
{   
    [SerializeField] Transform target;

    [SerializeField]float smoothSpeed = 0.125f;

    [SerializeField] Vector3 offset;

    private Quaternion rotation;
    private void Start() {
        rotation=transform.rotation;
    }
    //run after update
    void FixedUpdate() {
        //lock rotation of camera
        transform.rotation=rotation;

        if(target!=null){
            Vector3 wantedPosition=target.position+offset;
            //lerp smoothly goes from point a to point b
            Vector3 smoothedPosition= Vector3.Lerp(transform.position,wantedPosition,smoothSpeed);
            transform.position=smoothedPosition;
        }
        
    }
}
