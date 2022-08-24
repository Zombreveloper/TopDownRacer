using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeRaceSetter : MonoBehaviour
{
    public GameMode_SO gameMode;

    // Start is called before the first frame update
    void Start()
    {
        gameMode.gameMode = "Race";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
