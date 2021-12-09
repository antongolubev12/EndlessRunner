using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    public static bool isPaused=false;

   [SerializeField] private GameObject deadUI;

    // Update is called once per frame
    private void Update() {
        CheckIfDead();
    }

    public void CheckIfDead()
    {
        if(State.playerState==PlayerState.dead){
            print("Player dead!!");
            deadUI.SetActive(true);
        }
    }
}
