using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObject/PlayerProfile")]
public class PlayerProfile : ScriptableObject
{
    public string playerName;
    public string leftInput;
    public string rightInput;

    public string color; //should be a hex-value
    public string vehicle; //should be a SO on its own
    public string health;
}
