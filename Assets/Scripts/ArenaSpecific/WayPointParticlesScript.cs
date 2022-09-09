using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WayPointParticlesScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WayPointScript.OnCarGotWaypoint += placeParticles;

        dur = sparkEmitter.main.duration;//Thiscode ist copied from the Script "ObstacleParticles.c"
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void placeParticles(GameObject other)
    {
        transform.position = other.transform.position;

        MakeSparks();
    }

    //The following code ist copied from the Script "ObstacleParticles.c"
    public ParticleSystem sparkEmitter;
    //public GameObject  sparkEmitterParent;
    private float dur;

    //public void MakeSparks(Vector2 pos) //for specific position of the Impact
    public void MakeSparks()
    {
        //sparkEmitterParent.SetActive(true);
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

        //sparkEmitterParent.SetActive(false);
    }
}
