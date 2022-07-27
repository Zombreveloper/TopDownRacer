using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameMode_SO gameMode;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void selectRace()
    {
        Debug.Log("Go to Race-Scene");
        gameMode.gameMode = "Race";
        //SceneManager.LoadScene("MyScene");
        playerCount();
    }

    public void selectArenaRace()
    {
        Debug.Log("Go to Arena-Scene");
        gameMode.gameMode = "ArenaRace";
        //SceneManager.LoadScene("MyScene");
        playerCount();
    }

    public void selectArena()
    {
        Debug.Log("Go to Arena-Scene");
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
        Debug.Log("Quit Game");
        //Application.Quit();
    }
}
