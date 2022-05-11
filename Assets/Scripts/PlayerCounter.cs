using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerCounter : MonoBehaviour
{
    public GameObject startButton;
    public GameObject player_1;
    public GameObject player_2;
    public GameObject player_3;
    public GameObject player_4;
    public GameObject player_5;
    public GameObject player_6;
    public GameObject player_7;
    public GameObject player_8;


    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> players = new List<GameObject>();
        players.Add(player_1);
        players.Add(player_2);
        players.Add(player_3);
        players.Add(player_4);
        players.Add(player_5);
        players.Add(player_6);
        players.Add(player_7);
        players.Add(player_8);

        //startButton.interactable = false; //button has "disabled"-color...
    }

    // Update is called once per frame
    void Update()
    {
        enoughPlayers();
        //setPlayer();
    }
/*
    setPlayer()
    {
        //somehow make player
        currentPlayer = players[0];
        //currentPlayer
    }*/

    void enoughPlayers()
    {
        //check if players 1 and 2 exist
        //startButton.interactable = true;

    }

    void startGame()
    {
        Debug.Log("started Game");
    }
}
