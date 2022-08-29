using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperDeActivator : MonoBehaviour
{
    public GameMode_SO gameMode;
    public GameObject bumper;

    // Start is called before the first frame update
    void Start()
    {
        if (gameMode.gameMode == "ArenaRace" || gameMode.gameMode == "Race")
        {
            bumper.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
