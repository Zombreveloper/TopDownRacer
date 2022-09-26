using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMxer;

    private Slider slider;
    private float sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        LoadSavedSettings();
    }

    // Update is called once per frame
    void Update()
    {
        //code
    }

    public void SetMasterVolume()
    {
        sliderValue = slider.value;
        myAudioMxer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume()
    {
        sliderValue = slider.value;
        myAudioMxer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetEffectsVolume()
    {
        sliderValue = slider.value;
        myAudioMxer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMenuVolume()
    {
        sliderValue = slider.value;
        myAudioMxer.SetFloat("MenuVolume", Mathf.Log10(sliderValue) * 20);
    }

    void LoadSavedSettings()
    {
        //PlayerPrefs.DeleteAll(); //only unComment for testing Purpose!!!1!!!!11!!!!
        //Debug.Log("load saved settings for sound");

        //load smth from playerPrefs;
        slider.value = PlayerPrefs.GetFloat(slider.name);

        //Debug.Log("OnLoad: Name: " + slider.name + " Value: " + slider.value);

        if (PlayerPrefs.HasKey(slider.name))
        {
            //Debug.Log("key " + slider.name + " exists");
        }
        else
        {
            slider.value = 1f;
            //Debug.Log("key " + slider.name + " does not exist");
        }

        //Debug.Log("OnLoad after if: Name: " + slider.name + " Value: " + slider.value);
    }

    void OnDisable() //when quitting or esiting scene, save stuff
    {
        //Debug.Log("OnDisable: Name: " + slider.name + " Value: " + slider.value);
        PlayerPrefs.SetFloat(slider.name, slider.value);
    }
}
