using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTile : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public GameObject[] obstacles; //Objects that contains different obstacle types which will be randomly activated
    private int amount;

    private void Update() {
        amount=State.amount;
    }
    public void ActivateRandomObstacle()
    {   
        DeactivateAllObstacles();
        for(int i=0;i<amount; i++){
            //print("Activating obstacle, amount is: "+amount);
            int randomNumber = Random.Range(0, obstacles.Length);
            obstacles[randomNumber].SetActive(true);
        }
        //print("Loop ended");
    }

    public void DeactivateAllObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }
    }
}