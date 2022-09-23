using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectSound();
        }
        else if (Input.GetKeyDown(KeyCode.Return)) //specific for Top Down Racing Games PlayerCounterMenu
        {
            submitSound();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            backSound();
        }
    }

    //Sound related
    public void selectSound()
    {
        audioManager.Play("Select");
    }

    public void backSound()
    {
        audioManager.Play("Back");
    }

    public void submitSound()
    {
        //Debug.Log("submitSound");
        //FindObjectOfType<AudioManager>().Play("Submit");
        audioManager.Play("Submit");
    }
}
