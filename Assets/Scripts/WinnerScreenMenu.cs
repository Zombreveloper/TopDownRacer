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

    // Start is called before the first frame update
    void Start()
    {

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
}
