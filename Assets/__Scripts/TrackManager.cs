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
        GameObject p;
        
        for(int i=0; i<numOfTracks;i++){
            
            if(i==0){
                trackNumber=0;
            }
            else{
                trackNumber=Random.Range(0,tracks.Length);
            }

            //int size=
            p=Instantiate(tracks[trackNumber],pos,Quaternion.identity);

            if (p.tag=="Track")
            {
                pos.z+=10;
            }

            else if(p.tag=="Jump")
            {
                pos.z+=3;
            }
            
            // print(p.tag+" loop: "+i);
            // print(pos);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
