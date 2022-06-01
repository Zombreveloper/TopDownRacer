using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartTakingCarsList_SO", menuName = "ScriptableObject/PartTakingCarsList_SO")]
public class PartTakingCarsListSO : ScriptableObject
{
    //list with all cars in the race
    public List<GameObject> carsList = new List<GameObject>();
}
