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

    float minPitch = .5f;
    float maxPitch = 3f;
    void playEngineSound()
    {
        float carSpeed = carController.getRelativeCarVelocity();
        float RPM = carSpeed / carController.currentGear();
        float effectiveRPM = Mathf.Lerp(minPitch, maxPitch, RPM);
        //Debug.Log(carSpeed);
        audioManager.setPitch(motorSound, effectiveRPM);
        //audioManager.Play(soundName);
    }
}
