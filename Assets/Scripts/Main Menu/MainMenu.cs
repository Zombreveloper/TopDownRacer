using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameMode_SO gameMode;
    public Button firstButton, startPlayButton, leavePlayButton, startOptionsButton, leaveOptionsButton, startHTPButton, leaveHTPButton;
    public GameObject mainMenu, optionsMenu, hTPMenu, playMenu;
    public Button mainQuit, optionsBack, hTPBack, playBack;

    // Start is called before the first frame update
    void Start()
    {
        firstButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //do sth.
            //referenceToTheButton.onClick.Invoke();
            if (mainMenu.activeSelf)
            {
                mainQuit.onClick.Invoke();
            }
            else if (optionsMenu.activeSelf)
            {
                optionsBack.onClick.Invoke();
            }
            else if (hTPMenu.activeSelf)
            {
                hTPBack.onClick.Invoke();
            }
            else if (playMenu.activeSelf)
            {
                playBack.onClick.Invoke();
            }
        }
    }

    //
    //select right button

    public void selectStartPlayButton()
    {
        startPlayButton.Select();
    }

    public void selectLeavePlayButton()
    {
        leavePlayButton.Select();
    }

    public void selectStartOptionsButton()
    {
        startOptionsButton.Select();
    }

    public void selectLeaveOptionsButton()
    {
         leaveOptionsButton.Select();
    }

    public void selectStartHTPButton()
    {
        startHTPButton.Select();
    }

    public void selectLeaveHTPButton()
    {
        leaveHTPButton.Select();
    }

    //
    //

    public void selectRace()
    {
        //Debug.Log("Go to Race-Scene");
        gameMode.gameMode = "Race";
        //SceneManager.LoadScene("MyScene");
        playerCount();
    }

    public void selectArenaRace()
    {
        //Debug.Log("Go to Arena-Scene");
        gameMode.gameMode = "ArenaRace";
        //SceneManager.LoadScene("MyScene");
        playerCount();
    }

    public void selectArena()
    {
        //Debug.Log("Go to Arena-Scene");
        gameMode.gameMode = "Arena";
        //SceneManager.LoadScene("MyScene");
        playerCount();
    }

    void playerCount()
    {
        SceneManager.LoadScene("Player Count Test");
    }

    public void quitGame()
    {
        //Debug.Log("Quit Game");
        Application.Quit();
    }
}
