using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    GameObject thisCar;

    [SerializeField]
    [Tooltip("Threshold how much the object can be out of sight without getting destroyed. Measured in Pixels.")] 
    private float goodwill = 0; //how much the car is allowed to go over the borders before being destroyed

    


    // Start is called before the first frame update
    void Start()
    {
        //Transform parentsTransform = this.transform.parent;
        //thisCar = parentsTransform.gameObject; //use those if you want to grab a parent object
        thisCar = this.gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        if (IsOutOfScreen(thisCar) == true)
        {
            Destroy(thisCar);
            Debug.Log("Car was destroyed");
        }
            
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
