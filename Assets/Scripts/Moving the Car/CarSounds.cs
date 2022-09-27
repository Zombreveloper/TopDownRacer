using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    private AudioManager audioManager;
    public string[] crashSounds;
    public string motorSound;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

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
}
