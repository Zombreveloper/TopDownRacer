using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
        //SceneManager.LoadScene("MyScene");
        playerCount();
    }

    public void selectArena()
    {
        Debug.Log("Go to Arena-Scene");
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
