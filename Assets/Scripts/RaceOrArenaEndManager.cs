using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceOrArenaEndManager : MonoBehaviour
{
    ListOfActiveCars activeCars;
    public WinnerSO saveWinner;

    void Awake()
    {
        activeCars = FindObjectOfType<ParticipantsManager>().GetComponent<ListOfActiveCars>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForWinner();
    }

    void CheckForWinner()
    {
        if (activeCars.carsList.Count == 1)
        {
            GameObject winnerCar = activeCars.carsList[0];
            PlayerProfile winnerProfile = winnerCar.GetComponent<LassesTestInputHandler>().myDriver;
            saveWinner.winnerSoProfile = winnerProfile;

            StartCoroutine(CountAndSound());
        }
    }

    void CallWinnerScene()
    {
        StopCoroutine(CountAndSound());
        StopCoroutine(CountAndScene());

        SceneManager.LoadScene("WinnerScreen");
    }

    private IEnumerator CountAndSound()
    {
        while(true)
        {
            yield return new WaitForSeconds(1); //wait 2 seconds
            //do thing

            //make a Sound
            StartCoroutine(CountAndScene());
        }
    }

    private IEnumerator CountAndScene()
    {
        while(true)
        {
            yield return new WaitForSeconds(1); //wait 2 seconds
            //do thing

            CallWinnerScene();
        }
    }
}
