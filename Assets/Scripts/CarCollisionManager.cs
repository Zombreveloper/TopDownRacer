using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
detects every collision it does
necessairy for
- own behavior on street
- PlacementMagager (ispired by CodeMonkey https://youtu.be/IOYNg6v9sfc)
*/

public class CarCollisionManager : MonoBehaviour
{
    PlacementManager placementManager;
    string myName;

    void Awake()
    {
        myName = this.transform.root.gameObject.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        placementManager = GetComponent<PlacementManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            CheckpointScript checkpoint = other.GetComponent<CheckpointScript>();
            //check if im the first to collide...
            if (checkpoint.AmIFirst()) //AmIFirst() gibt true aus, wenn dies die erste Collision ist
            {
                placementManager.FirstOne(myName);
            }
        }
    }
}
