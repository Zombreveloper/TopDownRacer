using UnityEngine;
using System;
using UnityEngine.Audio;

/*
Copied from Brackeys:
https://youtu.be/6OT43pvUyfY
*/
public class AudioManager : MonoBehaviour
{
    public AudioMixer myAudioMxer;
    public Sound[] sounds; //Array mit (rohen) Sounds aus dem Asset Ordner.

    /*public static AudioManager instance;*/ //static refeence to itself / the first one

    void Awake()
    {
        /*if (instance == null) //if there is no AudioManagers
        {
            instance = this;
        }
        else //if there are two AudioManagers
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); //is persistent between Scenes.*/

        foreach (Sound s in sounds)
        {
            //Audio wird nur Abgespielt, wenn es eine AudioSource-Komponente gibt, die den entsprechenden Sound hat.
            s.source = gameObject.AddComponent<AudioSource>(); //erschafft pro Sound in Liste eine AudioSource-Komponente.
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.outputAudioMixerGroup;
        }
    }

    public void Play(string name)
    {
        //Debug.Log("A Sound should be played");
        Sound s = Array.Find(sounds, sound => sound.name == name); //Find in the "sounds" Array, a sound with the name "name"
        if (s == null) //does not play a sound that is not there. Catches spelling Errors.
        {
            Debug.Log("ERROR in AudioManager.Play(): Sound " + name + " was not found!!!");
            return;
        }
        s.source.Play();
        //Debug.Log("A Sound should be played");
    }
}
