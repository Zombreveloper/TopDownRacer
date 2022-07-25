using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassesTestInputHandler : MonoBehaviour
{
    //Components
    TopDownCarController topDownCarController;
    public PlayerProfile myDriver;
    float direction;
    private float carTorque;
    bool gameStarted;

    //Awake is called when the script instance is being loaded
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        carTorque = 0f;
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        setInput();
        sendInput();
    }

    void setInput()
    {
        //Debug.Log(myDriver.leftInput);
        if(Input.GetKey(myDriver.rightInput) && Input.GetKey(myDriver.leftInput))
        {
            //backwards
            SetCarTorque(-1.0f);
        }
        else if(Input.GetKey(myDriver.leftInput))
        {
            SetCarTorque(1.0f);
            direction = -1.0f;
            //Debug.Log("left");
        }
        else if(Input.GetKey(myDriver.rightInput))
        {
            SetCarTorque(1.0f);
            direction = 1.0f;
            //Debug.Log("right");
        }
        else
        {
            SetCarTorque(1.0f);
            direction = 0.0f;
        }
        //Debug.Log(direction);
    }

    public void GameStartet(bool status)
    {
        gameStarted = status;
    }

    void SetCarTorque(float value)
    {
        if (gameStarted)
        {
            carTorque = value;
        }
    }


    void sendInput()
    {
        Vector2 inputVector = Vector2.zero;

        //inputVector.x = Input.GetAxis("Horizontal");
        //inputVector.y = Input.GetAxis("Vertical");

        inputVector.x = direction;
        inputVector.y = carTorque;
        //Debug.Log("Vector X ist " + inputVector.x);
        //Debug.Log("Variable direction ist " + direction);

        topDownCarController.SetInputVector(inputVector);
    }
}
