using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObject/TileEffectsOnCar")]

public class TileData : ScriptableObject
{
	public TileBase[] tiles;
	
	public float resistance, slippyness;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
