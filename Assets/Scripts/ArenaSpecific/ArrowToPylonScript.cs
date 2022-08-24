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
        //transform.position = camCenterPos;

        //arrow near screen edge
        //vector-position = between camCenterPos and pylonPos, but x and y are never greater than camera.pixelHeigt or pixelWidth /2

        Vector3 v1 = Intersection(camCenterPos, pylonPos, topLeftCamCorner, topRightCamCorner);
        Vector3 v2 = Intersection(camCenterPos, pylonPos, topRightCamCorner, botRightCamCorner);
        Vector3 v3 = Intersection(camCenterPos, pylonPos, botRightCamCorner, botLeftCamCorner);
        Vector3 v4 = Intersection(camCenterPos, pylonPos, botLeftCamCorner, topLeftCamCorner);

        List<Vector3> possibleVectors = new List<Vector3>();
        possibleVectors.Add(v1);
        possibleVectors.Add(v2);
        possibleVectors.Add(v3);
        possibleVectors.Add(v4);

        foreach ( Vector3 vector in possibleVectors)
        {
            if (vector.x > 0.005f || vector.x < -0.005f && vector.y > 0.005f || vector.y < -0.005f)
            {
                pylonDir = vector;
            }
        }

        transform.position = Vector3.Lerp(camCenterPos, pylonDir, 0.8f);
    }

    Vector3 Intersection(Vector3 camCenter, Vector3 pylon, Vector3 camCorner1, Vector3 camCorner2)
    {
        //Wikipedia Article with equation
        //Line - Line Intersection Given Two Points On Each Line
        //https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection

        //Line 1
        float x1 = camCenter.x;
        float y1 = camCenter.y;
        float x2 = pylon.x;
        float y2 = pylon.y;
        //Line 2
        float x3 = camCorner1.x;
        float y3 = camCorner1.y;
        float x4 = camCorner2.x;
        float y4 = camCorner2.y;

        float pointX = (((x1*y2 - y1*x2) * (x3 - x4) - (x1 - x2) * (x3*y4 - y3*x4))/((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4)));
        float pointY = (((x1*y2 - y1*x2) * (y3 - y4) - (y1 - y2) * (x3*y4 - y3*x4))/((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4)));

        Vector3 IntersectionResult = new Vector3(pointX, pointY, 0.0f);

        return IntersectionResult;
    }
}
