using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToPylonScript : MonoBehaviour
{
    Vector3 camCenterPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        camCenterPos = Camera.main.transform.position;
    }

    public void UpdateArrow(GameObject currentWayPoint)
    {
        UpdateRotation(currentWayPoint);
        UpdatePosition();
    }

    void UpdateRotation(GameObject currentWayPoint)
    {
        /*
        //mk1

        //get positions
        Vector2 pylonPos = currentWayPoint.transform.position;
        //Vector2 camCenterPos = Camera.main.transform.position;

        //calculate rotation
        float angle = Vector2.Angle(camCenterPos, pylonPos);
        Vector3 arrowRot = new Vector3(0.0f, 0.0f, angle); //the angle is completely wrong

        //set rotation
        Quaternion arrowQ = Quaternion.Euler(arrowRot);
        transform.rotation = arrowQ;
        */


        //mk2
        /*
        Inspired by @abar s answer here;
        https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html

        basically a "LookAt 2d" function
        */


        //get positions
        Vector3 pylonPos = currentWayPoint.transform.position;
        //calculate rotation
        Vector3 arrowRot = pylonPos - camCenterPos;
        //set rotation
        transform.up = arrowRot;
    }

    void UpdatePosition()
    {
        transform.position = camCenterPos;
    }
}
