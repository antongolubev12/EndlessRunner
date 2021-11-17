using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create platforms at the start
public class TrackManager : MonoBehaviour
{
    [SerializeField] GameObject[] tracks;
    [SerializeField] int numOfTracks=8;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos= new Vector3(0,0,0);
        int trackNumber;
        GameObject current;
        GameObject lastTrack=null;
        
        for(int i=0; i<numOfTracks;i++){
            
            //control first two tracks
            if(i==0||i==1){
                trackNumber=0;
            }
            else{
                trackNumber=Random.Range(0,tracks.Length);
            }

            current=tracks[trackNumber];
            
            
            //first loop
            if(lastTrack==null)
            {
                pos.z+=10;
            }
            
            //dont want two jumps in a row
            else if(current.tag=="EnemyJump" && lastTrack.tag=="EnemyJump")
            {
                continue;
            } 
            else if(current.tag=="EnemyJump" && lastTrack.tag=="Track")
            {
                pos.z+=6.5f;
            }
            else if(current.tag=="Track" && lastTrack.tag=="Track")
            {
                pos.z+=10;
            }
            else if(current.tag=="Track" && lastTrack.tag=="EnemyJump")
            {
                pos.z+=6.5f;
            }
            
            current=Instantiate(tracks[trackNumber],pos,Quaternion.identity);

            lastTrack=current;

            print("Loop: "+i+" Current: "+current.tag+" Last Track: "+lastTrack.tag);
            print(pos);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
