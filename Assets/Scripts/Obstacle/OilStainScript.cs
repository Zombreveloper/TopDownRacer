using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System; Remnant of the Event System

/*Attach to any GameObject that lets the Player Car Spin around
 */

public class OilStainScript : MonoBehaviour
{
    //public static event Action OnTouchCar; //Remnant of the Event System

    //variables
    [Header("Toggle to enable semirandom Oilstain Spinning rotation")]
    [SerializeField] bool allowUnevenSpins = false;
    [SerializeField] int amountOfFullSpins = 1;


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


    private void OnTriggerEnter2D(Collider2D other)
    {
        //OnTestEvent?.Invoke(); //Remnant of the Event System
        //Debug.Log("Works even if the other is no trigger but I am");

        if (other.transform.parent.tag == "Car")
        {
            other.transform.parent.gameObject.GetComponent<CarCollisionManager>().OilStainBehavior(amountOfFullSpins, allowUnevenSpins);
        }
    }

}
