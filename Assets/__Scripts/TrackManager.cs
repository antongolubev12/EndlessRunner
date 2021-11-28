using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create platforms at the start
public class TrackManager : MonoBehaviour
{
    [SerializeField] GameObject[] tracks;

    [SerializeField] GameObject[] stageOneTracks;
    [SerializeField] int numOfStageOne;

    [SerializeField] GameObject[] stageTwoTracks;
    [SerializeField] int numOfStageTwo;
    
    [SerializeField] GameObject[] stageThreeTracks;
    [SerializeField] int numOfStageThree;
    
    private float lastPos;

    // Start is called before the first frame update
    void Start()
    {   
        lastPos=0f;
        CreateTrack(stageOneTracks,numOfStageOne);
    }

    void Update() {
        CheckScore();
    }

    void CreateTrack(GameObject[] tracks, int numOfTracks)
    {
        Vector3 pos = new Vector3(0, 0, 0);
        int trackNumber;
        GameObject current;
        GameObject lastTrack = null;

        for (int i = 0; i < numOfTracks; i++)
        {

            //control first track
            if (i == 0 || i == 1)
            {
                trackNumber = 0;
            }
            else
            {
                trackNumber = Random.Range(0, tracks.Length);
            }

            current = tracks[trackNumber];


            //first loop
            if (lastTrack == null)
            {
                pos.z += 10;
                //if starting the loop for the next stage, set the offset for the new track generation
                //default is zero, so there is no loop
                pos.z=lastPos;
            }

            //dont want two jumps in a row
            else if (current.tag == "EnemyJump" && lastTrack.tag == "EnemyJump")
            {
                continue;
            }
            //control the spawn distance base on the type of track spawned
            else if (current.tag == "EnemyJump" && lastTrack.tag == "Track")
            {
                pos.z += 6.5f;
            }
            else if (current.tag == "Track" && lastTrack.tag == "Track")
            {
                pos.z += 10;
            }
            else if (current.tag == "Track" && lastTrack.tag == "EnemyJump")
            {
                pos.z += 6.5f;
            }

            
            current = Instantiate(tracks[trackNumber], pos, Quaternion.identity);

            //create a pointer in the loop that keeps track of the last created track
            lastTrack = current;

            lastPos=pos.z;

            // print("Loop: " + i + " Current: " + current.tag + " Last Track: " + lastTrack.tag);
            // print(pos);

        }
    }

    void CheckScore()
    {
        if(PlayerPrefs.GetFloat("Score")>stageOneTracks.Length/2)
        {
            print("stage two");
            CreateTrack(stageTwoTracks,numOfStageTwo);
        }
    }
}
