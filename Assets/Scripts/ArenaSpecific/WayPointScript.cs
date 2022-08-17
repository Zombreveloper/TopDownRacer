using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointScript : MonoBehaviour
{
    ArenaRaceManagerScript myManager;
    bool isColliding; //used to prevent multiple Triggers in one Frame

    // Start is called before the first frame update
    void Start()
    {
        myManager = FindObjectOfType<ArenaRaceManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false; //used to prevent multiple Triggers in one Frame
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding) //used to prevent multiple Triggers in one Frame
        {
            return;
        }
        isColliding = true; //used to prevent multiple Triggers in one Frame

        
        if (other.tag == "Car")
        {
            Debug.Log("WayPointTrigger");
            myManager.UpdateWayPoints();
        }
    }
}
