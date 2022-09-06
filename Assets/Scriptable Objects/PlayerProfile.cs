using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObject/PlayerProfile")]
public class PlayerProfile : ScriptableObject
{
    public string playerName;
    public string leftInput;
    public string rightInput;

    public bool ready = false;

    //public string color;
    public byte red_value;
    public byte green_value;
    public byte blue_value;
    public byte alpha_value;

    public string vehicle; //should be a SO on its own
    public string health;
    public string maxHealth;
    public int wayPointCounter;
}
