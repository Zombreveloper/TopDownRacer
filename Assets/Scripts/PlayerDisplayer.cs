using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDisplayer : MonoBehaviour
{
    public PlayerProfile profile;

    public TMP_Text playerName;
    public GameObject inputFieldLeft;
    public GameObject inputFieldRight;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = profile.playerName;
        playerName.color = new Color32(profile.red_value, profile.green_value, profile.blue_value, profile.alpha_value);
    }

    // Update is called once per frame
    void Update()
    {
        profile.leftInput = inputFieldLeft.GetComponent<TMP_InputField>().text;
        profile.rightInput = inputFieldRight.GetComponent<TMP_InputField>().text;
        //Debug.Log(inputFieldLeft.GetComponent<TMP_InputField>().text);
    }
}
