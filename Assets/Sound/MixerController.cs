using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMxer;

    //every slider this specific MixerController needs, because Mixer hast that much buses...
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;
    public Slider buttonSlider;

    private List<Slider> volumeSlider = new List<Slider>();

    private float sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.Add(masterSlider);
        volumeSlider.Add(musicSlider);
        volumeSlider.Add(effectsSlider);
        volumeSlider.Add(buttonSlider);

        LoadSavedSettings();
    }

    // Update is called once per frame
    void Update()
    {
        //code
    }

    public void SetMasterVolume()
    {
        var sliderValue = masterSlider.value;
        myAudioMxer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume()
    {
        var sliderValue = musicSlider.value;
        myAudioMxer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetEffectsVolume()
    {
        var sliderValue = effectsSlider.value;
        myAudioMxer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMenuVolume()
    {
        var sliderValue = buttonSlider.value;
        myAudioMxer.SetFloat("MenuVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void LoadSavedSettings()
    {
        //PlayerPrefs.DeleteAll(); //only uncomment this for testing purposes!!!!1!!!!11!!!

        //load smth from playerPrefs;
        foreach (Slider slider in volumeSlider)
        {
            if (slider != null)
            {
                slider.value = PlayerPrefs.GetFloat(slider.name);

                if (PlayerPrefs.HasKey(slider.name))
                {
                    //Debug.Log("key " + slider.name + " exists");
                }
                else
                {
                    slider.value = 1f;
                }
            }
            else
            {
                return;
            }
        }
    }

    void OnDisable() //when quitting or esiting scene, save stuff
    {
        //Debug.Log("OnDisable: Name: " + slider.name + " Value: " + slider.value);
        foreach (Slider slider in volumeSlider)
        {
            if (slider != null)
            {
                PlayerPrefs.SetFloat(slider.name, slider.value);
            }
            else
            {
                return;
            }
        }
    }
}
