using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSoundScript : MonoBehaviour
{
    private AudioManager audioManager;
    public string soundName;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioManager.Play(soundName);
    }
}
