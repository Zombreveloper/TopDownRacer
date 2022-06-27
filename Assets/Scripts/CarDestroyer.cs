using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    GameObject thisCar;

    [SerializeField]
    [Tooltip("Threshold how much the object can be out of sight without getting destroyed. Measured in Pixels.")] 
    private float goodwill = 0; //how much the car is allowed to go over the borders before being destroyed

    //referenced classes
    private ListOfActiveCars activeCars;



    // Start is called before the first frame update
    void Start()
    {
        //Transform parentsTransform = this.transform.parent;
        //thisCar = parentsTransform.gameObject; //use those if you want to grab a parent object
        thisCar = this.gameObject;
        activeCars = GameObject.Find("/ParticipantsManager").GetComponent<ListOfActiveCars>();

        //StartCoroutine(ExecuteDestroy());

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOutOfScreen(thisCar) == true)
        {
            // gescheiterter Versuch, die Liste nur einmal checken zu müssen. 
            // da das Auto erst einen Frame später zerstört ist, muss die Liste mit dem Updaten einen Frame warten
            /*int i = 0;
            while (i == 0)
            {
                if (i < 2)
                {
                    Destroy(thisCar);
                    Debug.Log("Car was destroyed");
                }
                else
                {
                    activeCars.UpdateList(); //Constantly checks for and deletes empty keys in the List
                }

            }*/

            Destroy(thisCar);
            Debug.Log("Car was destroyed");
        }

        //Constantly checks for and deletes empty keys in the List. This gets only called by the remaining Cars => bad design!!
        activeCars.UpdateListOnNextFrame(); 


    }

    private IEnumerator ExecuteDestroy(int i = 0)
    {
        if (i == 0)
        {
            Destroy(thisCar);
            Debug.Log("Car was destroyed");
            i++;
            yield return null;
        }
        else
        {
            activeCars.UpdateList(); //Constantly checks for and deletes empty keys in the List
        }
        //yield break;
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
            if (min.x > Screen.width + goodwill || max.x < 0f - goodwill ||
                min.y > Screen.height + goodwill || max.y < 0f - goodwill)
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

    /*public Vector2 SpriteScreenSize(GameObject o, Camera cam = null)
    {
        if (cam == null) cam = Camera.main;
        Vector2 sdim = new Vector2();
        //Renderer ren = o.GetComponent<Renderer>() as Renderer;
        if (o)
        {
            sdim = cam.WorldToScreenPoint(o.transform.position) -
                cam.WorldToScreenPoint(o.transform.position);
        }
        return sdim;
    }*/

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
}
