using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkSystemScript : MonoBehaviour
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

    public void MakeSparks(Vector2 pos)
    {
        sparkEmitterParent.SetActive(true);
        sparkEmitter.transform.position = pos;

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
