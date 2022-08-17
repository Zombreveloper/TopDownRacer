using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticipantsManager : MonoBehaviour
{
    public ReadyPlayersList_SO allMyParicipants;
    public ListOfActiveCars activeCars; //connect in hirachy
    public GameObject carPrefab;
    private GameObject currentCar;
    private int index = 1;

    public GameMode_SO gameMode;
    public GameObject lifeBarPrefab;
    private GameObject currentLifeBar;
    public GameObject bumperPrefab;
    private GameObject currentBumper;

    void Awake()
    {
        index = 1;
        activeCars.carsList.Clear();
        spawnCars();
    }

        // Start is called before the first frame update
    void Start()
    {
        //index = 1;
        //spawnCars();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnCars()
    {
        if(allMyParicipants.ReadyPlayersArray.Count > 0)
        {
            foreach(PlayerProfile player in allMyParicipants.ReadyPlayersArray)
            {
                
                Vector2 spawnPosition = positionCar();
                //spawn position
                //float x = 10 * index;
                //float y = 0;
                //spawn car at given potition
                currentCar = Instantiate(carPrefab, spawnPosition, Quaternion.identity);
                //give car a name
                currentCar.name = ("Player" + index);
                currentCar.GetComponent<LassesTestInputHandler>().myDriver = player;
                currentCar.GetComponent<CarColor>().myDriver = player;

                //WhatGamemode(currentCar, x, y, player);

                index++;
                //make Car available for every script in the Scene
                activeCars.carsList.Add(currentCar);
            }
        }
    }

    void WhatGamemode(GameObject _currentCar, float x, float y, PlayerProfile player)
    {
        if (gameMode.gameMode == "Arena")
        {
            //Add a life bar to car.
            currentLifeBar = Instantiate(lifeBarPrefab, new Vector2(x,y), Quaternion.identity);
            currentLifeBar.name = ("LifeBar_Player" + index);
            LifeBarScript currentLife_Script = currentLifeBar.GetComponent<LifeBarScript>();
            currentLife_Script.ThisIsYourCar(_currentCar);
            //currentLifeBar.transform.parent = _currentCar.transform;

            //Add a Bumper to car
            currentBumper = Instantiate(bumperPrefab, new Vector2(x,y), Quaternion.identity);
            currentBumper.name = ("Bumper_Player" + index);
            currentBumper.transform.parent = currentCar.transform;
        }
        else if (gameMode.gameMode == "ArenaRace")
        {
            //activate WayPoints
            //Debug.Log("WAYPOINTS");
            player.wayPointCounter = 0;
            ArenaRaceManagerScript arms = FindObjectOfType<ArenaRaceManagerScript>();
            arms.InitiateArenaRace();
        }
    }

    Vector2 positionCar()
    {
        int xDistance = 6; //vertical distance between cars in start Grid
        int yDistance = 3; //horizontal distance in start Grid
        int carsInRow = 4;
        float setBackRow = xDistance * carsInRow; //widht of the row on the starting grid

        if (index <= carsInRow) //first 4 cars
        {
            float x = xDistance * index;
            float y = yDistance * -index;
            return new Vector2(x, y);
        }
        else if (index > carsInRow && index <= (carsInRow * 2)) //other 4 cars
        {            
            float x = (xDistance * index) - setBackRow;
            float y = yDistance * -index;
            return new Vector2(x, y);
        }
        else //if for whatever reason there would be more than 8 cars on the track
        {
            float x = (xDistance * index) - 2*setBackRow;
            float y = yDistance * -index;
            return new Vector2(x, y);
        }

    }
}
