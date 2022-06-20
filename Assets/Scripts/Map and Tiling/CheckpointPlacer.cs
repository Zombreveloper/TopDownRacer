using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckpointPlacer : MonoBehaviour
{
    //referenced classes
    private TrackBuildMarker buildMarker;

    public GameObject checkpointPrefab;
    private GameObject checkpoints;
    private GameObject currentCheckpoint;
    private Tilemap myTilemap;

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



    public void PlaceCheckpoint(Vector3Int position, Vector3 rotationVec)
    {
        //string aNumber = "mit Ordnungsnummer";
        Quaternion rotation = Quaternion.Euler(rotationVec);
        Vector3 worldPosition = myTilemap.GetCellCenterWorld(position);
        //currentCheckpoint = Instantiate(checkpointPrefab, worldPosition, rotation, checkpoints.transform);
        //currentCheckpoint.name = "Checkpoint " + aNumber;
        checkpointPool.Enqueue(Instantiate(checkpointPrefab, worldPosition, rotation, checkpoints.transform));

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
