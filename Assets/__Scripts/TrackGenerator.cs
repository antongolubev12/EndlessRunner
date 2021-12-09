using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrackGenerator : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform startPoint; //Point from where tracks will start to
    [SerializeField] private TrackTile tilePrefab;
    [SerializeField] private float movingSpeed = 12;
    [SerializeField] private int tilesToPreSpawn = 15; //How many tiles should be pre-spawned
    [SerializeField] private int tilesWithoutObstacles = 3; //How many tiles at the beginning should not have obstacles, good for warm-up
    
    //keep track of the spawned tiles
    private List<TrackTile> spawnedTiles = new List<TrackTile>();
    

    // Start is called before the first frame update
    void Start()
    {   
        InstanciateTrack();        
    }

    void InstanciateTrack()
    {
        Vector3 spawnPosition = startPoint.position;
        int tilesWithNoObstaclesTmp = tilesWithoutObstacles;
        for (int i = 0; i < tilesToPreSpawn; i++)
        {
            spawnPosition -= tilePrefab.startPoint.localPosition;
            TrackTile spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity) as TrackTile;

            //if start of track then deactivate obstacles
            if(tilesWithNoObstaclesTmp > 0)
            {
                spawnedTile.DeactivateAllObstacles();
                tilesWithNoObstaclesTmp--;
            }
            else
            {
                spawnedTile.ActivateRandomObstacle();
            }
            
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        //if player is dead don't move track
        if(State.playerState!=PlayerState.dead)
        {
            // Move the object upward in world space x unit/second.
            transform.Translate(-spawnedTiles[0].transform.forward * Time.deltaTime * movingSpeed, Space.World);        

            //if  
            if (playerCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < 0)
            {
                //Move the tile to the front if it's behind the Camera
                TrackTile tileTmp = spawnedTiles[0];
                spawnedTiles.RemoveAt(0);
                tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
                tileTmp.ActivateRandomObstacle();
                spawnedTiles.Add(tileTmp);
            }
        }
        
    }
}