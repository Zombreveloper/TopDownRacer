using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundScript : MonoBehaviour
{
    private AudioManager audioManager;
    public string soundName;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(soundName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
