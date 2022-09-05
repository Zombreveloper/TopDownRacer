using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Lasses Arrow to Pylon Script but reused to always let the Explosion rotate towards the center of the screen
 */
public class ExplosionScript : MonoBehaviour
{
    Vector3 camCenterPos;
    Vector3 pylonPos;
    Vector3 pointOfImpactScreenSpace;

    // Start is called before the first frame update
    void Start()
    {
        pointOfImpactScreenSpace = this.gameObject.transform.position - Camera.main.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        camCenterPos = Camera.main.transform.position;
        UpdateRotation();
        simpleUpdatePosition(); //currently handled by OutOfScreenDestroyEffects
    }

    void UpdateRotation()
    {
        //mk2
        /*
        Inspired by @abar s answer here;
        https://answers.unity.com/questions/585035/lookat-2d-equivalent-.html

        basically a "LookAt 2d" function
        */

        //calculate rotation towards CameraCenterPoint
        Vector3 spriteRot = camCenterPos - this.gameObject.transform.position;
        //set rotation
        transform.up = spriteRot;
        //Debug.Log("trafo up: " + arrowRot);

    }

    void simpleUpdatePosition()
    {   
        Vector3 worldPosition = pointOfImpactScreenSpace + camCenterPos;
        this.gameObject.transform.position = worldPosition;
    }
}
