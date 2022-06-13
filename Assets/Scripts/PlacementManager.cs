using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
is responsible for looking wich car is first, and then measuring who is last.

The race-track has colliders with the "CheckpointScript" attached.
The cars all have a collider with the "CarCollisionManager" attached.
if the "CarCollisionManager" collides with a "CheckpointScript", the car asks if
it ist the first one to collide with the checkpoint.
If it is, the checkpoint returns true, so the car calls "PlacementManager"s
"void FirstOne".
*/

public class PlacementManager : MonoBehaviour
{
    public ListOfActiveCars activeCars; //connet in hirachy
    GameObject firstCar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //check if car is first to collide with streen-trigger
        //then call funktion for that
    }

    public void FirstOne(string carsName) //gets called when a car is the first one to enter a checkpoint
    {
        //get car with carsName from List for(int i=0; i<list.Count; i++){
        for (int i=0; i<activeCars.carsList.Count; i++)
        {
           if (activeCars.carsList[i].name == carsName)
           {
               firstCar = activeCars.carsList[i];
           }
        }
    }

    void LastOne()
    {
        //check distance from the firstCar to others
        //or not Marv?
        //because camera view shall kick the plaxers
    }
}
