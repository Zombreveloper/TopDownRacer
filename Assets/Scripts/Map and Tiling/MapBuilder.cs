using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
	[SerializeField] Tilemap currentTilemap;
	[SerializeField] TileBase currentTile;
	
	[SerializeField] Camera cam; //Only necessary for pointing with mouse.
	
	private Vector3Int buildPos;
	
    // Start is called before the first frame update
    void Start()
    {
		for (int i=-2; i<2; i++)
		{
			buildPos.Set(i,1,0);
			PlaceTile(buildPos);
		}
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int pos = currentTilemap.WorldToCell(mousePosition);
		
		
		if (Input.GetMouseButton(0))
		{
			PlaceTile(pos);
		}
    }
	
	
	void PlaceTile(Vector3Int pos)
	{
		currentTilemap.SetTile(pos, currentTile);
	}
}