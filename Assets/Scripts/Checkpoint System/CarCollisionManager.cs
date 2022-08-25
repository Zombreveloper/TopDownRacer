using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System; includes the EventSystem

/*
detects every collision it does
necessairy for
- own behavior on street
- PlacementMagager (inspired by CodeMonkey https://youtu.be/IOYNg6v9sfc)
*/

public class CarCollisionManager : MonoBehaviour
{
    //listeners
    //public static event Action OnOilstain;

    //referenced Classes
    PlacementManager placementManager;
    TopDownCarController carController;
    PlayerProfile myPlayer;

    //variables
    string myName;
    private float oilConstant;
    //bool isColliding; //used to prevent multiple Triggers in one Frame

    bool coroutineRunning = false;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        myName = this.transform.root.gameObject.name;
        placementManager = GameObject.Find("/PlacementManager").GetComponent<PlacementManager>();
        carController = GetComponent<TopDownCarController>();
        myPlayer = GetComponent<LassesTestInputHandler>().myDriver;

        //OilStainScript.OnTestEvent += doEventThing; //Remnant of the Event System
    }

    // Update is called once per frame
    void Update()
    {
        //isColliding = false; //used to prevent multiple Triggers in one Frame
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (isColliding) //used to prevent multiple Triggers in one Frame
        {
            return;
        }
        isColliding = true; //used to prevent multiple Triggers in one Frame
        */

        //Debug.Log("I´ve hit a trigger");
        if (other.tag == "Checkpoint")
        {
            Debug.Log("This trigger is a checkpoint");
            CheckpointScript checkpoint = other.GetComponent<CheckpointScript>();
            checkpoint.CallMapBuilder();



            //check if im the first to collide...
            if (checkpoint.AmIFirst()) //AmIFirst() gibt true aus, wenn dies die erste Collision ist
            {
                if (placementManager != null)
                {
                    //placementManager.FirstOne(myName);
                    //Debug.Log(myName + " is now first place");
                    //other.enabled = false;
                }

            }
        }
        /*else if(other.tag == "carussel")
        {
            //make oil stain the cars parent
            transform.parent = other.transform;
        }*/
        else if(other.tag == "bumper")
        {
            //Debug.Log("health");
            int myHealth = int.Parse(myPlayer.health);
            //Debug.Log("prev health " + myHealth);
            myHealth--;
            //Debug.Log("curr health " + myHealth);
            myPlayer.health = myHealth.ToString();
        }
        else if (other.tag == "wayPoint")
        {
            //count up on myPlayer.checkpointCounter
            myPlayer.wayPointCounter++;
            int numberOfWayPoints = myPlayer.wayPointCounter;

            if (numberOfWayPoints >= 10)
            {
                FindObjectOfType<ArenaRaceManagerScript>().Winner(myPlayer);
            }
        }
    }

    public void OilStainBehavior(int spinAmount, bool _unevenSpins)
    {
        if (coroutineRunning == false)
        {
            oilConstant = Random.Range(5.0f, 10.0f);
            int spinOtherDirection = Random.Range(0, 2); //upper value is exclusive!
            if (spinOtherDirection == 1) 
                oilConstant = -oilConstant;

            StartCoroutine(spinCar(spinAmount, _unevenSpins));
            //carController.adjustRotationAngle(150.0f);
        }
    }

    public void BoostPadBehavior(Vector2 _force, float duration)
    {
        //carController.addAdditionalVelocity(_force, duration);
        StartCoroutine(carController.additionalVelocityForSeconds(_force, duration));
    }

    private IEnumerator spinCar(float spins, bool _unevenSpins = false) //possibly better placed directly inside CarController
    {
        coroutineRunning = true;
        if(_unevenSpins)
        {
            float randomModifier = Random.Range(0.5f, 1.0f);
            spins *= randomModifier;
        }
        float maxSpinAngle = spins * 360;
        float alreadySpun = 0;
        //Quaternion alreadySpun;
        while (alreadySpun < maxSpinAngle && alreadySpun > -maxSpinAngle)
        {
            float slowSpin = oilConstant / 2;
            carController.adjustRotationAngle(slowSpin);
            alreadySpun += slowSpin;
            yield return null;
        }
        coroutineRunning = false;
        yield break;
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        /*if(other.tag == "oil stain")
        {
            carController.adjustRotationAngle(0.0f, false);
            //carController.adjustRotationAngle(150.0f);
        }*/
        /*if (other.tag == "carussel")
        {
            //let car be its own parent again
            transform.parent = null;
        }*/
    }

    public void callFirstOne()
    {
        placementManager.FirstOne(myName);
    }

    private void doEventThing()
    {
        transform.position = new Vector3(2, 2, 0);
        Debug.Log("This Event gets called");
    }
}
