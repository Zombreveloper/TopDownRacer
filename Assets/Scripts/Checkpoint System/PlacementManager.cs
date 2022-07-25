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
    public ListOfActiveCars activeCars; //connect in hirachy
    GameObject firstCar;
    GameObject previousFirstCar;
    GameObject firstCarPrevFrame;

    // Start is called before the first frame update
    void Start()
    {
        activeCars = FindObjectOfType<ListOfActiveCars>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //check if car is first to collide with streen-trigger
        //then call funktion for that
        
        
        isOvertaken();

        firstCarPrevFrame = firstCar;
    }
    bool isOvertaken()
    {
        if (firstCar == firstCarPrevFrame)
        {
            return false;
        }
        
        else
        {
            Debug.Log("Overtake!");
            return true;
        }
    }

    public void FirstOne(string carsName) //gets called by CarCollisionManager when a car is the first one to enter a checkpoint
    {
        //get car with carsName from List for(int i=0; i<list.Count; i++){
        for (int i=0; i<activeCars.carsList.Count; i++)
        {
           if (activeCars.carsList[i] != null &&  activeCars.carsList[i].name == carsName)
           {
               previousFirstCar = firstCar;
               firstCar = activeCars.carsList[i];
           }
        }
    }

 

    public GameObject getFirstPlaced()
    {
        return this.firstCar;
    }

    public GameObject getPreviousFirstPlaced()
    {
        return this.previousFirstCar;
    }
}
