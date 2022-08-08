using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaRaceManagerScript : MonoBehaviour
{
    //referenced classes
    public WinnerSO saveWinner;

    //referenced GameObject
    public List<GameObject> allWayPoints = new List<GameObject>();

    //referenced variables
    bool doRace = false;

    // Start is called before the first frame update
    void Start()
    {
        //set all players wayPointCounter to 0
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitiateArenaRace()
    {
        //Set all WayPoints to Active or something similar...
        //Debug.Log("Start ArenaRace");

        doRace = true;

        foreach (GameObject waypoint in allWayPoints)
        {
            waypoint.SetActive(true);
        }
    }

    public void Winner(PlayerProfile myWinner)
    {
        //pass winner
        saveWinner.winnerSoProfile = myWinner;
        StartCoroutine(CountAndSound());
    }


    //copied from RaceOrArenaEndManager
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
