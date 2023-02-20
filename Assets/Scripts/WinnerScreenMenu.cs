using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerScreenMenu : MonoBehaviour
{
    public Button winnerBack;
    public Button restart;
    public GameMode_SO gameMode;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("WinJingle");
        audioManager.Play("CrowdCheer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            winnerBack.onClick.Invoke();
        }
    }

    public void restartGame() //Brings you back to PlayerCounterMenu
    {
        SceneManager.LoadScene("Player Count Test");
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Menu Test");
    }
}
