using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
This scipt has to be attached to a slider to work properly!

call ValueChanged() trough sliders "on ValueChanged"
*/

public class sliderSoundScript : MonoBehaviour
{
    Slider slider;
    AudioManager audioManager;
    public string moveLeftSound;
    public string moveRightSound;
    private bool diffrentValue;

    // Start is called before the first frame update
    void Start()
    {
        slider = FindObjectOfType<Slider>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && diffrentValue)
        {
            audioManager.Play(moveRightSound);
            diffrentValue = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && diffrentValue)
        {
            audioManager.Play(moveLeftSound);
            diffrentValue = false;
        }
    }

    public void ValueChanged() //call this trough sliders "on ValueChanged"
    {
        diffrentValue = true;
    }
}
