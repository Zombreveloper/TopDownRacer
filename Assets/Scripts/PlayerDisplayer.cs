using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDisplayer : MonoBehaviour
{
    public PlayerProfile profile_1;

    public TMP_Text playerName;
    public GameObject inputFieldLeft;
    public GameObject inputFieldRight;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = profile_1.playerName;
        playerName.color = new Color32(profile_1.red_value, profile_1.green_value, profile_1.blue_value, profile_1.alpha_value);
    }

    // Update is called once per frame
    void Update()
    {
        profile_1.leftInput = inputFieldLeft.GetComponent<TMP_InputField>().text;
        profile_1.rightInput = inputFieldRight.GetComponent<TMP_InputField>().text;
        //Debug.Log(inputFieldLeft.GetComponent<TMP_InputField>().text);
    }
}
