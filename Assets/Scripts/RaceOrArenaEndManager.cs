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
            yield return new WaitForSeconds(1); //wait seconds
            //do thing

            //make a Sound
            StartCoroutine(CountAndScene());
        }
        //SlowmotionEffect slowmo = gameObject.AddComponent<SlowmotionEffect>();
    }

    private IEnumerator CountAndScene()
    {
        float secondsTillEndScene = 1;
        float slowmoFactor = 0.3f;
        //SlowmotionEffect slowmo = new SlowmotionEffect(slowmoFactor, secondsTillEndScene);

        while (true)
        {

            //Time.timeScale = 0.3f;
            yield return new WaitForSeconds(secondsTillEndScene); //wait seconds
            //Time.timeScale = 1f;

            CallWinnerScene();
        }
    }
}
