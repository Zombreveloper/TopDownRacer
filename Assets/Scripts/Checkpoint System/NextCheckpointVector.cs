using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Vector that always points from the players cars to the next checkpoint
 * ("relevantCheckpoint" in Class CheckpointPlacer)
 */

public class NextCheckpointVector : MonoBehaviour
{

    //referenced classes
    CheckpointPlacer checkpointPlacer;
    ListOfActiveCars activeCars;
    GameObject[] activeCarObjects;

    //used objects
    GameObject activeCheckpoint;

    //used variables
    Vector3 distance;

    private void Awake()
    {
        checkpointPlacer = FindObjectOfType<CheckpointPlacer>();
        //I don't know yet which of those I need
        activeCars = FindObjectOfType<ParticipantsManager>().GetComponent<ListOfActiveCars>();
        activeCarObjects = GameObject.FindGameObjectsWithTag("Car");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (activeCars != null)
            Debug.Log(activeCars.carsList[0]);
    }

    // Update is called once per frame
    void Update()
    {
        activeCheckpoint = checkpointPlacer.getActiveCheckpoint();
        foreach (GameObject car in activeCars.carsList)
        {
            distance = car.transform.position - checkpointPlacer.getActiveCheckpoint().transform.position;
            float length = distance.magnitude;
            Debug.Log("distance from checkpoint to " + car.name + " is " + length);

            //TODO: Tidy this mess up, save the values, compare them and make a whoIsFirst message out of it
        }
    }


}
