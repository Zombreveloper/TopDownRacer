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

    public GameObject ArrowTurntable; //has position and rotation like arrow, is in the middle of the screen and points at the pylon
    public GameObject ArrowBase; //changes its position.y component relative to the srcreen size

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
        //Debug.Log("trafo up: " + arrowRot);

        //
        //ArrowTurntable Rotation
        ArrowTurntable.transform.up = arrowRot;
    }

    void UpdatePosition()
    {
        //arrow in screen center
        transform.position = camCenterPos;
        ArrowTurntable.transform.position = camCenterPos;

        //
        //ArrowBase position
        float turntableRotAngel = ArrowTurntable.transform.rotation.z;
        //Debug.Log("turntableRotAngel: " + turntableRotAngel);
        //turntableRotAngel is between 180 and -180, up is 0/-0, down is 180/-180

        Vector3 arrowBasePos = ArrowBase.transform.position; //get whole position vector

        if (-45 <= turntableRotAngel && turntableRotAngel <= 0 || 0 <= turntableRotAngel && turntableRotAngel <= 45 ) //top
        {
            //relativeToScreenHeight(arrowBasePos);
            arrowBasePos.y = ((Camera.main.orthographicSize/2)-1); //change the y component
            ArrowBase.transform.position = arrowBasePos;
        }
        else if (135 <= turntableRotAngel && turntableRotAngel <= 180 || -180 <= turntableRotAngel && turntableRotAngel <= -135) //bot
        {
            //relativeToScreenHeight(arrowBasePos);
            arrowBasePos.y = -((Camera.main.orthographicSize/2)-1); //change the y component
            ArrowBase.transform.position = arrowBasePos;
        }
        else if (45 <= turntableRotAngel && turntableRotAngel <= 135 ) //left
        {
            //relativeToScreenWidth(arrowBasePos);
            float myAspect = Camera.main.aspect;
            arrowBasePos.y = (((Camera.main.orthographicSize * myAspect)/2)-1); //change the y component
            ArrowBase.transform.position = arrowBasePos;
        }
        else if ( -45 <= turntableRotAngel && turntableRotAngel <= -135) //right
        {
            //relativeToScreenWidth(arrowBasePos);
            float myAspect = Camera.main.aspect;
            arrowBasePos.y = (((Camera.main.orthographicSize * myAspect)/2)-1); //change the y component
            ArrowBase.transform.position = arrowBasePos;
        }
        else
        {
            Debug.Log("ERROR: _Arrow To Pylon Script_ has no correct angle _Line 98_");
        }
    }

    /*
    void relativeToScreenHeight(Vector3 arrowBasePos)
    {
        //float arrowBaseY = ((Screen.height/2)-1);
        //ArrowBase.transform.position.y = arrowBaseY;

        arrowBasePos.y = ((Camera.main.orthographicSize/2)-1); //change the y component
        ArrowBase.transform.position = arrowBasePos;
    }

    void relativeToScreenWidth(Vector3 arrowBasePos)
    {
        float myAspect = Camera.main.aspect;
        arrowBasePos.y = (((Camera.main.orthographicSize * myAspect)/2)-1); //change the y component
        ArrowBase.transform.position = arrowBasePos;
    }
    */
}
