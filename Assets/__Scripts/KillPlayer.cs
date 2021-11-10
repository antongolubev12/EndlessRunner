using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private bool isDead = false;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        print("Colliding with " + other.collider.tag);
        if (other.collider.tag == "Enemy")
        {
            //Destroy(gameObject);
        }
    }
}
