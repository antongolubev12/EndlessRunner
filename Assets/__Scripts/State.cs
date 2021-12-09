using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{   
    [SerializeField] private float stateTwo=50f;
    [SerializeField] private float stateThree=150f;

    [SerializeField] private GameObject player;
    
    public static PlayerState playerState=PlayerState.one;

    private float playerScore=0;

    public static int amount=1;

    private void Start() {
        amount=1;
    }

    private void Update() {
        playerScore=PlayerPrefs.GetFloat("Score");
        CheckState();
    }

    void CheckState()
    {   
        if(playerScore<=stateTwo)
        {
            playerState=PlayerState.one;
            amount=1;
        }

        else if(playerScore>stateTwo && playerScore<stateThree)
        {
            playerState=PlayerState.two;
            amount=2;
        }

        else if(playerScore>stateThree)
        {
            playerState=PlayerState.three;
            amount=3;
        }

        if(player==null){
           playerState=PlayerState.dead;
        }
    }

}

public enum PlayerState{
    one,
    two,
    three,
    dead
}