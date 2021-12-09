using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create platforms at the start
public class TrackManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stageOneTracks;
    [SerializeField] private int numOfStageOne;

    [SerializeField] private GameObject[] stageTwoTracks;
    [SerializeField] private int numOfStageTwo;
    
    [SerializeField] private GameObject[] stageThreeTracks;
    [SerializeField] private int numOfStageThree;
    
    [SerializeField] private GameObject floor;
   
    private int stageCouter;

    private Stages stage=Stages.stageOne;

    private float playerScore;

    private GameObject lastTrack;

    //lastTrackZ keeps track of z position of the last created track
    private float lastTrackZ;


    // Start is called before the first frame update
    void Start()
    {   
        lastTrackZ=0f;
    }

    void Update() {
        playerScore=PlayerPrefs.GetFloat("Score");
        CheckScore();
    }

    void CreateTrack(GameObject[] tracks, int numOfTracks)
    {
        Vector3 pos = new Vector3(0, 0, 0);
        int trackNumber;
        GameObject current;
        //GameObject lastTrack = null;

        for (int i = 0; i < numOfTracks; i++)
        {

            //control first track
            if (i == 0 || i == 1 || i == 2 )
            {
                trackNumber = 0;
            }
            else
            {
                trackNumber = Random.Range(0, tracks.Length);
            }

            current = tracks[trackNumber];


            //first loop
            if (i==0)
            {
                //if starting the loop for the next stage, set the offset for the new track generation
                pos.z=lastTrackZ;
                pos.z += 10;
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

            //pointer in the loop that keeps track of the last created track
            lastTrack = current;

            //keep track of the last z position if this function needs to be run again
            lastTrackZ=pos.z;
            //print(lastTrackZ);

            print("Loop: " + i + " Current: " + current.tag + " Last Track: " + lastTrack.tag);
            print(pos);
            CreateFloor(lastTrackZ);
        }
    }

    void CreateFloor(float z)
    {  
        float numOfTracks=z/20;
        Vector3 left = new Vector3(-16.5f, 2.5f, 0);
        Vector3 right = new Vector3(15, 2.5f, 0);

        for(int i=0;i<numOfTracks;i++){
            Instantiate(floor, left, Quaternion.identity);
            left.z+=22f;

            Instantiate(floor, right, Quaternion.identity);
            right.z+=22f;
        }
    }
    void CheckScore()
    {   
        switch(stage)
        {   
            case Stages.stageOne:
                if(playerScore>lastTrackZ*.5 && stageCouter==0)
                {
                    CreateTrack(stageOneTracks,numOfStageOne);
                    stage=Stages.stageTwo;
                    stageCouter++;
                }
                break;
            
            case Stages.stageTwo:
                if(playerScore>lastTrackZ*.5 && stageCouter==1)
                {
                    CreateTrack(stageTwoTracks,numOfStageTwo);
                    stage=Stages.stageThree;
                    stageCouter++;
                }
                break;
            
            case Stages.stageThree:
                if(playerScore>lastTrackZ*.5 && stageCouter>1)
                {
                    CreateTrack(stageThreeTracks,numOfStageThree);
                    stage=Stages.stageThree;
                    stageCouter++;
                }
                break;
        }

        //if player score is half of the length of stage one tracks, spawn stage two tracks
        //incCounter prevents this from being called every frame
        // if(PlayerPrefs.GetFloat("Score")>stageOneTracks.Length/2 && stageCouter==0)
        // {
        //     print("stage two");
        //     CreateTrack(stageTwoTracks,numOfStageTwo);
        //     stageCouter++;
        // }

        // else if(PlayerPrefs.GetFloat("Score")>stageTwoTracks.Length/2 && stageCouter==1)
        // {
        //     print("stage three");
        //     CreateTrack(stageThreeTracks,numOfStageThree);
        //     stageCouter++;
        // }
    }

        public enum Stages
    {
        stageOne,
        stageTwo,
        stageThree,
    }
}
