using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TilePoolList_SO tilePools;
    public GameMode_SO gameMode;
    public Selectable firstButton, startPlayButton, leavePlayButton, startOptionsButton, leaveOptionsButton, startHTPButton, leaveHTPButton, startAreaSelectButton, leaveAreaSelectButton;
    public GameObject mainMenu, optionsMenu, hTPMenu, playMenu, areaSelectMenu;
    public Button mainQuit, optionsBack, hTPBack, playBack, areaSelectBack;
    public Toggle fullScreenToggle;

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
            else if (areaSelectMenu.activeSelf)
            {
                areaSelectBack.onClick.Invoke();
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
        diplayToggleFullscreen();
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

    public void selectStartAreaSelectButton()
    {
        startAreaSelectButton.Select();
    }

    public void selectLeaveAreaSelectButton()
    {
        leaveAreaSelectButton.Select();
    }

    //
    //

    public void setLevelMeadows() //not working anymore,level now determined by int
    {
        tilePools.levelName = "Meadows";
        playerCount();
    }

    public void setLevelDesert() //not working anymore,level now determined by int
    {
        tilePools.levelName = "Desert";
        playerCount();
    }


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

    private void diplayToggleFullscreen()
    {
        //get info from Screen and display on Toggle
        if (Screen.fullScreen == true)
        {
            fullScreenToggle.isOn = true;
        }
        else
        {
            fullScreenToggle.isOn = false;
        }
    }

    public void fullscreen(bool isFullscreen) //implemented via "on Value Changed" and "Dynamic Bool"
    {
        Screen.fullScreen = isFullscreen;
    }
}
