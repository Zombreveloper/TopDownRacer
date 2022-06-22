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

    //variables
    string myName;

    void Awake()
    {
        myName = this.transform.root.gameObject.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        //placementManager = GetComponentInParent<PlacementManager>();
        //placementManager = FindObjectOfType<PlacementManager>();
        placementManager = GameObject.Find("/PlacementManager").GetComponent<PlacementManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I´ve hit a trigger");
        if (other.tag == "Checkpoint")
        {
            Debug.Log("This trigger is a checkpoint");
            CheckpointScript checkpoint = other.GetComponent<CheckpointScript>();
            checkpoint.CallMapBuilder();
            
            
            
            //check if im the first to collide...
            if (checkpoint.AmIFirst()) //AmIFirst() gibt true aus, wenn dies die erste Collision ist
            {
                if (placementManager != null)
                    placementManager.FirstOne(myName);
                other.enabled = false;
            }
        }
    }
}
