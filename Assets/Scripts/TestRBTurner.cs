using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRBTurner : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform myT;
    private float torque;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        torque = 100.0f;
        //rb.AddTorque(torque);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //torque += torque;
        //rb.AddTorque(torque);

        //rb.angularVelocity += torque*Time.deltaTime;

        //rb.rotation += torque*Time.deltaTime;
        myT.Rotate(0.0f ,0.0f ,torque);
    }
}
