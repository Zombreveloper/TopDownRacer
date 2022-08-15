using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCarDestroyer : MonoBehaviour
{
    GameObject[] activeCarObjects;

    //referenced classes
    private ListOfActiveCars activeCars;



    // Start is called before the first frame update
    void Start()
    {
        //Transform parentsTransform = this.transform.parent;
        //thisCar = parentsTransform.gameObject; //use those if you want to grab a parent object
        activeCars = GameObject.Find("/ParticipantsManager").GetComponent<ListOfActiveCars>();
        //activeCars = GameObject.Find("/ParticipantsManager").GetComponent<ListOfActiveCars>();
        activeCarObjects = GameObject.FindGameObjectsWithTag("Car");
        Debug.Log("This scene starts with " + activeCarObjects.Length + " Cars");
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

            int currentHealth = int.Parse(myPlayer.health);
            if (currentHealth < 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else return false;
    }
}
