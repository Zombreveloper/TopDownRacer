using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Attach to any GameObject that gives the Player Car forward Boost relative to the Cars rotation
*/

public class BoostPadNonDirScript : MonoBehaviour
{
    //variables
    [SerializeField] float boostDurationSeconds = 1f;
    [SerializeField] float boostStrength = 1f;
    Vector2 addForce;

    // Start is called before the first frame update
    void Start()
    {
        addForce = new Vector2(boostStrength, boostStrength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //OnTestEvent?.Invoke(); //Remnant of the Event System
        //Debug.Log("Works even if the other is no trigger but I am");

        if (other.transform.parent.tag == "Car")
        {
            other.transform.parent.gameObject.GetComponent<CarCollisionManager>().BoostPadBehavior(addForce, boostDurationSeconds);
        }
    }
}
