using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System; Remnant of the Event System

/*Attach to any GameObject that lets the Player Car Spin around
 */

public class SpinObstacleScript : MonoBehaviour
{
    //public static event Action OnTouchCar; //Remnant of the Event System

    //variables for inspector
    [Header("set velocities to same value for fixed velocity")]
    [SerializeField] float spinVelocityMin = 7f;
    [SerializeField] float spinVelocityMax = 7f;
    [SerializeField] int amountOfFullSpins = 1;

    enum SpinType { fullSpinsOnly, randomStopAngle, closeToFullSpins}
    [SerializeField] SpinType spinType; //creates a dropdown menu 
        


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetMouseButtonDown(1))
        {
                //OnTestEvent?.Invoke(); //Remnant of the Event System
                //Debug.Log("Event invoked");
            
                
        }*/
    }

    float spinFactorByType() //wanted to try out switch cases. Works well but this is not really a fitting use scenario
    {
        float calculatedSpinFactor = amountOfFullSpins;
        float randomModifier = 1f;
        switch(spinType)
        {
            case SpinType.fullSpinsOnly:
                //do full spins
                //
                break;

            case SpinType.randomStopAngle:
                //do random spins
                randomModifier = Random.Range(-0.5f, 0.5f);
                calculatedSpinFactor += randomModifier;
                break;

            case SpinType.closeToFullSpins:
                //do something a bit less random
                randomModifier = Random.Range(-0.2f, 0.2f);
                calculatedSpinFactor += randomModifier;
                break;

                default: //throw warning and do full spins instead
                Debug.LogError("There is no Spintype set for " + this.gameObject.name);
                break;
        }
        return calculatedSpinFactor;
    }

    float determineSpinVelocity()
    {
        float appliedVelocity = 0;
        if (spinVelocityMin != spinVelocityMax)
        {
            appliedVelocity = Random.Range(spinVelocityMin, spinVelocityMax);
        }
        else
        {
            appliedVelocity = spinVelocityMin;
        }

        //50% Chance to spin in the other direction
        int spinOtherDirection = Random.Range(0, 2); //upper value is exclusive!
        if (spinOtherDirection == 1)
            appliedVelocity = -appliedVelocity;

        return appliedVelocity;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //OnTestEvent?.Invoke(); //Remnant of the Event System
        //Debug.Log("Works even if the other is no trigger but I am");

        if (other.transform.parent.tag == "Car")
        {
            float finalSpins = spinFactorByType();
            float spinVelocity = determineSpinVelocity();
            other.transform.parent.gameObject.GetComponent<CarCollisionManager>().SpinForceBehavior(finalSpins, spinVelocity);
        }
    }

}
