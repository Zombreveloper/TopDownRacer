using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColor : MonoBehaviour
{
    public PlayerProfile myDriver;
    private SpriteRenderer mySkin;

    void Awake()
    {
        /*foreach(SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
          spriteRenderer.color = new Color32(myDriver.red_value, myDriver.green_value, myDriver.blue_value, myDriver.alpha_value);
        }*/
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
          spriteRenderer.color = new Color32(myDriver.red_value, myDriver.green_value, myDriver.blue_value, myDriver.alpha_value);
        }
        //mySkin = GetComponentInChildren<SpriteRenderer>();
        //mySkin.color = new Color32(myDriver.red_value, myDriver.green_value, myDriver.blue_value, myDriver.alpha_value);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
