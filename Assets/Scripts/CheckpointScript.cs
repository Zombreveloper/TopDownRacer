using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    int carCounter = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            carCounter++;
        }
    }

    public bool AmIFirst() //for CarCollisionManager
    {
        if (carCounter == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
