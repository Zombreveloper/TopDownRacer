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

    int index;

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

        index = 0;

        LoadSettings(); //pulls from PlayerPrefs, pushes into Mixer and Sliders
    }

    // Update is called once per frame
    void Update()
    {
        //code
    }

    public void SetMasterVolume()
    {
        var sliderValue = masterSlider.value;
        //myAudioMxer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        SetSettings("MasterVolume", sliderValue);
    }

    public void SetMusicVolume()
    {
        var sliderValue = musicSlider.value;
        //myAudioMxer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        SetSettings("MusicVolume", sliderValue);
    }

    public void SetEffectsVolume()
    {
        var sliderValue = effectsSlider.value;
        //myAudioMxer.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
        SetSettings("EffectsVolume", sliderValue);
    }

    public void SetMenuVolume()
    {
        var sliderValue = buttonSlider.value;
        //myAudioMxer.SetFloat("MenuVolume", Mathf.Log10(sliderValue) * 20);
        SetSettings("MenuVolume", sliderValue);
    }

    private void LoadSettings() //pulls from PlayerPrefs, pushes into Mixer and Sliders
    {
        //PlayerPrefs.DeleteAll(); //only uncomment this for testing purposes!!!!1!!!!11!!!

        PullFromPlayerPrefs();
        //PushIntoSliders(); //gest called by PullFromPlayerPrefs()
        //PushIntoMixer(); //gest called by PullFromPlayerPrefs()
    }

    private void PullFromPlayerPrefs()
    {
        foreach (string name in mySaveNames)
        {
            float currentValue;

            if (PlayerPrefs.HasKey(name)) //if something was saved previously
            {
                //Debug.Log("key " + slider.name + " exists");
                currentValue = PlayerPrefs.GetFloat(name);
            }
            else // if this is the first time you open the game
            {
                currentValue = 1f;
            }

            PushIntoSliders(index , currentValue); //display slider with right value

            //PushIntoMixer() -> SetSettings
            SetSettings(name, currentValue);
        }
    }
    private void PushIntoSliders(int _sliderIndex, float _value)
    {
        if (volumeSlider[_sliderIndex] != null)
        {
            Slider slider = volumeSlider[_sliderIndex]; //get right slider from Slider List (volumeSlider)
            index++;
            slider.value = _value;
        }
    }
    /*PushIntoMixer() ->directly use SetSettings()
    {
        SetSettings(name, value)
    }*/

    public void SetSettings(string _name, float _value) //changes Mixer values
    {
        myAudioMxer.SetFloat(_name, Mathf.Log10(_value) * 20);

        SaveSettings(_name, _value);
    }

    private void SaveSettings(string name, float value) //push into PlayerPrefs
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
