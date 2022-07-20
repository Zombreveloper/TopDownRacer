using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* This class is only responsible for placing the Checkpoint-Prefab
 * in the right places
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
    private GameObject currentlyBuiltCheckpoint;
    private GameObject activeCheckpoint;
    private Tilemap myTilemap;

    //variables
    private Queue<GameObject> checkpointPool = new Queue<GameObject>();
    private int checkpointCounter = 1;

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


    //Das ist die bisher funktionierende Backup Methode!
    public void PlaceCheckpointt(Vector3Int position, Vector3 rotationVec) //gets called by MapBuilder.placePattern
    {
        //string aNumber = "mit Ordnungsnummer";
        Quaternion rotation = Quaternion.Euler(rotationVec);
        Vector3 worldPosition = myTilemap.GetCellCenterWorld(position);
        //currentCheckpoint = Instantiate(checkpointPrefab, worldPosition, rotation, checkpoints.transform);
        //currentCheckpoint.name = "Checkpoint " + aNumber;


        currentlyBuiltCheckpoint = Instantiate(checkpointPrefab, worldPosition, rotation, checkpoints.transform);
        currentlyBuiltCheckpoint.name = "HenryDerCheckpointMaster";
        checkpointPool.Enqueue(currentlyBuiltCheckpoint);


        if (checkpointPool.Count > 5)
        {
            Destroy(checkpointPool.Dequeue());
        }
    }

    public void PlaceCheckpoint(Vector3Int position, Vector3 rotationVec) //gets called by MapBuilder.placePattern
    {
        string numberString = checkpointCounter.ToString();
        Quaternion rotation = Quaternion.Euler(rotationVec);
        Vector3 worldPosition = myTilemap.GetCellCenterWorld(position);

        currentlyBuiltCheckpoint = Instantiate(checkpointPrefab, worldPosition, rotation, checkpoints.transform);
        currentlyBuiltCheckpoint.name = "Checkpoint " + numberString;
        checkpointPool.Enqueue(currentlyBuiltCheckpoint);

        /* replaced by public destroyChekcpoint
        if (checkpointPool.Count > 3)
        {
            Destroy(checkpointPool.Dequeue());
        }
        */

        SetActiveCheckpoint();
        checkpointCounter++;
    }

    void SetActiveCheckpoint() //currently next checkpoint to drive through
    {
        activeCheckpoint = checkpointPool.Peek();
        Debug.Log("The active Checkpoint is " + activeCheckpoint.name);
    }

    //public methods
    public void destroyCheckpoint() //gets called my Checkpoint Script OnTriggerEnter
    {
        Destroy(checkpointPool.Dequeue());
        Debug.Log("destroyCheckpoint gets called");
    }

    public GameObject getActiveCheckpoint()
    {
        return this.activeCheckpoint;
    }
}
