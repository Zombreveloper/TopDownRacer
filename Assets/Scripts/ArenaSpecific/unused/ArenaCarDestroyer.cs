using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArenaCarDestroyer : MonoBehaviour
{
    public GameMode_SO gameMode;

    //referenced classes
    private ListOfActiveCars activeCars;

    //Events
    public static event Action<GameObject> OnOutOfHealthDestroy;


    // Start is called before the first frame update
    void Start()
    {
        activeCars = GameObject.Find("/ParticipantsManager").GetComponent<ListOfActiveCars>();
        Debug.Log("This scene starts with " + activeCars.getCarsList().Count + " Cars");
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (GameObject car in activeCarObjects)
        foreach (GameObject car in activeCars.getCarsList())
        {
            if ((car != null) && (IsOutOfHealth(car))) //activeCarObjects receives empty entries by deleting values!
            {
                StartCoroutine(ExecuteDestroy(car)); //deletes the car and updates activeCarsList one frame later
                OnOutOfHealthDestroy?.Invoke(car);
            }
        }
    }


    private IEnumerator ExecuteDestroy(GameObject o)
    {
            Destroy(o);
            Debug.Log(o.name + " was destroyed");
            yield return 0;

            activeCars.UpdateList(); //checks for and deletes empty keys in the List
        Debug.Log("Only " + activeCars.carsList.Count + " cars left!");
            yield break;
    }

    public bool IsOutOfHealth(GameObject o)
    {
        if (o)
        {
            //check if health is below 0
            PlayerProfile myPlayer = o.GetComponent<LassesTestInputHandler>().myDriver;
            //Debug.Log("myPlayer: " + myPlayer.name);
            //Debug.Log("GameObject o: " + o.name);
            //Debug.Log("LassesTestInputHandler.myDriver: " + o.GetComponent<LassesTestInputHandler>().myDriver.name);

            if (gameMode.gameMode == "Arena")
            {
                int currentHealth = int.Parse(myPlayer.health);
                if (currentHealth < 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (gameMode.gameMode == "ArenaRace")
            {
                int currentHealth = myPlayer.wayPointCounter;
                if (currentHealth < 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else return false;
    }
}
