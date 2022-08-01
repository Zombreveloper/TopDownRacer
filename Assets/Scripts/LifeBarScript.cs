using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeBarScript : MonoBehaviour
{
    PlayerProfile myPlayer;
    string myHealth;
    public TMP_Text healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponentInParent<LassesTestInputHandler>().myDriver;
    }

    // Update is called once per frame
    void Update()
    {
        myHealth = myPlayer.health;
        healthDisplay.text = myHealth;
    }
}
