using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
	[SerializeField]
	private Tilemap map;
	
	[SerializeField]
	private List<TileData> tileDatas;
	
	private Dictionary<TileBase, TileData> dataFromTiles;
	
	private void Awake()
	{
		dataFromTiles = new Dictionary<TileBase, TileData>();
		
		foreach (var tileData in tileDatas)
		{
			foreach (var tile in tileData.tiles)
			{
				dataFromTiles.Add(tile, tileData);
			}
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		testTileProperties();	
    }
	
	void testTileProperties()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int gridPosition = map.WorldToCell(mousePosition);
			
			TileBase clickedTile = map.GetTile(gridPosition);
			
			if (clickedTile != null)
			{
			float resistance = dataFromTiles[clickedTile].resistance;
			print("At postition " + gridPosition + " there is a " + clickedTile + "with a resistance of " + resistance);
			}
			else print("There is no Tile set on" + gridPosition);
		}
	}
	
	public float GetTileResistance(Vector2 worldPosition) //Gets called by the CarController
	{
		Vector3Int gridPosition = map.WorldToCell(worldPosition);
		TileBase tile = map.GetTile(gridPosition);
		if (tile != null)
		{
			float resistance = dataFromTiles[tile].resistance;
			return resistance;
		}
		else return 1;
	}
}
