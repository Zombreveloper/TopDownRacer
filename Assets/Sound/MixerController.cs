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

    private List<string> mySaveNames = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.Add(masterSlider);
        volumeSlider.Add(musicSlider);
        volumeSlider.Add(effectsSlider);
        volumeSlider.Add(buttonSlider);

        mySaveNames.Add("MasterVolume");
        mySaveNames.Add("MusicVolume");
        mySaveNames.Add("EffectsVolume");
        mySaveNames.Add("MenuVolume");

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
        saveOptions("MasterVolume", sliderValue);
    }

    public void SetMusicVolume()
    {
        var sliderValue = musicSlider.value;
        myAudioMxer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        saveOptions("MusicVolume", sliderValue);
    }

    public void SetEffectsVolume()
    {
        var sliderValue = effectsSlider.value;
        myAudioMxer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
        saveOptions("EffectsVolume", sliderValue);
    }

    public void SetMenuVolume()
    {
        var sliderValue = buttonSlider.value;
        myAudioMxer.SetFloat("MenuVolume", Mathf.Log10(sliderValue) * 20);
        saveOptions("MenuVolume", sliderValue);
    }

    public void LoadSavedSettings()
    {
        //PlayerPrefs.DeleteAll(); //only uncomment this for testing purposes!!!!1!!!!11!!!

        //load smth from playerPrefs;
        foreach (string name in mySaveNames)
        {
            float value = PlayerPrefs.GetFloat(name);

            if (PlayerPrefs.HasKey(name)) //if something was saved previously
            {
                //Debug.Log("key " + slider.name + " exists");
                setSettings(name, value);
            }
            else // if this is the first time you open the game
            {
                float firstValue = 1f;
                setSettings(name, firstValue);
            }
        }
    }

    void setSettings(string _name, float _value)
    {
        myAudioMxer.SetFloat(_name, Mathf.Log10(_value) * 20);
    }

    /*void OnDisable() //when quitting or exiting scene, save stuff
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
    }*/

    public void saveOptions(string name, float value) //call this whn you want to save your changes
    {
        //Debug.Log("OnDisable: Name: " + slider.name + " Value: " + slider.value);
        /*foreach (Slider slider in volumeSlider)
        {
            if (slider != null)
            {
                PlayerPrefs.SetFloat(slider.name, slider.value);
            }
            else
            {
                return;
            }
        }*/
        PlayerPrefs.SetFloat(name, value);
    }
}
