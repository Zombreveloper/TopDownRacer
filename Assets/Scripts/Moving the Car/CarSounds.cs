using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    private AudioManager audioManager;
    private TopDownCarController carController;
    public string[] crashSounds;
    public string motorSound;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<TopDownCarController>();
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(motorSound);
    }

    // Update is called once per frame
    void Update()
    {
        playEngineSound();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ArenaMap" || collision.collider.tag == "underpass collider" || collision.collider.tag == "overpass collider" || collision.collider.transform.parent.tag == "Car")
        {
            CrashSound();
        }
    }

    void CrashSound()
    {
        var number = Random.Range(0,3);
        string soundName = crashSounds[number];
        audioManager.Play(soundName);
    }

    float minPitch = 1;
    float maxPitch = 3;
    void playEngineSound()
    {
        float carSpeed = carController.getRelativeCarVelocity();
        float effectiveRPM = Mathf.Lerp(minPitch, maxPitch, carSpeed);
        //Debug.Log(carSpeed);
        audioManager.setPitch(motorSound, carSpeed);
        //audioManager.Play(soundName);
    }
}
