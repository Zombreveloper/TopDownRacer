using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundScript : MonoBehaviour
{
    private AudioManager audioManager;
    public string soundName1;
    public string soundName2;
    private bool state;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        state = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSound()
    {
        Debug.Log("EXPLOSION PLAY SOUND");
        if (state)
        {
            audioManager.Play(soundName1);
            audioManager.Play("CrowdCheer");
            state = false;
        }
        else
        {
            audioManager.Play(soundName2);
            audioManager.Play("CrowdCheer");
            state = true;
        }
    }
}
