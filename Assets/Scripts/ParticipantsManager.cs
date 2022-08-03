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
                //spawn position
                float x = 10 * index;
                float y = 0;
                //spawn car at given potition
                currentCar = Instantiate(carPrefab, new Vector2(x,y), Quaternion.identity);
                //give car a name
                currentCar.name = ("Player" + index);
                currentCar.GetComponent<LassesTestInputHandler>().myDriver = player;
                currentCar.GetComponent<CarColor>().myDriver = player;

                WhatGamemode(currentCar, x, y);

                index++;
                //make Car available for every script in the Scene
                activeCars.carsList.Add(currentCar);
            }
        }
    }

    void WhatGamemode(GameObject _currentCar, float x, float y)
    {
        if (gameMode.gameMode == "Arena")
        {
            //Add a life bar to car.
            currentLifeBar = Instantiate(lifeBarPrefab, new Vector2(x,y), Quaternion.identity);
            currentLifeBar.name = ("LifeBar_Player" + index);
            LifeBarScript currentLife_Script = currentLifeBar.GetComponent<LifeBarScript>();
            currentLife_Script.ThisIsYourCar(_currentCar);
            //currentLifeBar.transform.parent = _currentCar.transform;
        }
    }
}
