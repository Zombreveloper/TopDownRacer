using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToPylonScript : MonoBehaviour
{
    Vector2 camCenterPos;

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
        //get positions
        Vector2 pylonPos = currentWayPoint.transform.position;
        //Vector2 camCenterPos = Camera.main.transform.position;
        //calculate rotation
        Vector3 arrowRot = new Vector3(0.0f, 0.0f, Vector2.Angle(pylonPos, camCenterPos)); //the angle is completely wrong
        //set rotation
        Quaternion arrowQ = Quaternion.Euler(arrowRot);
        transform.rotation = arrowQ;
    }

    void UpdatePosition()
    {
        transform.position = camCenterPos;
    }
}
