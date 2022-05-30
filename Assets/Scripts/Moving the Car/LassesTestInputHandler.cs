using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassesTestInputHandler : MonoBehaviour
{
    //Components
    TopDownCarController topDownCarController;
    public PlayerProfile myDriver;
    float direction;

    //Awake is called when the script instance is being loaded
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setDirection();
        sendInput();
    }

    void setDirection()
    {
        //Debug.Log(myDriver.leftInput);
        if(Input.GetKey(myDriver.leftInput))
        {
            direction = -1.0f;
            //Debug.Log("left");
        }
        else if(Input.GetKey(myDriver.rightInput))
        {
            direction = 1.0f;
            //Debug.Log("right");
        }
        else
        {
            direction = 0.0f;
        }
        //Debug.Log(direction);
    }

    void sendInput()
    {
        Vector2 inputVector = Vector2.zero;

        //inputVector.x = Input.GetAxis("Horizontal");
        //inputVector.y = Input.GetAxis("Vertical");

        inputVector.x = direction;
        inputVector.y = 1f;
        //Debug.Log("Vector X ist " + inputVector.x);
        //Debug.Log("Variable direction ist " + direction);

        topDownCarController.SetInputVector(inputVector);
    }
}
