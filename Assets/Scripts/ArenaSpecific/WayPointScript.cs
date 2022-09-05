using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WayPointScript : MonoBehaviour
{
    ArenaRaceManagerScript myManager;
    bool isColliding; //used to prevent multiple Triggers in one Frame

    //public static event Action OnCarGotWaypoint; //Remnant of the Event System

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
        //Debug.Log("i collided with " + other.transform.parent);
        if (isColliding) //used to prevent multiple Triggers in one Frame
        {
            return;
        }
        isColliding = true; //used to prevent multiple Triggers in one Frame


        if (other.transform.parent.tag == "Car")
        {
            //Debug.Log("WayPointTrigger");
            myManager.UpdateWayPoints();
            myManager.PunishOthers(other.transform.parent.name);

            //call event
            //OnCarGotWaypoint?.Invoke(); //Remnant of the Event System
        }
    }
}
