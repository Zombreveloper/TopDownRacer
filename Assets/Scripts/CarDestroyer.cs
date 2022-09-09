using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarDestroyer : MonoBehaviour
{
    GameObject[] activeCarObjects;

    [SerializeField]
    [Tooltip("Threshold how much the object can be out of sight without getting destroyed. Measured in Pixels.")]
    private float threshold = 0; //how much the car is allowed to go over the borders before being destroyed

    public bool dependsOnHealth;

    //referenced classes
    public GameMode_SO gameMode;
    private ListOfActiveCars activeCars;

    //Events
    public static event Action<GameObject> OnCarDestroy;


    // Start is called before the first frame update
    void Start()
    {
        //Transform parentsTransform = this.transform.parent;
        //thisCar = parentsTransform.gameObject; //use those if you want to grab a parent object
        activeCars = GameObject.Find("/ParticipantsManager").GetComponent<ListOfActiveCars>();

        //for getting Objects by the activeCarsList
        Debug.Log("This scene starts with " + activeCars.getCarsList().Count + " Cars");


    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject car in activeCars.getCarsList())
        {
            if (dependsOnHealth == true) //depends on cars Health
            {
                if (car!=null && IsOutOfHealth(car)) //activeCarObjects receives empty entries by deleting values!
                {

                    StartCoroutine(ExecuteDestroy(car)); //deletes the car and updates activeCarsList one frame later
                    OnCarDestroy?.Invoke(car);
                }
            }
            else if (dependsOnHealth == false) //depends on cars Position in Camera View
            {
                if (car!=null && IsOutOfScreen(car)) //activeCarObjects receives empty entries by deleting values!
                {

                    StartCoroutine(ExecuteDestroy(car)); //deletes the car and updates activeCarsList one frame later
                    OnCarDestroy?.Invoke(car);
                }
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

    //inspired from this website https://stackoverflow.com/questions/23217840/unity-2d-destroy-instantiated-prefab-when-it-goes-off-screen
    public bool IsOutOfScreen(GameObject o, Camera cam = null)
    {
        bool result = false;
        Renderer renderer = o.GetComponent<Renderer>();
        if (o)
        {
            if (cam == null) cam = Camera.main;
            Vector2 sdim = SpriteScreenSize(o, cam);
            Vector2 pos = cam.WorldToScreenPoint(o.transform.position);
            Vector2 min = pos - sdim;
            Vector2 max = pos + sdim;
            if (min.x > Screen.width + threshold || max.x < 0f - threshold ||
                min.y > Screen.height + threshold || max.y < 0f - threshold)
            {
                result = true;
            }
        }
        else
        {
            //TODO: throw exception or something
        }
        return result;
    }

    public Vector2 SpriteScreenSize(GameObject o, Camera cam = null)
    {
        if (cam == null) cam = Camera.main;
        Vector2 sdim = new Vector2();
        Renderer ren = o.GetComponentInChildren<Renderer>() as Renderer;
        if (ren)
        {
            sdim = cam.WorldToScreenPoint(ren.bounds.max) -
                cam.WorldToScreenPoint(ren.bounds.min);
        }
        return sdim;
    }

    //public functions
    public void callExecuteDestroy(int i)
    {
        Debug.Log("bleep bloop");
        if (activeCars.carsList[i] != null)
        {
            GameObject car = activeCars.carsList[i];
            StartCoroutine(ExecuteDestroy(car)); //deletes the car and updates activeCarsList one frame later
        }
        else
        {
            Debug.Log("car on index " + i + "doesn't exist");
        }

    }



    public bool IsOutOfHealth(GameObject o)
    {
        if (o)
        {
            int currentHealth = getHealthPoints(o);

            if (currentHealth < 1)
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

    private int getHealthPoints(GameObject o)
    {
        PlayerProfile myPlayer = o.GetComponent<LassesTestInputHandler>().myDriver;
        int currentHealth = 20;
        if (gameMode.gameMode == "Arena")
        {
            currentHealth = int.Parse(myPlayer.health);
        }
        else if (gameMode.gameMode == "ArenaRace")
        {
            currentHealth = myPlayer.wayPointCounter;
        }
        return currentHealth;
    }

}
