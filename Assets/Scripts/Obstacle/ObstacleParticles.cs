using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This Script can be attached to an Obstacle.
It also needs two Gameobjects in the following Hirachy:

-Empty
    -ParticleSystem

Empty is the sparkEmitterParent
ParticleSystem is the sparkEmitter
*/

public class ObstacleParticles : MonoBehaviour
{
    public ParticleSystem sparkEmitter;
    public GameObject  sparkEmitterParent;
    private float dur;

    // Start is called before the first frame update
    void Start()
    {
        dur = sparkEmitter.main.duration;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.parent.tag == "Car")
        {
            //Vector2 posOfImpact = collision.GetContact(0).point; //for specific position of the Impact
            //MakeSparks(posOfImpact); //for specific position of the Impact
            MakeSparks();
        }
    }*/

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.tag == "Car")
        {
            //Vector2 posOfImpact = collision.GetContact(0).point; //for specific position of the Impact
            //MakeSparks(posOfImpact); //for specific position of the Impact
            MakeSparks();
        }
    }

    //public void MakeSparks(Vector2 pos) //for specific position of the Impact
    public void MakeSparks()
    {
        sparkEmitterParent.SetActive(true);
        //sparkEmitter.transform.position = pos; //for specific position of the Impact

        var em = sparkEmitter.emission;
        em.enabled = true;

        sparkEmitter.Play(true);

        Invoke("StopSparks", dur);
    }

    private void StopSparks()
    {
        sparkEmitter.Play(false);
        var em = sparkEmitter.emission;
        em.enabled = false;

        sparkEmitterParent.SetActive(false);
    }
}
