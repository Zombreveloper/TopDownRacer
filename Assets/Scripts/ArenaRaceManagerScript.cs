using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaRaceManagerScript : MonoBehaviour
{
    //referenced classes

    //referenced GameObject
    public List<GameObject> allWayPoints = new List<GameObject>();

    //referenced variables
    bool doRace = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitiateArenaRace()
    {
        //Set all WayPoints to Active or something similar...
        //Debug.Log("Start ArenaRace");

        doRace = true;

        foreach (GameObject waypoint in allWayPoints)
        {
            waypoint.SetActive(true);
        }
    }
}
