using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerCounter : MonoBehaviour
{
    public Button startButton;

    //UI Objects
    /*public GameObject player_1;
    public GameObject player_2;
    public GameObject player_3;
    public GameObject player_4;
    public GameObject player_5;
    public GameObject player_6;
    public GameObject player_7;
    public GameObject player_8;*/

    public List<GameObject> PlayerUI = new List<GameObject>();

    //Scriptable Objects
    public PlayerProfile profile_1;

    //list of all possible playerProfile SOs
    public List<PlayerProfile> PlayerProfileArray = new List<PlayerProfile>();

    //list of all playerProfile SOs that have input-buttons
    public List<PlayerProfile> ReadyPlayersArray = new List<PlayerProfile>(); //maby make this a SO so everybody knows...

    private int currentPlayerUI;
    private int currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerUI = 0;
        currentPlayer = 0;

        startButton.interactable = false; //button has "disabled"-color...

        ReadyPlayersArray.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        countPlayers();
        Debug.Log("im at player " + currentPlayer);
        enoughPlayers();
        //Debug.Log("passed enoughPlayers");
        updateUI();
        //Debug.Log("passed updateUI");
    }

    void countPlayers()
    {
        /*if (profile_1.leftInput != "" && profile_1.rightInput != "") //checks if a player has 2 inputs
        {
            ReadyPlayersArray.Add(profile_1);
            Debug.Log("Player " + player_1.name + " is ready");
        }*/

        if(currentPlayer < PlayerProfileArray.Count) //only possible Player.
        {
            PlayerProfile current = PlayerProfileArray[currentPlayer];

            if (current.leftInput != "" && current.rightInput != "") //checks if a player has 2 inputs
            {
                ReadyPlayersArray.Add(current);
                currentPlayer++;
                Debug.Log("Player " + current.name + " is ready");
            }
        }
    }

    void enoughPlayers()
    {
        //check if players 1 and 2 exist
        //startButton.interactable = true;

        if (ReadyPlayersArray.Count >= 2)
        {
            startButton.interactable = true;
        }
    }

    void updateUI()
    {
        currentPlayerUI = ReadyPlayersArray.Count;
        if(currentPlayerUI < PlayerProfileArray.Count) //only 1 UI-Element for 1 possible Player.
        {
            GameObject currentUI = PlayerUI[currentPlayerUI];
            currentUI.SetActive(true);
        }
    }

    void startGame() //shouod also get called by pressing SpaceBar
    {
        Debug.Log("started Game");
    }
}
