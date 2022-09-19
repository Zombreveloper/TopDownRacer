using System.Collections;
using System.Collections.Generic;
using System;
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
    public GameObject wayPointDisplayerPrefab;
    private GameObject currentWayPointDisplayer;
    public GameObject bumperPrefab;
    private GameObject currentBumper;

    private GameObject startPointObject; //GameObject in a level to determine where the grid of cars spawns (optional)

    void Awake()
    {
        index = 1;
        activeCars.carsList.Clear();

        startPointObject = GameObject.Find("/LevelStartPoint");
        if (startPointObject != null)
            Debug.Log("I found a startpoint!");
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

                WhatGamemode(currentCar, spawnPosition, player);

                index++;
                //make Car available for every script in the Scene
                activeCars.carsList.Add(currentCar);
            }
        }
    }

    void WhatGamemode(GameObject _currentCar, Vector2 _spawnPos, PlayerProfile player)
    {
        if (gameMode.gameMode == "Arena")
        {
            //Add a life bar to car.
            AddLifeBar(_currentCar, _spawnPos);
            //currentLifeBar.transform.parent = _currentCar.transform;

            //Add a Bumper to car
            //currentBumper = Instantiate(bumperPrefab, _spawnPos, Quaternion.identity);
            //currentBumper.name = ("Bumper_Player" + index);
            //currentBumper.transform.parent = currentCar.transform;
        }
        else if (gameMode.gameMode == "ArenaRace")
        {
            //activate WayPoints
            //Debug.Log("WAYPOINTS");
            //player.wayPointCounter = 20;
            ArenaRaceManagerScript arms = FindObjectOfType<ArenaRaceManagerScript>();
            arms.InitiateArenaRace();

            AddLifeBar(_currentCar, _spawnPos);
        }
    }

    void AddLifeBar(GameObject _currentCar, Vector2 _spawnPos)
    {
        currentLifeBar = Instantiate(lifeBarPrefab, _spawnPos, Quaternion.identity);
        currentLifeBar.name = ("LifeBar_Player" + index);
        LifeBarScript currentLife_Script = currentLifeBar.GetComponent<LifeBarScript>();
        currentLife_Script.ThisIsYourCar(_currentCar);
    }

    //positions participating cars on a formula1 like starting grid. Propably better off as an own class but whatever
    //TODO: add in the reference Points rotation if necessary
    Vector2 positionCar()
    {
        int _lowIndex = index - 1; //to let Index start at 0. Important for Division
        Vector2 _referencePoint = Vector2.zero;
        if (startPointObject != null)
        {
           _referencePoint = startPointObject.transform.position;
            //Debug.Log("LevelStartPoint is on " + _startPoint);
        }
        int _xDistance = 6; //vertical distance between cars in start Grid
        int _yDistance = 3; //horizontal distance in start Grid
        int _carsInRow = 4;
        float _rowWidth = _xDistance * (_carsInRow-1);

        int _placeInRow = _lowIndex % _carsInRow; //gives a value between 0 and the amount of cars to be set until the row resets

        //calculations start here
            float x = (_xDistance * _placeInRow);
            float y = _yDistance * -index;
            Vector2 _spawnPoint = new Vector2(x, y) + _referencePoint;
            _spawnPoint = _spawnPoint - new Vector2(_rowWidth / 2, 0); //offsets the LevelStartPoint to the center of the row
            return _spawnPoint;
    }


    // trash dump
    Vector2 positionCarBackup()
    {
        Vector2 _startPoint = Vector2.zero;
        if (startPointObject != null)
        {
            _startPoint = startPointObject.transform.position;
            Debug.Log("LevelStartPoint is on " + _startPoint);
        }
        int _xDistance = 6; //vertical distance between cars in start Grid
        int _yDistance = 3; //horizontal distance in start Grid
        int _carsInRow = 4;
        float _setBackRow = _xDistance * _carsInRow; //width of the row on the starting grid

        if (index <= _carsInRow) //first 4 cars
        {
            float x = _xDistance * index;
            float y = _yDistance * -index;
            Vector2 _spawnPoint = new Vector2(x, y) + _startPoint;
            return _spawnPoint;
        }
        else if (index > _carsInRow && index <= (_carsInRow * 2)) //other 4 cars
        {
            float x = (_xDistance * index) - _setBackRow;
            float y = _yDistance * -index;
            Vector2 _spawnPoint = new Vector2(x, y) + _startPoint;
            return _spawnPoint;
        }
        else //if for whatever reason there would be more than 8 cars on the track
        {
            float x = (_xDistance * index) - 2 * _setBackRow;
            float y = _yDistance * -index;
            Vector2 _spawnPoint = new Vector2(x, y) + _startPoint;
            return _spawnPoint;
        }
        /* //here goes pseudo code
        {
            float x = (_xDistance * [Rest aus Division index/_cars in row]) - [index / _cars in row, Ergebnis in ganzer Zahl!!] * _setBackRow;
            float y = _yDistance * -index;
            Vector2 _spawnPoint = new Vector2(x, y) + _startPoint;
            return _spawnPoint;
        }*/

    }
}
