using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ArenaRaceManagerScript : MonoBehaviour
{
    //referenced classes
    public WinnerSO saveWinner;
    public GameMode_SO gameMode;

    //referenced GameObject
    public List<GameObject> allWayPoints = new List<GameObject>();
    GameObject prevWayPoint;
    GameObject currentWayPoint;
    public GameObject Arrow;
    public List<GameObject> allHealthPads = new List<GameObject>();

    private ListOfActiveCars activeCars;

    //referenced variables
    //bool doRace = false;
    int numberOfWayPoints;
    bool arrowActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //set all players wayPointCounter to 0 -> happens in ParticipantsManager
        numberOfWayPoints = allWayPoints.Count;
        prevWayPoint = null;

        //WayPointScript.OnCarGotWaypoint += EventOnCarGotWaypoint;

        activeCars = GameObject.Find("/ParticipantsManager").GetComponent<ListOfActiveCars>();

        ActivateHealthRegenPad();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateArrow();
    }

    public void InitiateArenaRace()
    {
        //Set all WayPoints to Active or something similar...
        //Debug.Log("Start ArenaRace");

        //doRace = true;
        ActivateFirstWayPoint();

        /*
        foreach (GameObject waypoint in allWayPoints)
        {
            waypoint.SetActive(true);
        }
        */

        if (gameMode.gameMode == "ArenaRace")
        {
            ActivateArrow();
        }
    }

    void ActivateArrow()
    {
        // Richtungsvektor aus pos mitte und pos waypoint
        // Richtungsvektor Mitte_Pylon = Pos Poylon - Pos Mitte
        arrowActive = true;
        Arrow.SetActive(true);
    }

    void UpdateArrow()
    {
        if (arrowActive)
        {
            Arrow.GetComponent<ArrowToPylonScript>().UpdateArrow(currentWayPoint);
        }
    }

    void ActivateFirstWayPoint()
    {
        currentWayPoint = allWayPoints[UnityEngine.Random.Range(0, numberOfWayPoints)];
        ActivateWayPoint(currentWayPoint);
    }

    /*void EventOnCarGotWaypoint()
    {
        UpdateWayPoints();

    }*/

    public void PunishOthers(string carGotWaypoint)
    {
        foreach (GameObject car in activeCars.carsList)
        {
            string currentName = car.name;
            if (currentName != carGotWaypoint)
            {
                PlayerProfile myPlayer = car.GetComponent<LassesTestInputHandler>().myDriver;

                //myPlayer.wayPointCounter--;
                int myhealth = int.Parse(myPlayer.health);
                myhealth--;
                myPlayer.health = myhealth.ToString();
            }
        }
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

    public void UpdateWayPoints() //gets called by WayPointScript when triggered
    {
        int randomNumber = UnityEngine.Random.Range(0, numberOfWayPoints);
        prevWayPoint = currentWayPoint;
        prevWayPoint.SetActive(false);

        //some checkpoint that is not the previous one
        currentWayPoint = allWayPoints[randomNumber];
        if (currentWayPoint == prevWayPoint)
        {
            if ((randomNumber + 1) == numberOfWayPoints)
            {
                currentWayPoint = allWayPoints[((randomNumber + 1) - numberOfWayPoints)];
                //ActivateWayPoint(currentWayPoint);
                currentWayPoint.SetActive(true);
            }
            else
            {
                currentWayPoint = allWayPoints[(randomNumber + 1)];
                //ActivateWayPoint(currentWayPoint);
                currentWayPoint.SetActive(true);
            }
        }
        else
        {
            currentWayPoint.SetActive(true);
        }
    }

    void ActivateWayPoint(GameObject _currentWayPoint)
    {
        Debug.Log("ActivateWayPoint");
        _currentWayPoint.SetActive(true);
        prevWayPoint = _currentWayPoint;
    }

    public void Winner(PlayerProfile myWinner)
    {
        //amount of WayPoints to win get declared in CarCollisionManagers OnTriggerEnter2D

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

    void ActivateHealthRegenPad()
    {
        if (gameMode.gameMode == "ArenaRace")
        {
            foreach (GameObject heatlthRegenPad in allHealthPads)
            {
                if (heatlthRegenPad.activeSelf == true)
                {
                    heatlthRegenPad.SetActive(false);
                }
            }
        }
        else if (gameMode.gameMode == "Arena")
        {
            foreach (GameObject heatlthRegenPad in allHealthPads)
            {
                if (heatlthRegenPad.activeSelf == false)
                {
                    heatlthRegenPad.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("ERROR: ArenaRaceManagerScript Line 199: No Gamemode!");
        }
    }
}
