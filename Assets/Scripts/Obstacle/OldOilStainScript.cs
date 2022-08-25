using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldOilStainScript : MonoBehaviour
{
    private Rigidbody2D otherRb;
    private Rigidbody2D myRb;
    private Transform otherTransform;
    public float torque = 0.1f;
    private float myTorque;
    private TopDownCarController myCarController;

    // Start is called before the first frame update
    void Start()
    {
        //myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, torque * Time.deltaTime);
        //float drehung = 1.0f;
        //drehung += drehung;
        //myRb.AddTorque(torque, ForceMode2D.Force);
        //myRb.AddTorque(drehung, ForceMode2D.Force);
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        //myTorque = torque;
        //adjustTransform(collider2d);
    }

    void OnTriggerStay2D(Collider2D collider2d)
    {
        //adjustRigidbody(collider2d);
        //adjustSteering(collider2d);
        //adjustRigidbody3(collider2d);

        //otherRb = collider2d.GetComponentInParent<Rigidbody2D>();
        //otherRb.angularVelocity += 100.0f*Time.deltaTime;


        //adjustTransform(collider2d);
    }

    void OnTriggerExit2D(Collider2D collider2d)
    {
        //myTorque = torque; //reset myTorque to the value of torque
    }
/*
    void adjustRigidbody(Collider2D collider2d)
    {
        //Debug.Log(collider2d.name);
        //Add rotational Force
        otherRb = collider2d.GetComponentInParent<Rigidbody2D>();
        //Debug.Log(rb.name);

        myTorque += myTorque;

        otherRb.AddTorque(myTorque, ForceMode2D.Force);
        //rb.AddTorque(torque, ForceMode2D.Force);

        //funktioniert
        //rb.AddForce(transform.up * torque, ForceMode2D.Impulse);
    }

    void adjustSteering(Collider2D collider2d)
    {
        myCarController = collider2d.GetComponentInParent<TopDownCarController>();
        Debug.Log("adjustSteering");
        //does what the inputHandler does
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Random.Range(1.0f, -1.0f);
        inputVector.y = 1f;

        myCarController.SetInputVector(inputVector);
    }

    void adjustRigidbody2(Collider2D collider2d)
    {
        Debug.Log("adjustRigidbody2");
        otherRb = collider2d.GetComponentInParent<Rigidbody2D>();
        //rb.MoveRotation(Random.Range(360.0f, 0.0f));
        otherRb.MoveRotation(2000);
    }

    void adjustRigidbody3(Collider2D collider2d)
    {
        otherRb = collider2d.GetComponentInParent<Rigidbody2D>();
        otherRb.angularVelocity += 100.0f*Time.deltaTime;
    }
*/
    void adjustTransform(Collider2D collider2d)
    {
        otherTransform = collider2d.transform.root.GetComponent<Transform>();
        otherTransform.Rotate(0.0f ,0.0f ,torque);
        Debug.Log("i am colliding with " + otherTransform);
    }
}
