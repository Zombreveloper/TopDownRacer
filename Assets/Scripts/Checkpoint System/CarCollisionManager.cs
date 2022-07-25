using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
detects every collision it does
necessairy for
- own behavior on street
- PlacementMagager (inspired by CodeMonkey https://youtu.be/IOYNg6v9sfc)
*/

public class CarCollisionManager : MonoBehaviour
{
    //referenced Classes
    PlacementManager placementManager;
    TopDownCarController carController;

    //variables
    string myName;
    private float oilConstant;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        myName = this.transform.root.gameObject.name;
        placementManager = GameObject.Find("/PlacementManager").GetComponent<PlacementManager>();
        carController = GetComponent<TopDownCarController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
                    //placementManager.FirstOne(myName);
                Debug.Log(myName + " is now first place");
                //other.enabled = false;
            }
        }
        /*else if(other.tag == "carussel")
        {
            //make oil stain the cars parent
            transform.parent = other.transform;
        }*/
        else if(other.tag == "oil stain")
        {
            oilConstant = Random.Range(-10.0f, 10.0f);
            if (oilConstant < 5 && oilConstant >= 0)
            {
                oilConstant += 5;
                //Debug.Log("oil Constant: " + oilConstant);
            }
            else if (oilConstant <= 0 && oilConstant > -5)
            {
                oilConstant -= 5;
                //Debug.Log("oil Constant: " + oilConstant);
            }

            //carController.adjustRotationAngle(150.0f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "oil stain")
        {
            carController.adjustRotationAngle(oilConstant);
            //carController.adjustRotationAngle(150.0f);
        }
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
}
