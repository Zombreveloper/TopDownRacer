using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceOrArenaEndManager : MonoBehaviour
{
    ListOfActiveCars activeCars;
    public WinnerSO saveWinner;

    //to make sure that check for winner only gets called once
    bool winnerDetermined = false;
    bool checkIfBoolChanged = false;

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
        checkForDownToOneCar();
        if (boolChanged()) //checks for the exact moment where the cars go down to the last one 
        {
            startGameEnd();
        }
    }

    bool boolChanged()
    {
        if (winnerDetermined != checkIfBoolChanged)
        {
            checkIfBoolChanged = winnerDetermined;

            print("winnerDetermined has changed to: " + winnerDetermined);
            if (winnerDetermined == true)
            {
                return true;
            }
        }
        return false;
    }

    void checkForDownToOneCar()
    {
        if (activeCars.carsList.Count == 1)
        {
            winnerDetermined = true;
        }
    }

    void startGameEnd() //Check for winner gets called multiple times!!! set bool here and call other method 1 time!
    {

            GameObject winnerCar = activeCars.carsList[0];
            PlayerProfile winnerProfile = winnerCar.GetComponent<LassesTestInputHandler>().myDriver;
            saveWinner.winnerSoProfile = winnerProfile;

            StartCoroutine(CountAndSound());
    }

    void CallWinnerScene()
    {
        StopCoroutine(CountAndSound());
        StopCoroutine(CountAndScene());

        SceneManager.LoadScene("WinnerScreen");
    }

    private IEnumerator CountAndSound()
    {
            yield return new WaitForSecondsRealtime(0); //wait seconds

            //make a Sound
            Debug.Log("Count and Scene is going to be called");
            StartCoroutine(CountAndScene());
        
    }

    private IEnumerator CountAndScene()
    {
        float slowmoDuration = 1.5f;
        float slowmoFactor = 0.15f;
        SlowmotionEffect slowmo = gameObject.AddComponent<SlowmotionEffect>();
        StartCoroutine(slowmo.reverseSlowmotion(slowmoFactor, slowmoDuration));


        while (slowmo.coroutineRunning)
        {
            yield return null;            
        }
        yield return new WaitForSeconds(2);
        CallWinnerScene();
    }
}
