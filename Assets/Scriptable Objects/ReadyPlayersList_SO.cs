using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReadyPlayersList_SO", menuName = "ScriptableObject/ReadyPlayersList_SO")]
public class ReadyPlayersList_SO : ScriptableObject
{
    //public List with players that participate in the race, gets participants from PlayerCounter.
    
    //list of all playerProfile SOs that have input-buttons
    public List<PlayerProfile> ReadyPlayersArray = new List<PlayerProfile>(); //maby make this a SO so everybody knows...
}
