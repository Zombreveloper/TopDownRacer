using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPlacer : MonoBehaviour
{
    //referenced classes
    private TrackBuildMarker buildMarker;

    public GameObject checkpointPrefab;
    private GameObject currentCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        buildMarker = FindObjectOfType<TrackBuildMarker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceCheckpoint(Vector3Int position, Vector3 rotationVec)
    {
        Quaternion rotation = Quaternion.Euler(rotationVec);
        currentCheckpoint = Instantiate(checkpointPrefab, position, rotation);
    }

    public GameObject GetCheckpoint()
    {
        return this.currentCheckpoint;
    }
}
