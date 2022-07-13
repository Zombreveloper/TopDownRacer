using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    //referenced classes
    private MapBuilder mapBuilder;

    int carCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        mapBuilder = FindObjectOfType<MapBuilder>();
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
            Debug.Log("Car Counter set to " + carCounter);
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

    public void CallMapBuilder()
    {
        mapBuilder.BuildRandomPiece();
    }
}
