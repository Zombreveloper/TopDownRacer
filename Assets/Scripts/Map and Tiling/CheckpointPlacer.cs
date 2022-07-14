using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* This class is only responsible for placing the Checkpoint-Prefab
 * in the right place
 * and making sure that there are not more than 5 Checkpoints at a time.
 * it has nothing to do with the functionality of the checkpoints
 * (maybe not true since the names may also be set here ((BAD DESIGN!!!)))
 */

public class CheckpointPlacer : MonoBehaviour
{
    //referenced classes
    private TrackBuildMarker buildMarker;

    //used Objects
    public GameObject checkpointPrefab;
    private GameObject checkpoints;
    private GameObject currentCheckpoint;
    private Tilemap myTilemap;

    //variables
    private Queue<GameObject> checkpointPool = new Queue<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        buildMarker = FindObjectOfType<TrackBuildMarker>();
        myTilemap = GameObject.Find("/Grid/Streetmap").GetComponent<Tilemap>();
        checkpoints = GameObject.Find("/Checkpoints");
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void PlaceCheckpoint(Vector3Int position, Vector3 rotationVec) //gets called by MapBuilder.placePattern
    {
        //string aNumber = "mit Ordnungsnummer";
        Quaternion rotation = Quaternion.Euler(rotationVec);
        Vector3 worldPosition = myTilemap.GetCellCenterWorld(position);
        //currentCheckpoint = Instantiate(checkpointPrefab, worldPosition, rotation, checkpoints.transform);
        //currentCheckpoint.name = "Checkpoint " + aNumber;

        currentCheckpoint = Instantiate(checkpointPrefab, worldPosition, rotation, checkpoints.transform);
        currentCheckpoint.name = "HenryDerCheckpointMaster";
        checkpointPool.Enqueue(currentCheckpoint);


        if (checkpointPool.Count > 5)
        {
            Destroy(checkpointPool.Dequeue());
        }
    }

    public GameObject GetCheckpoint()
    {
        return this.currentCheckpoint;
    }
}
