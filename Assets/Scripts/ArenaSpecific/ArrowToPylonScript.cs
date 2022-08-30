using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToPylonScript : MonoBehaviour
{
    Vector3 camCenterPos;
    Vector3 pylonPos;

    Vector3 pylonDir;

    public Sprite arrowSprite;
    public Sprite targetSprite;
    SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponentInChildren<SpriteRenderer>();
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
        //float borderSize = 50f;
        float borderSize = ((-(Camera.main.orthographicSize))/2) + 55;


        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(pylonPos);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen)
        {
            UpdateRotation();
            myRenderer.sprite = arrowSprite;

            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;

            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            transform.position = pointerWorldPosition;
        }
        else
        {
            transform.up = Vector3.zero;
            myRenderer.sprite = targetSprite;

            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(targetPositionScreenPoint);
            transform.position = pointerWorldPosition;
        }
    }
}
