using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{   
    [SerializeField] GameObject effect;

    [SerializeField] float duration;

    [SerializeField] float xMultiplier;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(PickUp(other));
        }
    }
    
    //Co routine
    IEnumerator PickUp(Collider player)
    {   
        // Instantiate(effect,transform.position, transform.rotation);
        
        PlayerMovement playerMovement= player.GetComponent<PlayerMovement>();
        
        //Mulitply xSpeed by multiplier
        playerMovement.xSpeed*=xMultiplier;
        
        //disable mesh rendered so that powerup disappears
        GetComponent<MeshRenderer>().enabled=false;

        //wait for the specified duration
        yield return new WaitForSeconds(duration);

        //divide by multiplier to return to normal speed
        playerMovement.xSpeed/=xMultiplier;

        //destroy powerup
        Destroy(gameObject);
    }
}
