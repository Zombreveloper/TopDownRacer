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

    //variables
    public bool isOvertaken;

    // Start is called before the first frame update
    void Start()
    {
        activeCars = FindObjectOfType<ListOfActiveCars>();
    }

    // Update is called once per frame
    private void Update()
    {
        //StartCoroutine(checkOvertake());
       // if (firstCarPrevFrame != null)
            //Debug.Log("first Car was" + firstCarPrevFrame.name);
       // Debug.Log("first Car is currently" + firstCar.name);

        isOvertaken = OnOvertake(); //Achtung! works only when FirstOne() gets called every frame!
    }
    void LateUpdate()
    {
       

        
    }
    bool OnOvertake()
    {
        if (firstCar == previousFirstCar)
        {
            return false;
        }
        
        else
        {
            Debug.Log("Overtake!");
            return true;
        }
    }

    private IEnumerator checkOvertake()
    {
        OnOvertake();
        yield return new WaitForEndOfFrame();
        firstCarPrevFrame = firstCar;
        StartCoroutine(checkOvertake());
    }

    public void FirstOne(string carsName) //gets called by CarCollisionManager when NextCheckpointVector sets that car as first
    {
        //get car with carsName from List for(int i=0; i<list.Count; i++){
        for (int i=0; i<activeCars.carsList.Count; i++)
        {
           if (activeCars.carsList[i] != null &&  activeCars.carsList[i].name == carsName)
           {
               previousFirstCar = firstCar;
               firstCar = activeCars.carsList[i];
               //Debug.Log("firstOne got called for " + firstCar.name);
                //Debug.Log("previous is " + previousFirstCar.name);
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
        return this.previousFirstCar;
    }
}
