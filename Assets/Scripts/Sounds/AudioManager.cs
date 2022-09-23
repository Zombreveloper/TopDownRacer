using UnityEngine;
using System;
using UnityEngine.Audio;

/*
Copied from Brackeys:
https://youtu.be/6OT43pvUyfY
*/
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; //Array mit (rohen) Sounds aus dem Asset Ordner.

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            //Audio wird nur Abgespielt, wenn es eine AudioSource-Komponente gibt, die den entsprechenden Sound hat.
            s.source = gameObject.AddComponent<AudioSource>(); //erschafft pro Sound in Liste eine AudioSource-Komponente.
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        //Debug.Log("A Sound should be played");
        Sound s = Array.Find(sounds, sound => sound.name == name); //Find in the "sounds" Array, a sound with the name "name"
        s.source.Play();
        //Debug.Log("A Sound should be played");
    }
}
