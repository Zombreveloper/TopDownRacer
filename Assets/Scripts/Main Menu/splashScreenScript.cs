using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splashScreenScript : MonoBehaviour
{
    private AudioManager audioManager;
    public string soundName;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play(soundName);
        audioManager.Play("CrowdCheer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //specific for Top Down Racing Games PlayerCounterMenu
        {
            EnterMenu();
            audioManager.Play("Submit");
        }
    }

    void EnterMenu()
    {
        SceneManager.LoadScene("Menu Test");
    }
}
