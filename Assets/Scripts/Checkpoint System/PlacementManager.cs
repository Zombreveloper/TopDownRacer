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
    GameObject firstCarLastFrame;
    //GameObject firstCarPrevFrame; //doesn't get used yet
    GameObject previousFirst; //only gets updated when first and second place overtake each other

    //variables
    public bool isOvertaken;

    // Start is called before the first frame update
    void Start()
    {
        activeCars = FindObjectOfType<ListOfActiveCars>();
        firstCar = activeCars.getCarFromList(0); //only works as intended if car 0 really is first on in beginning!
    }

    // Update is called once per frame
    private void Update()
    {
       // if (firstCarPrevFrame != null)
            //Debug.Log("first Car was" + firstCarPrevFrame.name);
       // Debug.Log("first Car is currently" + firstCar.name);

        
    }
    void LateUpdate()
    {
       

        
    }
    bool OnOvertake()
    {
        if (firstCar == firstCarLastFrame)
        {
            return false;
        }
        
        else
        {
            Debug.Log("Overtake!");
            if (firstCarLastFrame != null)
            Debug.Log("First Car on overtake is" + firstCar.name + "and previous is" + firstCarLastFrame.name);
            previousFirst = firstCarLastFrame;
            return true;
        }
    }


    public void FirstOne(string carsName) //gets called by CarCollisionManager every frame
    {
        //get car with carsName from List for(int i=0; i<list.Count; i++){
        for (int i=0; i<activeCars.carsList.Count; i++)
        {
           if (activeCars.carsList[i] != null &&  activeCars.carsList[i].name == carsName)
           {
               firstCarLastFrame = firstCar;
               firstCar = activeCars.carsList[i];
                //Debug.Log("firstOne got called for " + firstCar.name);
                //Debug.Log("previous is " + firstCarLastFrame.name);
                isOvertaken = OnOvertake(); //Warning! works only flawlessly if FirstOne() gets called every frame!
            }
        }
        
        //Debug.Log("first car of this class is " + firstCar.name);
    }

 

    public GameObject getFirstPlaced()
    {
        return this.firstCar;
    }

    public GameObject getPreviousFirstPlaced()
    {
        return this.previousFirst;
    }
}
