using UnityEngine;
using UnityEngine.Audio;

/*
Mostly copied from Brackeys:
https://youtu.be/6OT43pvUyfY
*/

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] //makes a Slider in the Inspector
    public float volume;
    [Range(1f, 3f)]
    public float pitch;

    public bool loop;

    public AudioMixerGroup outputAudioMixerGroup;

    [HideInInspector] //following var behaves like a puplic var, but is not visible in the inspector
    public AudioSource source;
}
