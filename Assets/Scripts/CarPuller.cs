using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that is supposed to determine the direction and Speed in which the Player cars get Pulled by the Track.
// For now only with manually set numbers

public class CarPuller : MonoBehaviour
{
    [Header("Direction of pull")]
    public float pullX = 0f;
    public float pullY = -27f;

    //used Components
    TopDownCarController topDownCarController;

    //Awake is called when the script instance is being loaded
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();

    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        PullCarDown();
    }

    void PullCarDown()
    {
        Vector2 pullVector = new Vector2(pullX, pullY); //Vector that pulls the car into a set direction. Will be dependent on the direction of the track later

        topDownCarController.SetPullVector(pullVector);
    }


}
