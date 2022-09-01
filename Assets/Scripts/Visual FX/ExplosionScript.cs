using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Lasses Arrow to Pylon Script but reused to always let the Explosion rotate towards the center of the screen
 */
public class ExplosionScript : MonoBehaviour
{
    Vector3 camCenterPos;
    Vector3 pylonPos;
    Vector3 pointOfImpact;

    Vector3 pylonDir;

    SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //myRenderer = GetComponentInChildren<SpriteRenderer>();
        pointOfImpact = this.gameObject.transform.position - Camera.main.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        camCenterPos = Camera.main.transform.position;
        UpdateRotation();
        simpleUpdatePosition(); //currently handled by OutOfScreenDestroyEffects
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

        //calculate rotation towards CameraCenterPoint
        Vector3 spriteRot = camCenterPos - this.gameObject.transform.position;
        //set rotation
        transform.up = spriteRot;
        //Debug.Log("trafo up: " + arrowRot);

    }

    void simpleUpdatePosition()
    {
        this.gameObject.transform.position = pointOfImpact + camCenterPos;
    }

    void UpdatePosition()
    {
        /*
        Inspired by / copied from Code Monkey, link to the Video here;
        https://youtu.be/dHzeHh-3bp4
        */
        //float borderSize = 50f;
        float borderSize = ((-(Camera.main.orthographicSize)) / 2) + 75;


        Vector3 targetPositionScreenPoint = camCenterPos;
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (!isOffScreen)
        {
            //UpdateRotation();

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

            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(targetPositionScreenPoint);
            transform.position = pointerWorldPosition;
        }
    }
}
