using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToPylonScript : MonoBehaviour
{
    Vector3 camCenterPos;
    Vector3 pylonPos;
    Vector3 topLeftCamCorner;
    Vector3 topRightCamCorner;
    Vector3 botLeftCamCorner;
    Vector3 botRightCamCorner;

    Vector3 pylonDir;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        camCenterPos = Camera.main.transform.position;

        topLeftCamCorner = new Vector3(camCenterPos.x - (Camera.main.pixelWidth/2), camCenterPos.y + (Camera.main.pixelHeight/2), 0.0f);
        topRightCamCorner = new Vector3(camCenterPos.x + (Camera.main.pixelWidth/2), camCenterPos.y + (Camera.main.pixelHeight/2), 0.0f);
        botLeftCamCorner = new Vector3(camCenterPos.x - (Camera.main.pixelWidth/2), camCenterPos.y - (Camera.main.pixelHeight/2), 0.0f);
        botRightCamCorner= new Vector3(camCenterPos.x + (Camera.main.pixelWidth/2), camCenterPos.y - (Camera.main.pixelHeight/2), 0.0f);
    }

    public void UpdateArrow(GameObject currentWayPoint)
    {
        pylonPos = currentWayPoint.transform.position;
        UpdateRotation();
        UpdatePosition();
    }

    void UpdateRotation()
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

        //calculate rotation
        Vector3 arrowRot = pylonPos - camCenterPos;
        //set rotation
        transform.up = arrowRot;
    }

    void UpdatePosition()
    {
        //arrow in screen center
        transform.position = camCenterPos;
    }
}
