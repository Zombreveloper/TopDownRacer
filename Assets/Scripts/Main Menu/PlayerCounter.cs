using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

using System.Text.RegularExpressions;

public class PlayerCounter : MonoBehaviour
{
    public Button startButton;
    public Button buttonToRaceTrack; //Button to go to random generated Race Track

    //UI Objects
    public List<GameObject> PlayerUI = new List<GameObject>();
    public Button buttonBack;

    //list of all possible playerProfile SOs
    public List<PlayerProfile> PlayerProfileArray = new List<PlayerProfile>();

    //list of all playerProfile SOs that have input-buttons
    //public List<PlayerProfile> ReadyPlayersArray = new List<PlayerProfile>(); //maby make this a SO so everybody knows...
    public ReadyPlayersList_SO ReadyPlayersList;

    private int currentPlayerUI;
    private int currentPlayer;

    public GameMode_SO gameMode;

    public GameObject doubleKeyBindErrorText;
    public GameObject notAlphanumericErrorText;

    // Set Health
    public GameObject healthBG; //parent of all health GOs
    public Slider healthSlider; //has a value
    public TMP_Text startHealthText; // is just there
    public TMP_Text healthText; //tells the value of healthSlider

    // Start is called before the first frame update
    void Start()
    {
        doubleKeyBindErrorText.SetActive(false);

        currentPlayerUI = 0;
        currentPlayer = 0;

        startButton.interactable = false; //button has "disabled"-color...
        buttonToRaceTrack.interactable = false;

        ReadyPlayersList.ReadyPlayersArray.Clear();

        resetPlayers();
        resetHealth();

        if (gameMode.gameMode == "Race")
        {
            healthBG.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        countPlayers();
        //Debug.Log("im at player " + currentPlayer);
        enoughPlayers();
        //Debug.Log("passed enoughPlayers");
        updateUI();
        //Debug.Log("passed updateUI");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttonBack.onClick.Invoke();
        }
    }

    void resetPlayers() //resets the players Inputs
    {
        foreach(PlayerProfile profile in PlayerProfileArray)
        {
            profile.leftInput = "";
            profile.rightInput = "";
        }
    }

    void resetHealth() //gets called on start
    {
        if (gameMode.gameMode == "Arena")
        {
            healthSlider.value = 10f;
            //healthText = GetComponent<TMP_Text>(); //evtl nötig
            healthText.text = "10";
        }
        else if (gameMode.gameMode == "ArenaRace")
        {
            healthSlider.value = 20f;
            healthText.text = "20";
        }

        foreach (PlayerProfile profile in PlayerProfileArray)
        {
            profile.health = "10";
            profile.wayPointCounter = 20;
        }
    }

    void setHealth(string status) //gets called on startGame
    {
        foreach (PlayerProfile profile in PlayerProfileArray)
        {
            float temp = healthSlider.value;
            int iTemp = (int)temp; //makes an int from it
            //Debug.Log(temp);

            profile.health = temp.ToString();

            profile.wayPointCounter = iTemp;
        }
    }

    void updateHealthSlider()
    {
        float temp = healthSlider.value;
        healthText.text = temp.ToString();
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
                //Debug.Log("Player " + current.name + " is ready");
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
            buttonToRaceTrack.interactable = true;
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

        updateHealthSlider();
    }

    bool checkKeys()
    {
        doubleKeyBindErrorText.SetActive(false);
        notAlphanumericErrorText.SetActive(false);

        List<string> usedKeys = new List<string>();
        usedKeys.Add(" ");

        var keysToAdd = new List<string>();

        //check for double bindet keys
        foreach(PlayerProfile player in ReadyPlayersList.ReadyPlayersArray)
        {
            foreach(string key in usedKeys)
            {
                if(key != player.leftInput && key != player.rightInput)
                {
                    //keysToAdd.Add(player.leftInput);
                    //keysToAdd.Add(player.rightInput);

                    if(player.leftInput == player.rightInput)
                    {
                        Debug.Log("Some Keys are bound multiple times! Change Your Key-Bindings!");
                        doubleKeyBindErrorText.SetActive(true);

                        return false;
                    }


                    //check if key is alphanumeric
                    var myRegExp = new Regex("^[a-z0-9]$"); //regular Expression, checks for one key between lower case a to z and numeers 0 to 9

                    if( myRegExp.IsMatch(player.leftInput) && myRegExp.IsMatch(player.rightInput) )
                    {
                        keysToAdd.Add(player.leftInput);
                        keysToAdd.Add(player.rightInput);
                    }
                    else
                    {
                        Debug.Log("There may only be one (1) lower case alphanumeric (a-z & 0-9) key per Input! Change Your Key-Bindings!");
                        notAlphanumericErrorText.SetActive(true);

                        return false;
                    }

/*
                    if(key == "a" || key == "b" || key == "c" || key == "d" || key == "e" || key == "f" || key == "g" || key == "h" || key == "i" ||
                    key == "j" || key == "k" || key == "l" || key == "m" || key == "n" || key == "o" || key == "p" || key == "q" || key == "r" ||
                    key == "s" || key == "t" || key == "u" || key == "v" || key == "w" || key == "x" || key == "y" || key == "z" ||
                    key == "0" || key == "1" || key == "2" || key == "3" || key == "4" || key == "5" || key == "6" || key == "7" || key == "8" ||
                    key == "9")
                    {
                        Debug.Log("RegExp");
                        keysToAdd.Add(player.leftInput);
                        keysToAdd.Add(player.rightInput);
                    }
                    else
                    {
                        Debug.Log("Some Keys are not alphanumeric! Change Your Key-Bindings!");
                        notAlphanumericErrorText.SetActive(true);

                        return false;
                    }*/
                }
                else
                {
                    Debug.Log("Some Keys are bound multiple times! Change Your Key-Bindings!");
                    doubleKeyBindErrorText.SetActive(true);

                    return false;
                }
            }
            usedKeys.AddRange(keysToAdd);
        }
        Debug.Log("have fun playing!");
        return true;
    }

    public void startGame() //shouod also get called by pressing SpaceBar
    {
        //Debug.Log("started Game");

        if(checkKeys())
        {
            Debug.Log("Start Game!");

            if (gameMode.gameMode == "Race")
            {
                SceneManager.LoadScene("Race Track Scene");
            }
            else if (gameMode.gameMode == "Arena")
            {
                setHealth("Race");
                SceneManager.LoadScene("Arena mk1");
            }
            else if (gameMode.gameMode == "ArenaRace")
            {
                setHealth("ArenaRace");
                SceneManager.LoadScene("Arena mk1");
            }


        }
    }

    public void startGeneratedGame() //shouod also get called by pressing SpaceBar
    {
        //Debug.Log("started Game");
        SceneManager.LoadScene("Multiplayer Test");
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("Menu Test");
    }
}
