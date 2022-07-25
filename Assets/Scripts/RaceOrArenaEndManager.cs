using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceOrArenaEndManager : MonoBehaviour
{
    ListOfActiveCars activeCars;
    string winner;

    void Awake()
    {
        activeCars = FindObjectOfType<ParticipantsManager>().GetComponent<ListOfActiveCars>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForWinner();
    }

    void CheckForWinner()
    {
        if (activeCars.carsList.Count == 1)
        {
            GameObject winnerCar = activeCars.carsList[0];
            winner = winnerCar.name;
            //Debug.Log("THE WINNER IS " + winner);

            //farbe muss noch abgeschrieben werden...
            /*SpriteRenderer winnerSprite = winnerCar.GetComponentsInChildren<SpriteRenderer>();
            Color32 winnercolor = winnerSprite.color;*/
        }
    }
}
