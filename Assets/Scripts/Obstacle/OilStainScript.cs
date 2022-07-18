using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilStainScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float torque = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //hjkhadkjfhksuhguksd
    }

    void OnTriggerStay2D(Collider2D collider2d)
    {
        //Debug.Log(collider2d.name);
        //Add rotational Force
        rb = collider2d.GetComponentInParent<Rigidbody2D>();
        //Debug.Log(rb.name);

        //funktioniert nicht, aber addforce schon but fckn y?!?!?!
        rb.AddTorque(torque, ForceMode2D.Force);
        //rb.AddTorque(torque, ForceMode2D.Force);

        //funktioniert
        //rb.AddForce(transform.up * torque, ForceMode2D.Impulse);
    }
}
