using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WayPointDisplayerScript : MonoBehaviour
{
    PlayerProfile myPlayer;
    GameObject myCar;
    int myWaypoints;
    public TMP_Text healthDisplay;
    Canvas myCanvas;
    Camera mainCamera;
    Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        StartDisplaying();
        StartPositioning();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdatePositioning();
        UpdateDisplaying();
    }

//main Start Methods
    void StartPositioning()
    {
        SetRightPosition();
        cameraTransform = mainCamera.GetComponent<Transform>();
    }

    void StartDisplaying()
    {
        //myPlayer = GetComponentInParent<LassesTestInputHandler>().myDriver;
        myPlayer = myCar.GetComponent<LassesTestInputHandler>().myDriver;
        SetEventCamera();
    }

//main Update Methods
    void UpdatePositioning()
    {
        myCanvas.transform.LookAt(transform.position + cameraTransform.forward); //healthbar always looks at camera

        //myCarTransform nach oben...
        SetRightPosition();
    }

    void UpdateDisplaying()
    {
        myWaypoints = myPlayer.wayPointCounter;
        healthDisplay.text = myWaypoints.ToString();
    }

//Methods
    public void ThisIsYourCar(GameObject someCar) //set by ParticipantsManager
    {
        myCar = someCar;
    }

    void SetRightPosition()
    {
        if (myCar == null)
        {
            selfDestroy();
        }
        else
        {
            Transform myTransform = GetComponent<Transform>();
            myTransform.position = myCar.transform.position + new Vector3(0, 3, 0);
        }
    }

    void SetEventCamera()
    {
        myCanvas = GetComponentInChildren<Canvas>(); //get attached canvas
        mainCamera = FindObjectOfType<Camera>(); //get only camera in scene
        myCanvas.worldCamera = mainCamera; // define event camera of canvas
    }

    void selfDestroy()
    {
        Destroy(gameObject);
    }
}
