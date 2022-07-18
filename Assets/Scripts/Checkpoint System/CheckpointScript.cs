using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Provides function for the checkpoints by telling a car if he is first
 * I don´t know why but even though the checkpoints should be instances of one another with seperate scripts
 * the CarCounter counts up for all of them. Counter++ is deactivated for now.
 * destroying a checkpoint after passing it the first time is appropriate
 */

public class CheckpointScript : MonoBehaviour
{
    //referenced classes
    private MapBuilder mapBuilder;
    private CheckpointPlacer cPlacer;

    //used variables
    int carCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        mapBuilder = FindObjectOfType<MapBuilder>();
        cPlacer = FindObjectOfType<CheckpointPlacer>();
        //cPlacer = GameObject.Find("MapManager").GetComponent<CheckpointPlacer>(); //revert to this when upper function makes problems
    }

    // Update is called once per frame
    void Update()
    {

    }

    //makes that every Chekpoint can only once be interacted with
    //CARCOUNTER NOT NECESSARY WHILE CHECKPOINTS IMMEDIATLY GET DESTROYED
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.tag == "Car")
        {
            //carCounter++;
            //Debug.Log("Car Counter set to " + carCounter);
            cPlacer.destroyCheckpoint();
            
        }
    }

    public bool AmIFirst() //for CarCollisionManager
    {
        if (carCounter == 1)
        {
            //Debug.Log("carCounter is 1");
            return true;
        }
        else
        {
            Debug.Log("CheckpointScript: CarCounter counted up to fast!");
            return false;
        }
    }

    public void CallMapBuilder() //may not work, doesn´t get used yet
    {
        mapBuilder.BuildRandomPiece();
    }
}
