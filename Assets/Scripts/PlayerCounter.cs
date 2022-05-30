using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PlayerCounter : MonoBehaviour
{
    public Button startButton;

    //UI Objects
    public List<GameObject> PlayerUI = new List<GameObject>();

    //list of all possible playerProfile SOs
    public List<PlayerProfile> PlayerProfileArray = new List<PlayerProfile>();

    //list of all playerProfile SOs that have input-buttons
    //public List<PlayerProfile> ReadyPlayersArray = new List<PlayerProfile>(); //maby make this a SO so everybody knows...
    public ReadyPlayersList_SO ReadyPlayersList;

    private int currentPlayerUI;
    private int currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerUI = 0;
        currentPlayer = 0;

        startButton.interactable = false; //button has "disabled"-color...

        ReadyPlayersList.ReadyPlayersArray.Clear();

        resetPlayers();
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

    void resetPlayers() //resets the players Inputs
    {
        foreach(PlayerProfile profile in PlayerProfileArray)
        {
            profile.leftInput = "";
            profile.rightInput = "";
        }
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
                //ReadyPlayersArray.Add(current);
                ReadyPlayersList.ReadyPlayersArray.Add(current);
                currentPlayer++;
                Debug.Log("Player " + current.name + " is ready");
            }
        }
    }

    void enoughPlayers()
    {
        //check if players 1 and 2 exist
        //startButton.interactable = true;

        if (ReadyPlayersList.ReadyPlayersArray.Count >= 2)
        {
            startButton.interactable = true;
        }
    }

    void updateUI()
    {
        currentPlayerUI = ReadyPlayersList.ReadyPlayersArray.Count;
        if(currentPlayerUI < PlayerProfileArray.Count) //only 1 UI-Element for 1 possible Player.
        {
            GameObject currentUI = PlayerUI[currentPlayerUI];
            currentUI.SetActive(true);
        }
    }

    public void startGame() //shouod also get called by pressing SpaceBar
    {
        //Debug.Log("started Game");
        SceneManager.LoadScene("Multiplayer Test");
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Menu Test");
    }
}
