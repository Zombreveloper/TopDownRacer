using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticipantsManager : MonoBehaviour
{
    public ReadyPlayersList_SO allMyParicipants;
    public PartTakingCarsListSO allCars; //make this a MonoBehaviour instead of SO
    public GameObject carPrefab;
    private GameObject currentCar;
    private int index = 1;

    void Awake()
    {
        index = 1;
        allCars.carsList.Clear(); //make this a MonoBehaviour instead of SO
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
                index++;
                currentCar.GetComponent<LassesTestInputHandler>().myDriver = player;
                currentCar.GetComponent<CarColor>().myDriver = player;
                //make Car available for every script in the Scene
                allCars.carsList.Add(currentCar); //make this a MonoBehaviour instead of SO
            }
        }
    }
}
