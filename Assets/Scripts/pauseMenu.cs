using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{

	//public FmodVolumeSettings mySoundmanager;

    public static bool GameIsPaused = false;


    public GameObject pauseMenuUI;
    
    //variables to get CarSounds
    [SerializeField] public ListOfActiveCars activeCars;
    private List<GameObject> myCarsList;

    void Start()
    {
        activeCars = FindObjectOfType<ListOfActiveCars>();
        if (activeCars == null)
        {
            Debug.Log("PauseMenu Doesn't have CarsList!");
        }

        

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playSound();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseSound();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Resume();
        PlayerPrefs.SetString("fromPLanet", "Splash");
        PlayerPrefs.Save();
		//mySoundmanager.UnpauseSFX();
        SceneManager.LoadScene("Menu Test");
    }

	public void goToOptions()
	{
		pauseMenuUI.SetActive(false);

	}

	public void goToPauseMenu()
	{
		pauseMenuUI.SetActive(true);
	}

    public void QuitGame()
    {
        Application.Quit(); //spiel verlassen
    }

    //Playing and Pausing Sound
    private void pauseSound()
    {
        myCarsList = activeCars.getCarsList();
        foreach (GameObject car in myCarsList)
        {
            CarSounds myCarSounds = car.GetComponent<CarSounds>();
            myCarSounds.getAudioSource().Pause();
        }
    }

    private void playSound()
    {
        myCarsList = activeCars.getCarsList();
        foreach (GameObject car in myCarsList)
        {
            CarSounds myCarSounds = car.GetComponent<CarSounds>();
            myCarSounds.getAudioSource().Play();
        }
    }
}
