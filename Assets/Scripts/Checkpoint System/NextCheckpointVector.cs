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
    CheckpointScript checkpoints;
    ListOfActiveCars activeCars;
    GameObject[] activeCarObjects;

    //used objects
    GameObject activeCheckpoint;

    //used variables
    //Vector3 distance;

    private void Awake()
    {
        checkpointPlacer = FindObjectOfType<CheckpointPlacer>();
        checkpoints = FindObjectOfType<CheckpointScript>();
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
        determineFirstPlaced();
    }

    void determineFirstPlaced()
    {
        activeCheckpoint = checkpointPlacer.getActiveCheckpoint();

        float distance;
        float currentNearest = 1024; //incredibly high so first car will always overwrite
        GameObject nearestCar = activeCars.carsList[0];

        foreach (GameObject car in activeCars.carsList)
        {
            if (car != null)
            {
                Vector3 carPos = car.transform.position; //current car position
                Vector3 cPPos = checkpointPlacer.getActiveCheckpoint().transform.position; //checkpoint position
                Vector3 carToCP = carPos - cPPos;
                Debug.DrawLine(carPos, cPPos, Color.white, 0.0f, false);
                distance = carToCP.magnitude;
                //Debug.Log("distance from checkpoint to " + car.name + " is " + distance);

                if (currentNearest > distance)
                {
                    currentNearest = distance;
                    nearestCar = car;
                }
            }

        }

        nearestCar.GetComponent<CarCollisionManager>().callFirstOne();

        //Debug.Log(nearestCar + " is currently the nearest with" + currentNearest);
    }
}
