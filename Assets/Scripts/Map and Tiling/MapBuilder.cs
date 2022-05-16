using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Based on the Video: Edit, Save And Load Unity Tilemaps at Runtime! https://www.youtube.com/watch?v=snUe2oa_iM0
*/

public class MapBuilder : MonoBehaviour
{
	[SerializeField] Tilemap currentTilemap;
	[SerializeField] TileBase currentTile;
	
	[SerializeField] Camera cam; //Only necessary for pointing with mouse.
	
	[SerializeField] Grid currentGrid;
	
	public GameObject straightTrackPrefab;
	
	private Vector3Int buildPos;
	private Vector3 prefabSpawnPoint;
	private Tile _straightTrack;
	
	
    // Start is called before the first frame update
    void Start()
    {
		
		//builds the street Tiles for now. going to be outsourced
		for (int i=-2; i<2; i++)
		{
			buildPos.Set(i,1,0);
			PlaceSingleTile(buildPos);
			
			prefabSpawnPoint = currentGrid.CellToWorld(buildPos);
			GameObject newStraightTile = Instantiate(straightTrackPrefab, prefabSpawnPoint, Quaternion.identity) as GameObject;
			newStraightTile.transform.parent = currentTilemap.transform;
			
			
			
			//currentTilemap.SetTile(buildPos, straightTrackPrefab);
			
			//_straightTrack = Instantiate(straightTrackPrefab);
			//currentTilemap.SetTile(buildPos, _straightTrack);
		}
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int pos = currentTilemap.WorldToCell(mousePosition);
		
		
		if (Input.GetMouseButton(0))
		{
			PlaceSingleTile(pos);
		}
    }
	
	
	void PlaceSingleTile(Vector3Int pos)
	{
		currentTilemap.SetTile(pos, currentTile);
	}
}
