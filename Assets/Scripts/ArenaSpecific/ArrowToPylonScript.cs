using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToPylonScript : MonoBehaviour
{
    Vector3 camCenterPos;
    Vector3 pylonPos;

    Vector3 pylonDir;

    public GameObject ArrowOnCanvas;
    public GameObject TargetInWorld;

    // Start is called before the first frame update
    void Start()
    {
        ArrowOnCanvas.SetActive(false);
        TargetInWorld.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        camCenterPos = Camera.main.transform.position;
    }

    public void UpdateArrow(GameObject currentWayPoint)
    {
        pylonPos = currentWayPoint.transform.position;
        UpdatePosition();
    }

    void UpdateRotation()
    {
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

    }

    void UpdatePosition()
    {
        //arrow in screen center
        //transform.position = camCenterPos;

        /*
        Inspired by / copied from Code Monkey, link to the Video here;
        https://youtu.be/dHzeHh-3bp4
        */
        //float borderSize = 100f;
        //float borderSize = ((-(Camera.main.orthographicSize))/2) + 75;
        //Debug.Log("current resolution " + Screen.height);
        float borderSize = Screen.height/100*12; //make 100 units from the sreen.height, and then put the arrow x units away from screen-border.


        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(pylonPos);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen)
        {
            UpdateRotation();
            ArrowOnCanvas.SetActive(true);
            TargetInWorld.SetActive(false);

            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;

            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            transform.position = pointerWorldPosition;
            TargetInWorld.transform.position = pointerWorldPosition;
        }
        else
        {
            transform.up = Vector3.zero;
            TargetInWorld.SetActive(true);
            ArrowOnCanvas.SetActive(false);

            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(targetPositionScreenPoint);
            transform.position = pointerWorldPosition;
            TargetInWorld.transform.position = pointerWorldPosition;
        }
    }
}
