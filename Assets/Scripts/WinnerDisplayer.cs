using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinnerDisplayer : MonoBehaviour
{
    public WinnerSO saveWinner;
    public TMP_Text nameTextDisplay;

    private PlayerProfile myWinner;
    private string myWinnerName;

    //private winners name string color;
    private byte myRed_value;
    private byte myGreen_value;
    private byte myBlue_value;
    private byte myAlpha_value;

    // Start is called before the first frame update
    void Start()
    {
        myWinner = saveWinner.winnerSoProfile;
    }

    // Update is called once per frame
    void Update()
    {
        GetName();
        GetColor();
        DisplayName();
    }

    void GetName()
    {
        myWinnerName = myWinner.playerName;
    }

    void GetColor()
    {
        myRed_value = myWinner.red_value;
        myGreen_value = myWinner.green_value;
        myBlue_value = myWinner.blue_value;
        myAlpha_value = myWinner.alpha_value;
    }

    void DisplayName()
    {
        nameTextDisplay.text = myWinnerName;
        nameTextDisplay.color = new Color32(myRed_value, myGreen_value, myBlue_value, myAlpha_value);
    }


//the scene has to work, and i dont wanna write another script just for this method...
    public void backToMainMenu()
    {
        SceneManager.LoadScene("Menu Test");
    }
}
