using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* based on code found here: https://github.com/Gkxd/Rhythmify/blob/master/Assets/Rhythmify_Scripts/MusicWrapper.cs
 * Additional explanaition in this post: https://answers.unity.com/questions/941748/intro-to-looping-music-in-single-audio-clip.html
 */

public class IntroToLoop : MonoBehaviour
{
    public int BPM;
    public float loopLength; //Length of the looped part of the song in seconds
    public float loopThreshold;//Time where the loop ends in seconds
    private AudioSource audioSource;
    private AudioClip audioClip;

    public void Start()
    {

        audioSource = gameObject.GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }

    public void Update()
    {
        if (loopLength > 0 && loopThreshold > 0)
        {
            if (audioSource.timeSamples > loopThreshold * audioClip.frequency)
            {
                audioSource.timeSamples -= Mathf.RoundToInt(loopLength * audioClip.frequency);
            }
        }
    }
}
